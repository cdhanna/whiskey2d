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


        ContentManager Content;
        GraphicsDevice Device;

        GameController controller;

        RenderManager renMan;
        ObjectManager objMan = new Managers.Impl.DefaultObjectManager();
        ResourceManager resMan;
        InputManager inMan;
        LogManager logMan;
        InputSourceManager sourceMan;
        HudManager hudMan;

        int width;
        int height;

        Starter starter;


        //MANAGER PROPERTIES : TODO not all should be public
        public static InputManager Input { get { return getInstance().inMan; } }
        public static InputSourceManager InputSource { get { return getInstance().sourceMan; } }
        public static LogManager Log { get { return getInstance().logMan; } }
        public static ObjectManager Objects { get { return getInstance().objMan; } }
        public static RenderManager Renderer { get { return getInstance().renMan; } }
        public static ResourceManager Resources { get { return getInstance().resMan; } }
        public static GameController Controller { get { return getInstance().controller; } }

        protected GameManager()
        {
            settings = new PropertiesFiles(".gameprops");
            this.objMan.init();
            instance = this;


        }

        /// <summary>
        /// The width of the window
        /// </summary>
        public int ScreenWidth { get { return width; } }

        /// <summary>
        /// The height of the window
        /// </summary>
        public int ScreenHeight { get { return height; } }

        public TimeSpan TargetElapsedTime { get; set; }

        ///// <summary>
        ///// Launch the game
        ///// </summary>
        //public void go()
        //{
        //    this.Run();
        //}

        /// <summary>
        /// Closes all managers, and re-inits them
        /// </summary>
        public virtual void reset()
        {
            renMan.close();
            objMan.close();
            resMan.close();
            inMan.close();
            logMan.close();

            Rand.getInstance().reSeed();

            sourceMan.getSource().init();

            renMan.init(Device);
            objMan.init();
            resMan.init(Content);
            inMan.init();
            logMan.init();


            //RUN THE START CODE
            GameManager.Objects.setState(State.deserialize(CurrentScenePath));
        }

        private void start()
        {
            GameManager.Objects.setState(State.deserialize(StartScenePath));
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
            ContentManager Content,
            GraphicsDevice Device,
            InputManager inputMan,
            InputSourceManager inputSourceMan,
            LogManager logger,
            ObjectManager objectMan,
            RenderManager renderMan,
            ResourceManager resourceMan
            )
        {
            this.Device = Device;
            if (this.Device != null)
            {
                width = Device.PresentationParameters.BackBufferWidth;
                height = Device.PresentationParameters.BackBufferHeight;
            }

            this.Content = Content;
            if (Content != null)
            {
                Content.RootDirectory = "media";
            }

            
            this.controller = controller;


            
            inMan = inputMan;
            sourceMan = inputSourceMan;
            logMan = logger;


            if (objMan != null)
            {
                objMan.close();
            }
            objMan = objectMan;
            
            
            
            renMan = renderMan;
            resMan = resourceMan;
           
            
            hudMan = HudManager.getInstance();

            renMan.init(Device);
            objMan.init();
            resMan.init(Content);
            inMan.init();
            logMan.init();
            hudMan.init();
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
            renMan.close();
            objMan.close();
            resMan.close();
            inMan.close();
            logMan.close();
            hudMan.close();
            Console.WriteLine("CLOSING");

        }

        public virtual void Exit()
        {
            UnloadContent();
            //TODO signal for close
            //Exit();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
            

            hudMan.update();

            if (!hudMan.ConsoleMode)
            {
                inMan.update();
                sourceMan.update();
                logMan.update();
                objMan.updateAll();
            }
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Draw(GameTime gameTime)
        {
            Device.Clear(Color.LightGray);

            this.renMan.render();
            this.renMan.renderHud();
        }
    }
}
