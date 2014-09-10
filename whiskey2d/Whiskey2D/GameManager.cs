#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Whiskey2D.Core;

using Whiskey2D.TestImpl;
#endregion

namespace Whiskey2D
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameManager : Game
    {
        GraphicsDeviceManager graphics;
        
        RenderManager renMan;
        ObjectManager objMan;
        ResourceManager resMan;
        InputManager inMan;

        public GameManager()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            renMan = RenderManager.getInstance();
            objMan = ObjectManager.getInstance();
            resMan = ResourceManager.getInstance();
            inMan = InputManager.getInstance();
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
            inMan.init();
          
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            #region TEMP CODE
            GameObject gob = new Player();
            Texture2D text = resMan.loadImage("ai.png");
           
            gob.Sprite = new Sprite(text);
            gob.Position = new Vector2(100, 100);
            //gob.Sprite.Scale = new Vector2(100, 100);
            #endregion
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            renMan.close();
            objMan.close();
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
