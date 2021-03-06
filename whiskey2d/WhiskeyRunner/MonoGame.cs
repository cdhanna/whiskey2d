﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Whiskey2D.Core.Managers.Impl;
using Whiskey2D.Core;

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

        bool started = false;

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

            
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
            //this.graphics.IsFullScreen = true;

            Type type = typeof(OpenTKGameWindow);
            System.Reflection.FieldInfo field = type.GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            OpenTK.GameWindow window = (OpenTK.GameWindow)field.GetValue(this.Window);
            window.X = 50;
            window.Y = 50;
            this.graphics.ApplyChanges();

            gameMan = GameManager.Instance;

            if (gameMan.IsFullScreen)
                graphics.IsFullScreen = true;

            gameMan.Initialize(this, Content, GraphicsDevice,
                new DefaultInputManager(),
                new DefaultInputSourceManager(),
                DefaultLogManager.Instance,
                new DefaultObjectManager(),
                new DefaultRenderManager(),
                DefaultResourceManager.Instance
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
