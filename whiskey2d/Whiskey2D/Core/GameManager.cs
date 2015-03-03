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


#endregion

namespace Whiskey2D.Core
{
    /// <summary>
    /// The GameManager is the core part of every Whiskey Game.
    /// GameManager will control all of the game components, and provide the services required for Designers ,such as Input, Objects, and Level data.
    /// </summary>
    public class GameManager
    {

        private static GameManager instance;


        /// <summary>
        /// Get the GameManager instance
        /// </summary>
        public static GameManager Instance { get { if (instance == null) instance = new GameManager(); return instance; } }
  
        PropertiesFiles settings;

        /// <summary>
        /// Gets the start scene name of the game. The start scene is the first level that is run when the game launches
        /// </summary>
        public string StartScene
        {
            get
            {
                return settings.get(GameProperties.START_SCENE);
            }
        }
    
        /// <summary>
        /// Gets the value of IsFullScreen. If IsFullScreen is true, then the Game should be running in full screen mode
        /// </summary>
        public bool IsFullScreen
        {
            get
            {
                return bool.Parse(settings.get(GameProperties.FULL_SCREEN));
            }
        }


        private string CurrentScene
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current scene's file path
        /// </summary>
        public string CurrentScenePath
        {
            get
            {
                return "states\\" + CurrentScene;
            }
        }

        /// <summary>
        /// Gets the start scene's file path
        /// </summary>
        public string StartScenePath
        {
            get
            {
                return "states\\" + StartScene;
            }
        }


        /// <summary>
        /// Gets the ContentManager. The ContentManager gives direct access to MonoGame's asset loading features
        /// </summary>
        public ContentManager Content { get; private set; }

        /// <summary>
        /// Gets the GraphicsDevice. The GraphicsDevice gives direct access to MonoGame's rendering
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }

        /// <summary>
        /// Gets the InputManager. The InputManager is what provides all of the Input assessment. Use InputManager to determine if specific keys are
        /// being pressed.
        /// </summary>
        public InputManager InputManager { get; protected set; }

        /// <summary>
        /// Gets the InputSourceManager. The InputSourceManager is what provides the InputManager with an InputSource. An InputSource can be a Log file,
        /// a keyboard+mouse, or something else.
        /// </summary>
        public InputSourceManager InputSourceManager { get; protected set; }

        /// <summary>
        /// Gets the LogManager. The LogManager can be used to display messages to the game's log file, as well as the game
        /// </summary>
        public LogManager LogManager { get; protected set; }

        /// <summary>
        /// Gets the ObjectManager. The ObjectManager is what controls all of the currently loaded GameObjects
        /// </summary>
        public ObjectManager ObjectManager { get; protected set; }

        /// <summary>
        /// Gets the RenderManager. The RenderManager is what draws all GameObjects to the screen.
        /// </summary>
        public RenderManager RenderManager { get; protected set; }

        /// <summary>
        /// Gets the ResourceManager. The ResourceManager is an abstraction of MonoGame's ContentManager, and gives specific functions for
        /// loading sounds and sprites.
        /// </summary>
        public ResourceManager ResourceManager { get; protected set; }

        /// <summary>
        /// Gets the object that is controlling the Game. In most cases, this will be the WhiskeyLauncher.exe 
        /// </summary>
        public GameController GameController { get; protected set; }

        /// <summary>
        /// Gets the HudManager. The HudManager draws all of the HUD GameObejcts to screen
        /// </summary>
        public HudManager HudManager { get; protected set; }


        private GameLevel _level;

        /// <summary>
        /// Gets the currently active Level.
        /// </summary>
        public GameLevel ActiveLevel
        {
            get
            {
                return _level;
            }
        }





        /// <summary>
        /// Gets the InputManager. The InputManager is what provides all of the Input assessment. Use InputManager to determine if specific keys are
        /// being pressed.
        /// </summary>
        public static InputManager Input { get { return Instance.InputManager; } }

        /// <summary>
        /// Gets the InputSourceManager. The InputSourceManager is what provides the InputManager with an InputSource. An InputSource can be a Log file,
        /// a keyboard+mouse, or something else.
        /// </summary>
        public static InputSourceManager InputSource { get { return Instance.InputSourceManager; } }

        /// <summary>
        /// Gets the LogManager. The LogManager can be used to display messages to the game's log file, as well as the game
        /// </summary>
        public static LogManager Log { get { return Instance.LogManager; } }

        /// <summary>
        /// Gets the ObjectManager. The ObjectManager is what controls all of the currently loaded GameObjects
        /// </summary>
        public static ObjectManager Objects { get { return Instance.ObjectManager; } }

        /// <summary>
        /// Gets the RenderManager. The RenderManager is what draws all GameObjects to the screen.
        /// </summary>
        public static RenderManager Renderer { get { return Instance.RenderManager; } }

        /// <summary>
        /// Gets the ResourceManager. The ResourceManager is an abstraction of MonoGame's ContentManager, and gives specific functions for
        /// loading sounds and sprites.
        /// </summary>
        public static ResourceManager Resources { get { return Instance.ResourceManager; } }

        /// <summary>
        /// Gets the object that is controlling the Game. In most cases, this will be the WhiskeyLauncher.exe 
        /// </summary>
        public static GameController Controller { get { return Instance.GameController; } }
        
        /// <summary>
        /// Gets the currently active Level.
        /// </summary>
        public static GameLevel Level { get { return Instance.ActiveLevel; } }
        
        /// <summary>
        /// Gets the screen width of the game window, or -1 if there is an error
        /// </summary>
        public static int ScreenWidth { get { return Instance.WindowScreenWidth; } }
        
        /// <summary>
        /// Gets the screen height of the game window, or -1 if there is an error
        /// </summary>
        public static int ScreenHeight { get { return Instance.WindowScreenHeight; } }


        /// <summary>
        /// Changes the active level of the game
        /// </summary>
        /// <param name="levelName">The name of the level to load. Make sure to include the .state extension</param>
        public static void SetLevel(string levelName)
        {
            Instance.loadLevel(levelName);
        }

        /// <summary>
        /// Closes the game
        /// </summary>
        public static void Quit()
        {
            Instance.Exit();
        }



        /// <summary>
        /// Create singleton instance.
        /// Auto sets up basic managers
        /// </summary>
        protected GameManager()
        {
            settings = new PropertiesFiles(".gameprops");
            instance = this;
            ObjectManager = new DefaultObjectManager();
            InputManager = new DefaultInputManager();
            InputSourceManager = new DefaultInputSourceManager();
            LogManager = DefaultLogManager.Instance;
                
            
            HudManager = HudManager.Instance;
            ObjectManager.init();
            InputManager.init();

        }

        /// <summary>
        /// The width of the window, or -1 if the graphics device is null
        /// </summary>
        public int WindowScreenWidth { get { return (GraphicsDevice!=null && GraphicsDevice.PresentationParameters != null) ? GraphicsDevice.PresentationParameters.BackBufferWidth : -1; } }

        /// <summary>
        /// The height of the window, or -1 if the graphics device is null
        /// </summary>
        public int WindowScreenHeight { get { return (GraphicsDevice != null && GraphicsDevice.PresentationParameters != null) ? GraphicsDevice.PresentationParameters.BackBufferHeight : -1; } }

        public TimeSpan TargetElapsedTime { get; set; }

        /// <summary>
        /// Loads a new game level
        /// </summary>
        /// <param name="name">The name of the new level, with the .state extension</param>
        /// <returns></returns>
        public GameLevel loadLevel(string name) //input comes as "myLevel.state"
        {
            string path ="states\\" + name ;

            GameLevel level = GameLevel.deserialize(path);
            ObjectManager.close();
            ObjectManager = level;
            _level = level;
            level.Camera.Position = Vector.Zero;
            return level;
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

            Rand.Instance.reSeed();

            InputSourceManager.getSource().init();
            RenderManager.init(GraphicsDevice);
            ObjectManager.init();
            ResourceManager.init(Content);
            InputManager.init();
            LogManager.init();

            //RUN THE START CODE
            if (CurrentScene != null)
            {
                GameLevel lvl = loadLevel(CurrentScene);
            }
        }

        private void start()
        {
            if (StartScene != null)
            {
                GameLevel lvl = loadLevel(StartScene);
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
            ResourceManager = resourceMan;
            RenderManager = renderMan;
            
            HudManager = HudManager.Instance;

            //initialize
            ResourceManager.init(content);
            RenderManager.init(graphicsDevice);
            ObjectManager.init();
            
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
            HudManager.Instance.DebugColor = Color.RoyalBlue;
            start();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public virtual void UnloadContent()
        {
            try
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
            catch (Exception e)
            {
                // this is pure sin
            }
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
                    // Get stack trace for the exception with source file information
                    var st = new System.Diagnostics.StackTrace(e, true);
                    // Get the top stack frame
                    var frame = st.GetFrame(1);
                    // Get the line number from the stack frame
                    var line = frame.GetFileName() + " at " + frame.GetFileLineNumber();


                    LogManager.error(e.Source + ":: " + e.Message);
                   // HudManager.ConsoleMode = true;
                }
            }
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Draw(GameTime gameTime)
        {
            this.RenderManager.render();
            this.RenderManager.renderHud();
        }
    }
}
