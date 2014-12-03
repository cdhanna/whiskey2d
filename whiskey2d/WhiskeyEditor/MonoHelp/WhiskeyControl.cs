using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using WinFormsGraphicsDevice;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers.Impl;
using Whiskey2D.Core.Inputs;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Drawing.Design;
using WhiskeyEditor.Backend.Managers;
using Whiskey2D.PourGames.Game3;
using WhiskeyEditor.Backend;
using WhiskeyEditor.EditorObjects;
using System.Threading;

namespace WhiskeyEditor.MonoHelp
{

    /// <summary>
    /// A winforms control that interacts with a WhiskeyGame
    /// </summary>
    public class WhiskeyControl : GraphicsDeviceControl, GameController
    {


       
        private static List<WhiskeyControl> allControls = new List<WhiskeyControl>();

        private static GameManager gameMan = GameManager.getInstance();     //The game manager
        private static ContentManager content;                              //A content manager
        private static bool gameManInitialized = false;                     //true once the game manager has initialized, false until then
        private static Thread whiskeyThread;                                //the thread that represents the main ticking of the whiskey engine
        private static bool whiskeyThreadRunKey = true;                     //true for as long as the whiskey engine should tick. set to false to turn it off
        private static Stopwatch timer;                                     //a time to use to make sure the game doesn't run too fast
        private static TimeSpan TargetElapsedTime;                          //target time per tick
        private static InputSource whiskeyInput = null;                     //the latest input system
        private static InputSource requestedWhiskeyInput = null;            //the desired input system. Set this one to change the real input system

        protected static void launchWhiskeyThread()
        {
            // Start the animation timer.
            timer = Stopwatch.StartNew();
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 8);
            whiskeyThreadRunKey = true;
            whiskeyThread = new Thread(() =>
            {
                while (whiskeyThreadRunKey)
                {
                    if (timer.ElapsedMilliseconds > TargetElapsedTime.Milliseconds)
                    {
                        if (whiskeyInput != requestedWhiskeyInput)
                        {
                            gameMan.InputSourceManager.hotSwapInput(requestedWhiskeyInput);
                            whiskeyInput = requestedWhiskeyInput;
                        }

                        gameMan.Update(null); //todo fix nullgametime
                        timer.Restart();
                    }
                    Thread.Sleep(2);
                }
            });
            whiskeyThread.Name = "WHISKEYVIEW_MAIN";
            whiskeyThread.Start();
        }


        private EditorInputSource inputSource;
        private ObjectController oc;
        private DefaultObjectManager editorObjects;
        private static int idc;


        private EditorRenderManager renderer;

        private GameObject selectedGob;
        private int id;
        private Thread backThread;
        private bool backThreadRunKey = true;


        /// <summary>
        /// True if this Control should be getting input and running.
        /// </summary>
        private bool ViewedControl { get; set; }

        /// <summary>
        /// True while the update/draw loop is occuring. This is useful for other threads checking in
        /// </summary>
        private bool Working { get; set; }
        
        
        /// <summary>
        /// Set the level that the WhiskeyControl is rendering
        /// </summary>
        public Level Level
        {
            get { return level; }
            set
            {
                level = value;
                if (oc != null)
                    oc.CurrentLevel = level;
            }
        }
        private Level level;



        /// <summary>
        /// Creates a WhiskeyControl
        /// </summary>
        public WhiskeyControl()
        {
            id = idc++;
            allControls.Add(this);
        }

        /// <summary>
        /// Make sure that the game manager has been initialized to at least one control. 
        /// </summary>
        private void ensureGameManInitialized()
        {
            if (!gameManInitialized)
            {
                content = new ContentManager(Services, "media");
                gameMan.Initialize(this, content, GraphicsDevice,
                    new DefaultInputManager(),
                    new DefaultInputSourceManager(),
                    DefaultLogManager.getInstance(),
                    new DefaultObjectManager(),
                    new DefaultRenderManager(),
                    DefaultResourceManager.getInstance()
                    );
                gameMan.CurrentScene = null;
                launchWhiskeyThread();
            }
            
            gameManInitialized = true;

        }

        /// <summary>
        /// called to set this control as the one that recieves input and updates objects
        /// </summary>
        public void setAsActive()
        {
            inputSource = new EditorInputSource(this);
            requestedWhiskeyInput = inputSource;
            if (oc != null)
            {
                oc.Selected = null;
                oc.Unselect = true;
                oc.update();
                allControls.ForEach((w) => { w.ViewedControl = false; });
                ViewedControl = true;
            }
        }

        /// <summary>
        /// called just after creation.
        /// </summary>
        protected override void Initialize()
        {
            //make sure GameManager exists
            ensureGameManInitialized();

            //create a new object manager, just for our control objects
            editorObjects = new DefaultObjectManager();
            editorObjects.init();

            //mark this control as the active one
            setAsActive();

            //construct the object controller, and make sure it resides in the correct object managers
            oc = new ObjectController();
            editorObjects.addObject(oc);
            gameMan.ObjectManager.removeObject(oc);

            //construct the special renderer for this scene
            renderer = new EditorRenderManager();
            renderer.init(base.GraphicsDevice);

            
            //create the backthread that will do the updating for us
            backThread = new Thread(() =>
            {
                while (backThreadRunKey)
                {
                    update();
                    Thread.Sleep(2);
                    while (backThreadRunKey && Working)
                    {
                        //waiting for the update loop to finish (it causes some other side affects that need to finish)
                    }
                }
            });
            backThread.Name = "WHISKEYVIEW_BACK: " + id;
            backThread.Start();
 


            //set the current level of the object controller
            if (oc != null)
                oc.CurrentLevel = Level;
         
            
        }


        /// <summary>
        /// Destroys most of the object. If this is the last WhiskeyControl around, the gamemanager will be terminated
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            backThreadRunKey = false;
            editorObjects.close();
            renderer.close();
            allControls.Remove(this);
            if (allControls.Count == 0)
            {
                gameMan.close();
                gameManInitialized = false;
                whiskeyThreadRunKey = false;
            }
           // base.Dispose(disposing); <-calling this does bad things
        }

        protected void update()
        {
            if (ViewedControl)
            {
                Working = true; //start working...
                editorObjects.updateAll();
                Invalidate(); 
            }
        }

 
        /// <summary>
        /// Draw the scene
        /// </summary>
        protected override void Draw()
        {
            //clear
            GraphicsDevice.Clear(Whiskey2D.Core.Color.Green);
            
            //draw things from the game manager
            gameMan.Draw(null);

            //draw our control stuff
            renderer.render(editorObjects.getAllObjects());

            //draw our instances
            if (Level != null)
                renderer.render(Level.Descriptors);
            else
                GraphicsDevice.Clear(Whiskey2D.Core.Color.Red);



            Working = false; //done working...


        }

       

        GameObject GameController.SelectedGob
        {
            get
            {
                return selectedGob;
            }
            set
            {
                selectedGob = value;
                //GobGrid.SelectedObject = value;
                //GobGrid.Refresh();

                //GobScriptCollection.SelectedObject = value;
                //GobScriptCollection.Refresh();

            }
        }



        void GameController.Exit()
        {
            //do nothing?
        }
    }

}
