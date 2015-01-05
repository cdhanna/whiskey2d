using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using WhiskeyEditor.Backend.Managers;
using System.IO;

namespace WhiskeyEditor.MonoHelp
{
    public class EditorResourceManager : ResourceManager
    {
        private ContentManager content;

        private SpriteFont defaultFont;

        public EditorResourceManager()
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

            string destPath = "compile-media" + Path.DirectorySeparatorChar + filePath;


            //file is in project space
            string fullPath = ProjectManager.Instance.ActiveProject.PathMedia + Path.DirectorySeparatorChar + filePath;
            if (File.Exists(fullPath))
            {
                File.Copy(fullPath, destPath, true);
            }
            Texture2D texture = content.Load<Texture2D>(filePath);
            return texture;
        }

        public SoundEffect loadSound(string filePath)
        {
            return null; //TODO FIX 
            //string destPath = "compile-media" + Path.DirectorySeparatorChar + filePath;
        }


        public SpriteFont getDefaultFont()
        {
            return defaultFont;
        }

    }
}
