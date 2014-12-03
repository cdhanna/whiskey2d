using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Whiskey2D.Core.Managers.Impl
{

    /// <summary>
    /// Loads different Resources into the WHiskey Game
    /// </summary>
    public class DefaultResourceManager : ResourceManager
    {

        private static DefaultResourceManager instance;


        /// <summary>
        /// Retrives the ResourceManager
        /// </summary>
        /// <returns>The ResourceManager</returns>
        public static DefaultResourceManager getInstance()
        {
            if (instance == null)
            {
                instance = new DefaultResourceManager();
            }
            return instance;
        }


        private ContentManager content;

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
            this.content = content;
            if (content != null)
             this.defaultFont = content.Load<SpriteFont>("font");

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
            return content.Load<Texture2D>(filePath);
        }

        public SpriteFont getDefaultFont()
        {
            return defaultFont;
        }


 
    }
}
