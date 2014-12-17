using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Whiskey2D.Core.Managers.Impl;
using Whiskey2D.Core;
using Whiskey2D.PourGames.Game3;
using System.IO;

namespace WhiskeyRunner
{


    /// <summary>
    /// A way to run the GameManager through MonoGame's built in Game
    /// </summary>
    public class MonoBaseGame : Game , GameController
    {

        GameManager gameMan;
        GraphicsDeviceManager graphics;



        public MonoBaseGame() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "media";

            
        }


        protected override void OnExiting(object sender, EventArgs args)
        {
            gameMan.Exit();
            base.OnExiting(sender, args);
        }

        protected override void Initialize()
        {
            gameMan = GameManager.getInstance();

            gameMan.Initialize(this, Content, GraphicsDevice,
                new DefaultInputManager(),
                new DefaultInputSourceManager(),
                DefaultLogManager.getInstance(),
                new DefaultObjectManager(),
                new DefaultRenderManager(),
                DefaultResourceManager.getInstance()
            );
            base.Initialize();


        }

        protected override void LoadContent()
        {
            gameMan.LoadContent();
            base.LoadContent();


        }

        protected override void UnloadContent()
        {
            gameMan.UnloadContent();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            gameMan.Update(gameTime);
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            gameMan.Draw(gameTime);
            base.Draw(gameTime);



        }


        public GameObject SelectedGob
        {
            get;
            set;
        }

    }
}
