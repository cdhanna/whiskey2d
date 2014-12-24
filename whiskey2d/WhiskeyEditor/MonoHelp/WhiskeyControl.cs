using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using WinFormsGraphicsDevice;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

    public delegate void WhiskeyControlEventHandler (object sender, WhiskeyControlEventArgs args);
    public class WhiskeyControlEventArgs : EventArgs
    {
        public InstanceDescriptor SelectedObject { get; private set; }
        public WhiskeyControlEventArgs(InstanceDescriptor selected)
        {
            SelectedObject = selected;
        }
    }

    /// <summary>
    /// A winforms control that interacts with a WhiskeyGame
    /// </summary>
    public class WhiskeyControl : GraphicsDeviceControl, GameController
    {

        public static GraphicsDevice WhiskeyGraphicsDevice { get; private set; }
        public static EditorRenderManager Renderer { get; private set; }
        public static EditorResourceManager Resources { get; private set; }
        public static ContentManager Content { get; private set; }


        public static DefaultInputManager InputManager { get; private set; }
        public static DefaultInputSourceManager InputSourceManager { get; private set; }

        public static GameController Controller { get; private set; }

        private static List<WhiskeyControl> allControls = new List<WhiskeyControl>();
        private static WhiskeyControl active;
        private static Camera defaultCamera = new Camera();

        //private static GameManager gameMan = GameManager.getInstance();     //The game manager
        //private static ContentManager content;                              //A content manager
        //private static bool gameManInitialized = false;                     //true once the game manager has initialized, false until then
        //private static Thread whiskeyThread;                                //the thread that represents the main ticking of the whiskey engine
        //private static bool whiskeyThreadRunKey = true;                     //true for as long as the whiskey engine should tick. set to false to turn it off
        //private static Stopwatch timer;                                     //a time to use to make sure the game doesn't run too fast
        //private static TimeSpan TargetElapsedTime;                          //target time per tick
        //private static InputSource whiskeyInput = null;                     //the latest input system
        //private static InputSource requestedWhiskeyInput = null;            //the desired input system. Set this one to change the real input system

        //protected static void launchWhiskeyThread()
        //{
        //    // Start the animation timer.
        //    timer = Stopwatch.StartNew();
        //    TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 8);
        //    whiskeyThreadRunKey = true;
        //    whiskeyThread = new Thread(() =>
        //    {
        //        while (whiskeyThreadRunKey)
        //        {
        //            if (timer.ElapsedMilliseconds > TargetElapsedTime.Milliseconds)
        //            {
        //                if (whiskeyInput != requestedWhiskeyInput)
        //                {
        //                    gameMan.InputSourceManager.hotSwapInput(requestedWhiskeyInput);
        //                    whiskeyInput = requestedWhiskeyInput;
        //                }
                        
        //               // gameMan.Update(null); //todo fix nullgametime
        //                timer.Restart();
        //            }
        //            Thread.Sleep(2);
        //        }
        //    });
        //    whiskeyThread.Name = "WHISKEYVIEW_MAIN";
        //    whiskeyThread.Start();
        //}

        
        public EditorInputSource InputSource { get; private set; }
        private ObjectController objectController;
        private DefaultObjectManager editorObjects;
        private static int idc;


        private Stopwatch timer;                                     //a time to use to make sure the game doesn't run too fast
        private TimeSpan TargetElapsedTime;                          //target time per tick
        //private EditorRenderManager renderer;

        private GameObject selectedGob;
        private int id;
        private Thread backThread;
        private bool backThreadRunKey = true;

        public event EventHandler BecameDirty = new EventHandler((s, a) => { });
        //public event WhiskeyControlEventHandler SelectionChanged = new WhiskeyControlEventHandler((s, a) => { });
        
        
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
        public EditorLevel Level
        {
            get { return level; }
            set
            {
                level = value;
                if (objectController != null)
                    objectController.CurrentLevel = level;

                //ensureGameManInitialized();
                //gameMan.ObjectManager.close();
                //gameMan.ObjectManager.init();
                //foreach (GameObject gob in editorObjects.getAllObjects()){
                //    gameMan.ObjectManager.addObject(gob);
                //}
                
                //foreach (InstanceDescriptor instance in level.Descriptors)
                //{
                //    gameMan.ObjectManager.addObject(instance);
                //}

            }
        }
        private EditorLevel level;

        public static Camera ActiveCamera
        {
            get
            {

                if (active != null && active.Level != null)
                    return active.Level.Camera;


                return defaultCamera;
            }
        }

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
        //private void ensureGameManInitialized()
        //{
        //    if (!gameManInitialized)
        //    {
        //        content = new ContentManager(Services, "media");
        //        gameMan.Initialize(this, content, GraphicsDevice,
        //            new DefaultInputManager(),
        //            new DefaultInputSourceManager(),
        //            DefaultLogManager.getInstance(),
        //            new DefaultObjectManager(),
        //            new DefaultRenderManager(),
        //            DefaultResourceManager.getInstance()
        //            );
        //        gameMan.CurrentScene = null;
        //        launchWhiskeyThread();
        //    }
            
        //    gameManInitialized = true;

        //}

        /// <summary>
        /// called to set this control as the one that recieves input and updates objects
        /// </summary>
        public void setAsActive()
        {
            SelectionManager.Instance.SelectedInstance = null;
            active = this;

            Renderer.Level = Level;

            if (IsHandleCreated)
            {

                allControls.ForEach((w) => { Application.Idle -= w.update; });
                Application.Idle += update;
                InputSourceManager.hotSwapInput(InputSource);
            }
            else
            {
                this.Load += (s, a) =>
                {
                    allControls.ForEach((w) => { Application.Idle -= w.update; });
                    Application.Idle += update;
                    InputSourceManager.hotSwapInput(InputSource);
                };
            }
            //inputSource = new EditorInputSource(this);
            //requestedWhiskeyInput = inputSource;
            //if (objectController != null)
            //{
            //    objectController.Selected = null;
            //    objectController.Unselect = true;
            //    objectController.update();
            
            //    ViewedControl = true;
            //}
        }

        /// <summary>
        /// called just after creation.
        /// </summary>
        protected override void Initialize()
        {
            Controller = this;
            WhiskeyGraphicsDevice = this.GraphicsDevice;
            
            InputSource = new EditorInputSource(this);
            if (Renderer == null)
            {
                Renderer = new EditorRenderManager();
               

                
                Content = new ContentManager(this.Services);
                Content.RootDirectory = "compile-media";
                //Content.RootDirectory = ProjectManager.Instance.ActiveProject.PathMedia;
                Resources = new EditorResourceManager();
                Resources.init(Content);
                Renderer.init(WhiskeyGraphicsDevice);
                //Resources = new DefaultResourceManager();

            }
            if (InputManager == null)
            {
                InputSourceManager = new DefaultInputSourceManager();
                InputManager = new DefaultInputManager();
                InputManager.init(InputSourceManager);
            }


           

            //make sure GameManager exists
            //ensureGameManInitialized();

            

            timer = Stopwatch.StartNew();
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 8);

            //create a new object manager, just for our control objects
            editorObjects = new DefaultObjectManager();
            editorObjects.init();

            //mark this control as the active one
            

            //construct the object controller, and make sure it resides in the correct object managers
            objectController = new ObjectController(editorObjects);
            objectController.Sprite = new Sprite(Renderer, Resources, objectController.Sprite);


           // editorObjects.addObject(objectController);
            //gameMan.ObjectManager.removeObject(objectController);

            //construct the special renderer for this scene
            //renderer = new EditorRenderManager();
            //renderer.init(base.GraphicsDevice);

            
            //create the backthread that will do the updating for us
            //backThread = new Thread(() =>
            //{
            //    while (backThreadRunKey)
            //    {
            //        update();

            //        while (backThreadRunKey && Working)
            //        {
            //            Thread.Sleep(2);
            //            waiting for the update loop to finish (it causes some other side affects that need to finish)
            //        }
            //    }
            //});
            //backThread.Name = "WHISKEYVIEW_BACK: " + id;
            //backThread.IsBackground = true;
            //backThread.Start();
            


            //set the current level of the object controller
            if (objectController != null)
                objectController.CurrentLevel = Level;

            



            //////////
            
            setAsActive();
        }

        protected void update(object sender, EventArgs args)
        {
            if (timer.ElapsedMilliseconds > TargetElapsedTime.Milliseconds)
            {
                Level.Camera.Size = new Vector(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
                InputSourceManager.update();
                InputManager.update();
                Level.updateAll();
                editorObjects.updateAll();

                timer.Restart();
            }
            Invalidate();
        }


        /// <summary>
        /// Destroys most of the object. If this is the last WhiskeyControl around, the gamemanager will be terminated
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (this == active)
                active = null;

            Application.Idle -= update;
            backThreadRunKey = false;
            editorObjects.close();
            //renderer.close();
            allControls.Remove(this);
            if (allControls.Count == 0)
            {
                //gameMan.close();
                //gameManInitialized = false;
                //whiskeyThreadRunKey = false;
            }
           // base.Dispose(disposing); <-calling this does bad things
        }

        //protected void update()
        //{
        //    if (ViewedControl)
        //    {
        //        Working = true; //start working...
        //        editorObjects.updateAll();
        //        //Invoke(new NoArgFunction(() => { Refresh(); }));
        //        //Draw();
        //        Invalidate(); 
        //    }
        //}

        public static void forceRedraw(){
            if (active != null)
            {
                //active.Draw();
                active.Invalidate();
            }
        }
 
        /// <summary>
        /// Draw the scene
        /// </summary>
        protected override void Draw()
        {
            //clear
            WhiskeyGraphicsDevice.Clear(Level.BackgroundColor);


            Renderer.renderAll(editorObjects.getAllObjects(), Level.getInstances());
           // Renderer.render(editorObjects.getAllObjects());
           // Renderer.render(Level.getInstances());
            //draw things from the game manager
            //gameMan.Draw(null);

            //draw our control stuff
            //renderer.render(editorObjects.getAllObjects());

            //draw our instances
            //if (Level != null)
            //    //renderer.render(Level.Descriptors);
            //    gameMan.Draw(null);
            //else
            //    GraphicsDevice.Clear(Whiskey2D.Core.Color.Red);



            Working = false; //done working...


        }

       

        //public GameObject SelectedGob
        //{
        //    get
        //    {
        //        return selectedGob;
        //    }
        //    set
        //    {
        //        GameObject old = selectedGob;
        //        selectedGob = value;
        //        //BecameDirty(this, new EventArgs());

        //        if (old != value)
        //            SelectionChanged(this, new WhiskeyControlEventArgs((InstanceDescriptor)value));
        //        //GobGrid.SelectedObject = value;
        //        //GobGrid.Refresh();

        //        //GobScriptCollection.SelectedObject = value;
        //        //GobScriptCollection.Refresh();

        //    }
        //}



        void GameController.Exit()
        {
            //do nothing?
        }

  
    }

}
