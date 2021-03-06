﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;


namespace Whiskey2D.Core.Managers.Impl
{

    public static class ResourceCache
    {
        public static readonly Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();
        public static readonly Dictionary<string, Texture2D> Images = new Dictionary<string, Texture2D>();
        public static readonly Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();
        
    }


    /// <summary>
    /// Loads different Resources into the WHiskey Game
    /// </summary>
    public class DefaultResourceManager : ResourceManager
    {

        private static DefaultResourceManager instance = new DefaultResourceManager();


        /// <summary>
        /// Retrives the ResourceManager
        /// </summary>
        /// <returns>The ResourceManager</returns>
        public static DefaultResourceManager Instance { get { return instance; } }

        
        public ContentManager Content { get; private set; }
        private SpriteFont defaultFont;

        private DefaultResourceManager()
        {
        }

        /// <summary>
        /// Initializes the ResourceManager
        /// </summary>
        /// <param name="content">The content pipeline to use for loading resources</param>
        public void init(ContentManager content)
        {
           
            this.Content = content;
            if (content != null)
            {
                
                this.defaultFont = content.Load<SpriteFont>("font");

               
                //for each file in the art directory that is a .png
                //  load it.
                foreach (string pngFile in (System.IO.Directory.GetFiles(content.RootDirectory)))
                {
                    string contentFile = pngFile.Substring(content.RootDirectory.Length + 1);
                    if (contentFile.EndsWith(".png"))
                    {
                        //contentFile = contentFile.Replace(".png", "");
                        //if (GameManager.Log != null)
                        // GameManager.Log.debug(contentFile);
                        loadImage(contentFile);

                    }
                    if (contentFile.EndsWith(".wav"))
                    {
                        loadSound(contentFile);
                        

                    }


                }

            }
        }

        public void close()
        {
        }

        /// <summary>
        /// Loads an Image
        /// </summary>
        /// <param name="filePath">The filepath to an image</param>
        /// <returns>The Image</returns>
        public Texture2D loadImage(string filePath)
        {
            if (!ResourceCache.Images.ContainsKey(filePath))
            {
                //if (GameManager.Log != null)
                //    GameManager.Log.debug("sprite " + filePath + " from disc");
                Texture2D tex = Content.Load < Texture2D>(filePath);
                ResourceCache.Images.Add(filePath, tex);
            }
            else
            {
                //if (GameManager.Log != null)
                //    GameManager.Log.debug("sprite " + filePath + " from cache");
            }
            return ResourceCache.Images[filePath];
        }

        public SoundEffect loadSound(string filePath)
        {

            if (!ResourceCache.Sounds.ContainsKey(filePath))
            {
                SoundEffect s = Content.Load<SoundEffect>(filePath);
                ResourceCache.Sounds.Add(filePath, s);
            }
            return ResourceCache.Sounds[filePath];
        }


        public Effect loadEffect(string filePath)
        {
            filePath = filePath.Replace(".hlsl", ".mgfx");

            if (!ResourceCache.Effects.ContainsKey(filePath))
            {
                GameManager.Log.debug("LOADING EFFECT :" + filePath);
                Effect e = Content.Load<Effect>(filePath);
                ResourceCache.Effects.Add(filePath, e);
            }
            return ResourceCache.Effects[filePath];
        }

        public SpriteFont getDefaultFont()
        {
            return defaultFont;
        }


 
    }
}
