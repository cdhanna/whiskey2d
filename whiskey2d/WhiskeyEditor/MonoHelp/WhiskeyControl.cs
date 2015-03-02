using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using WinFormsGraphicsDevice;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using Whiskey2D.Core.Managers.Impl;
using Whiskey2D.Core.Inputs;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Drawing.Design;
using WhiskeyEditor.Backend.Managers;
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


        public static InputManager InputManager { get; private set; }
        public static DefaultInputSourceManager InputSourceManager { get; private set; }

        public static GameController Controller { get; private set; }

        private static List<WhiskeyControl> allControls = new List<WhiskeyControl>();
        public static WhiskeyControl Active { get; private set; }
        private static Camera defaultCamera = new Camera();

        
        public EditorInputSource InputSource { get; private set; }
        public ObjectController ObjectController { get; private set; }
        private DefaultObjectManager editorObjects;
        private static int idc;


        private Stopwatch timer;                                     //a time to use to make sure the game doesn't run too fast
        private TimeSpan TargetElapsedTime;                          //target time per tick

       
        private int id;
        private Thread backThread;
        private bool backThreadRunKey = true;

        public event EventHandler BecameDirty = new EventHandler((s, a) => { });
        
        
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
                if (ObjectController != null)
                    ObjectController.CurrentLevel = level;


            }
        }
        private EditorLevel level;

        public static Camera ActiveCamera
        {
            get
            {

                if (Active != null && Active.Level != null)
                    return Active.Level.Camera;


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
        /// called to set this control as the one that recieves input and updates objects
        /// </summary>
        public void setAsActive()
        {
            if (Renderer == null) return;


            SelectionManager.Instance.SelectedInstance = null;
            Active = this;

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
            ObjectController = new ObjectController(editorObjects);
            ObjectController.Sprite = new Sprite(Renderer, Resources, ObjectController.Sprite);



            //set the current level of the object controller
            if (ObjectController != null)
                ObjectController.CurrentLevel = Level;

            



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
            if (this == Active)
                Active = null;

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


        public static void forceRedraw(){
            if (Active != null)
            {
                //active.Draw();
                Active.Invalidate();
            }
        }
 
        /// <summary>
        /// Draw the scene
        /// </summary>
        protected override void Draw()
        {
            //clear

            Renderer.Level = Level;

            WhiskeyGraphicsDevice.Clear(Level.BackgroundColor);


            Renderer.renderAll(editorObjects.getAllObjects(), Level.getInstances());



            Working = false; //done working...


        }

 



        void GameController.Exit()
        {
            //do nothing?
        }

  
    }

}
