using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Whiskey2D.Core
{
    class ResourceManager
    {

        private static ResourceManager instance;
        public static ResourceManager getInstance()
        {
            if (instance == null)
            {
                instance = new ResourceManager();
            }
            return instance;
        }


        private ContentManager content;

        private ResourceManager()
        {
        }


        public void init(ContentManager content)
        {
            this.content = content;
        }

        public void close()
        {
        }

        public Texture2D loadImage(string filePath)
        {
            return content.Load<Texture2D>(filePath);
        }

    }
}
