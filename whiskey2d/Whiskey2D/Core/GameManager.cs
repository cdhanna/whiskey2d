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
using Whiskey2D.PourGames.TestImpl;
using Whiskey2D.PourGames.Game2;
#endregion

namespace Whiskey2D.Core
{
    /// <summary>
    /// The GameManager is the core part of every Whiskey Game. It is a derivation of the monoGame class.
    /// GameManager will control all of the game components
    /// </summary>
    public class GameManager : Game
    {
        GraphicsDeviceManager graphics;
        
        RenderManager renMan;
        ObjectManager objMan;
        ResourceManager resMan;
        InputManager inMan;
        LogManager logMan;


        ReplayService replServ;

        InputSource inputSource;


        Starter starter;

        /// <summary>
        /// Create a GameManager
        /// </summary>
        /// <param name="gameAssmebly">An assembly that contains classes that define a user's game. </param>
        public GameManager(Assembly gameAssmebly)
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            renMan = RenderManager.getInstance();
            objMan = ObjectManager.getInstance();
            resMan = ResourceManager.getInstance();
            inMan = InputManager.getInstance();
            logMan = LogManager.getInstance();
            inputSource = new RealKeyBoard();

            replServ = new ReplayService("whiskey.txt");
            inputSource = replServ;
            
            
            //find gameData assmebly
            //Type[] allGameTypes = gameAssmebly.GetTypes();
            //foreach (Type gt in allGameTypes)
            //{
            //    if (gt.IsSubclassOf(typeof(Starter)))
            //    {
            //        starter = (Starter)Activator.CreateInstance(gt);
            //    }
            //}
            starter = new LaunchPour2();

        }

        /// <summary>
        /// Launch the game
        /// </summary>
        public void go()
        {
            this.Run();
            this.TargetElapsedTime = TimeSpan.FromMilliseconds(60);
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            renMan.init(GraphicsDevice);
            objMan.init();
            resMan.init(Content);
            inMan.init(inputSource);
            logMan.init(inputSource);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

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
        protected override void UnloadContent()
        {
            renMan.close();
            objMan.close();
            resMan.close();
            inMan.close();
            logMan.close();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            inMan.update();
            replServ.update();
            logMan.update();
            objMan.updateAll();
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.renMan.render();
            base.Draw(gameTime);
        }
    }
}
