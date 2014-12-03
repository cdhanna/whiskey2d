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

        
        protected bool ActiveControl { get; set; }
        protected bool Working { get; set; }
        protected static List<WhiskeyControl> allControls = new List<WhiskeyControl>();

        protected static GameManager gameMan = GameManager.getInstance();
        protected static ContentManager content;
        protected static bool gameManInitialized = false;
        protected static Thread whiskeyThread;
        protected static bool whiskeyThreadRunKey = true;
        static Stopwatch timer;
        static TimeSpan TargetElapsedTime;
        static InputSource whiskeyInput = null;
        static InputSource requestedWhiskeyInput = null;
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
                           // gameMan.InputSourceManager.setRegularSource(requestedWhiskeyInput);
                           // gameMan.InputSourceManager.requestRegular();
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


        protected EditorInputSource inputSource;
        protected ObjectController oc;
        protected DefaultObjectManager editorObjects;
        protected static int idc;


        EditorRenderManager renderer;

        GameObject selectedGob;
        protected int id;
        private EventHandler updateHandler;
        private Thread backThread;
        private bool backThreadRunKey = true;

        private Level level;
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

        public WhiskeyControl()
        {
            id = idc++;
            allControls.Add(this);
        }

      //  public WhiskeyPropertyGrid GobGrid{get;set;}
      //  public ScriptCollection GobScriptCollection { get; set; }

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //}

        private void ensureGameManInitialized()
        {
            if (!gameManInitialized)
            {
                //updateHandler = new EventHandler((s, a) => { update(); });
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

        public void setAsActive()
        {
            inputSource = new EditorInputSource(this);
            requestedWhiskeyInput = inputSource;
            if (oc != null)
            {
                oc.Selected = null;
                oc.Unselect = true;
                oc.update();
                allControls.ForEach((w) => { w.ActiveControl = false; });
                ActiveControl = true;
            }
        }

        protected override void Initialize()
        {
            
            ensureGameManInitialized();
            editorObjects = new DefaultObjectManager();
            editorObjects.init();
            setAsActive();

            oc = new ObjectController();
            editorObjects.addObject(oc);
            gameMan.ObjectManager.removeObject(oc);

            renderer = new EditorRenderManager();
            renderer.init(base.GraphicsDevice);

            

            backThread = new Thread(() =>
            {
                while (backThreadRunKey)
                {
                    update();
                    Thread.Sleep(2);
                    //while (backThreadRunKey && Working)
                    //{
                    //}
                }
            });
            backThread.Name = "WHISKEYVIEW_BACK: " + id;
            backThread.Start();
            // Hook the idle event to constantly redraw our animation.
           // Application.Idle += updateHandler; 



            //add editor objects
            if (oc != null)
                oc.CurrentLevel = Level;
            //TypeDescriptor.AddAttributes(   typeof(Whiskey2D.Core.Vector),
            //                                new EditorAttribute(typeof(VectorEditor),
            //                                typeof(UITypeEditor)));
            ////TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Vector),
            //                                new TypeConverterAttribute(typeof(ExpandableObjectConverter)));



            //GameManager.Objects.getAllObjectsNotOfType<EditorObjects.EditorGameObject>().ForEach((g) => { g.close(); });

            //new Whiskey2D.PourGames.Game3.Game3Launch().start();
            
        }

        protected override void Dispose(bool disposing)
        {
            backThreadRunKey = false;
           // whiskeyThreadRunKey = false;
            editorObjects.close();
            renderer.close();
            allControls.Remove(this);
            if (allControls.Count == 0)
            {
                gameMan.close();
                gameManInitialized = false;
                whiskeyThreadRunKey = false;
            }
           // base.Dispose(disposing);
        }

        protected void update()
        {
            if (ActiveControl)
            {
                Working = true;
                editorObjects.updateAll();
                //if (timer.ElapsedMilliseconds > TargetElapsedTime.Milliseconds)
                //{
                    Console.WriteLine("update: " + id);
                //    gameMan.Update(null); //todo fix nullgametime


                //    timer.Restart();
                //}

                Invalidate();   //signals draw
            }
        }

        //public void close()
        //{
        //    Application.Idle -= updateHandler;

        //}

        protected override void Draw()
        {

          //  Console.WriteLine("draw: " + id);
            
            GraphicsDevice.Clear(Whiskey2D.Core.Color.Green);
            gameMan.Draw(null);

            renderer.render(editorObjects.getAllObjects());

            if (Level != null)
            {
                renderer.render(Level.Descriptors);
            }
            else
            {
                GraphicsDevice.Clear(Whiskey2D.Core.Color.Red);

            }
            Working = false;
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
