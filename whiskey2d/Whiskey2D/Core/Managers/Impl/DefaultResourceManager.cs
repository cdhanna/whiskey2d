using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Whiskey2D.Core.Managers.Impl
{

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

        private static Dictionary<string, Texture2D> texCache;
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
            texCache = new Dictionary<string, Texture2D>();
            this.Content = content;
            if (content != null)
            {
                
                this.defaultFont = content.Load<SpriteFont>("font");
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
            if (!texCache.ContainsKey(filePath))
            {
                GameManager.Log.debug("sprite " + filePath + " from disc");
                Texture2D tex = Content.Load < Texture2D>(filePath);
                texCache.Add(filePath, tex);
            }
            else
            {
                GameManager.Log.debug("sprite " + filePath + " from cache");
            }
            return texCache[filePath];
        }

        public SoundEffect loadSound(string filePath)
        {
            return Content.Load<SoundEffect>(filePath);
        }


        public SpriteFont getDefaultFont()
        {
            return defaultFont;
        }


 
    }
}
