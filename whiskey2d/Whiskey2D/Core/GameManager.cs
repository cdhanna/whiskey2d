#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Reflection;
using Whiskey2D.Core.Hud;
using Whiskey2D.Core.Managers;
using Whiskey2D.Core.Managers.Impl;
using Whiskey2D.PourGames.TestImpl;
using Whiskey2D.PourGames.Game2;
using Whiskey2D.PourGames.Game3;

#endregion

namespace Whiskey2D.Core
{
    /// <summary>
    /// The GameManager is the core part of every Whiskey Game. It is a derivation of the monoGame class.
    /// GameManager will control all of the game components
    /// </summary>
    public class GameManager
    {

        private static GameManager instance;
        public static GameManager Instance { get { if (instance == null) instance = new GameManager(); return instance; } }
        public static GameManager getInstance()
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }

        PropertiesFiles settings;
        public string StartScene
        {
            get
            {
                return settings.get(GameProperties.START_SCENE);
            }
        }

        public bool IsFullScreen
        {
            get
            {
                return bool.Parse(settings.get(GameProperties.FULL_SCREEN));
            }
        }


        public string CurrentScene
        {
            get;
            set;
        }

        public string CurrentScenePath
        {
            get
            {
                return "states\\" + CurrentScene;
            }
        }
        public string StartScenePath
        {
            get
            {
                return "states\\" + StartScene;
            }
        }

       // private Color BackgroundColor { get; set; }


        //Core pieces of MonoGame
        public ContentManager Content { get; private set; }
        public GraphicsDevice GraphicsDevice { get; private set; }

        //non static manager properties
        public InputManager InputManager { get; protected set; }
        public InputSourceManager InputSourceManager { get; protected set; }
        public LogManager LogManager { get; protected set; }
        
        
        public ObjectManager ObjectManager { get; protected set; }

       

        private GameLevel _level;
        public GameLevel ActiveLevel
        {
            get
            {
                return _level;
            }
        }

        public RenderManager RenderManager { get; protected set; }
        public ResourceManager ResourceManager { get; protected set; }
        
        
        public GameController GameController { get; protected set; }
        public HudManager HudManager { get; protected set; }

         //static accessors
        public static InputManager Input { get { return Instance.InputManager; } }
        public static InputSourceManager InputSource { get { return Instance.InputSourceManager; } }
        public static LogManager Log { get { return Instance.LogManager; } }
        public static ObjectManager Objects { get { return Instance.ObjectManager; } }
        public static RenderManager Renderer { get { return Instance.RenderManager; } }
        public static ResourceManager Resources { get { return Instance.ResourceManager; } }
        public static GameController Controller { get { return Instance.GameController; } }
        public static GameLevel Level { get { return Instance.ActiveLevel; } }



        /// <summary>
        /// Create singleton instance.
        /// Auto sets up basic managers
        /// </summary>
        protected GameManager()
        {
            settings = new PropertiesFiles(".gameprops");
            instance = this;
            //BackgroundColor = Color.SkyBlue;
            //create all default managers
            ObjectManager = new DefaultObjectManager();
            //RenderManager = new DefaultRenderManager();
            InputManager = new DefaultInputManager();
            InputSourceManager = new DefaultInputSourceManager();
            LogManager = DefaultLogManager.getInstance();
            
            HudManager = HudManager.getInstance();

            //LogManager.init();
            ObjectManager.init();
            InputManager.init();


            

            //HudManager.init();

        }

        /// <summary>
        /// The width of the window, or -1 if the graphics device is null
        /// </summary>
        public int ScreenWidth { get { return (GraphicsDevice!=null && GraphicsDevice.PresentationParameters != null) ? GraphicsDevice.PresentationParameters.BackBufferWidth : -1; } }

        /// <summary>
        /// The height of the window, or -1 if the graphics device is null
        /// </summary>
        public int ScreenHeight { get { return (GraphicsDevice != null && GraphicsDevice.PresentationParameters != null) ? GraphicsDevice.PresentationParameters.BackBufferHeight : -1; } }

        public TimeSpan TargetElapsedTime { get; set; }


        public GameLevel loadLevel(string name) //input comes as "myLevel.state"
        {
            string path ="states\\" + name ;

            GameLevel level = GameLevel.deserialize(path);

            ObjectManager.close();

            ObjectManager = level;
            _level = level;
            level.Camera.Position = Vector.Zero;
            return level;
            //level.init();

        }

        /// <summary>
        /// Shuts down all managers
        /// </summary>
        public virtual void close()
        {
            if (RenderManager != null)
                RenderManager.close();
            if (ObjectManager != null)
                ObjectManager.close();
            if (ResourceManager != null)
                ResourceManager.close();
            if (InputManager != null)
                InputManager.close();
            if (LogManager != null)
                LogManager.close();
        }

        /// <summary>
        /// Closes all managers, and re-inits them
        /// </summary>
        public virtual void reset()
        {
            close();

            Rand.getInstance().reSeed();

            InputSourceManager.getSource().init();
            RenderManager.init(GraphicsDevice);
            ObjectManager.init();
            ResourceManager.init(Content);
            InputManager.init();
            LogManager.init();

            //RUN THE START CODE
            if (CurrentScene != null)
            {
                //State RunningState = State.deserialize(CurrentScenePath);
                //BackgroundColor = RunningState.BackgroundColor;
                //GameManager.Objects.setState(RunningState);
                GameLevel lvl = loadLevel(CurrentScene);
                //BackgroundColor = lvl.BackgroundColor;
            }
        }

        private void start()
        {
            if (StartScene != null)
            {
                //State RunningState = State.deserialize(StartScenePath);
                //BackgroundColor = RunningState.BackgroundColor;
                //GameManager.Objects.setState(RunningState);
                GameLevel lvl = loadLevel(StartScene);
                //BackgroundColor = lvl.BackgroundColor;
            }
            CurrentScene = StartScene;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public virtual void Initialize(
            GameController controller,
            ContentManager content,
            GraphicsDevice graphicsDevice,
            InputManager inputMan,
            InputSourceManager inputSourceMan,
            LogManager logger,
            ObjectManager objectMan,
            RenderManager renderMan,
            ResourceManager resourceMan
            )
        {
            //set the basic components
            GraphicsDevice = graphicsDevice;
            Content = content;
            GameController = controller;

            //close any previous managers
            close();
            
            //assign new managers
            InputManager = inputMan;
            InputSourceManager = inputSourceMan;
            LogManager = logger;
            ObjectManager = objectMan;
            RenderManager = renderMan;
            ResourceManager = resourceMan;
            HudManager = HudManager.getInstance();

            //initialize
            RenderManager.init(graphicsDevice);
            ObjectManager.init();
            ResourceManager.init(content);
            InputManager.init();
            LogManager.init();
            HudManager.init();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public virtual void LoadContent()
        {
            HudManager.getInstance().DebugColor = Color.RoyalBlue;
            start();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public virtual void UnloadContent()
        {
            LogManager.debug("SHUTTING DOWN MANAGERS");
            RenderManager.close();
            ObjectManager.close();
            ResourceManager.close();
            InputManager.close();
            HudManager.close();
            LogManager.debug("CLOSING");
            LogManager.close();
           
        }

        public virtual void Exit()
        {
            UnloadContent();
            GameController.Exit();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
            

            HudManager.update();

            if (!HudManager.ConsoleMode)
            {
                InputManager.update();
                InputSourceManager.update();
                LogManager.update();


                try
                {
                    ObjectManager.updateAll();
                }
                catch (Exception e)
                {
                    LogManager.error(e.Message);
                }
            }
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Draw(GameTime gameTime)
        {
            
           GraphicsDevice.Clear(ActiveLevel.BackgroundColor);

            this.RenderManager.render();
            this.RenderManager.renderHud();
        }
    }
}
