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

using Whiskey2D.PourGames.TestImpl;
using Whiskey2D.PourGames.Game2;
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
        public static GameManager getInstance<G>() where G : GameManager
        {
            if (instance == null)
            {
                instance = Activator.CreateInstance<G>();
            }
            return instance;
        }



        ContentManager Content;
        GraphicsDevice Device;


        RenderManager renMan;
        ObjectManager objMan;
        ResourceManager resMan;
        InputManager inMan;
        LogManager logMan;
        InputSourceManager sourceMan;
        HudManager hudMan;

        int width;
        int height;

        Starter starter;

        


        protected GameManager()
        {

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
            //hudMan.close();

            Rand.getInstance().reSeed();

            sourceMan.getSource().init();

            //hudMan.init();
            renMan.init(Device);
            objMan.init();
            resMan.init(Content);
            inMan.init();
            logMan.init();


            //RUN THE START CODE
            if (starter != null)
            {
                starter.start();
            }
            else Console.WriteLine("ERROR: No start configuration found");


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public virtual void Initialize(ContentManager Content, GraphicsDevice Device)
        {
            this.Device = Device;
            this.Content = Content;
            Content.RootDirectory = "Content";


            width = Device.PresentationParameters.BackBufferWidth;
            height = Device.PresentationParameters.BackBufferHeight;

            renMan = RenderManager.getInstance();
            objMan = ObjectManager.getInstance();
            resMan = ResourceManager.getInstance();
            inMan = InputManager.getInstance();
            logMan = LogManager.getInstance();
            sourceMan = InputSourceManager.getInstance();
            hudMan = HudManager.getInstance();


            //find gameData assmebly
            //Type[] allGameTypes = gameAssmebly.GetTypes();
            //foreach (Type gt in allGameTypes)
            //{
            //    if (gt.IsSubclassOf(typeof(Starter)))
            //    {
            //        starter = (Starter)Activator.CreateInstance(gt);
            //    }
            //}
            starter = new Launch();




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


            //TextBox b = new TextBox();
            //b.Position = new Vector2(100, 100);
            //b.Size = new Vector2(200, 200);
            //b.TextSize = .8f;

            //b.pushTextFromBottom("a");
            //b.pushTextFromBottom("b");
            //b.pushTextFromBottom("c");

            //b.Text = "test this is a test\nnewline should have just happened";
            //b.append("another line");
            //b.append("abc");
            //b.prepend("in the start");
            //RUN THE START CODE
            if (starter != null)
            {
                starter.start();
            }
            else Console.WriteLine("ERROR: No start configuration found");

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
            Device.Clear(Color.CornflowerBlue);

            this.renMan.render();
            this.renMan.renderHud();
        }
    }
}
