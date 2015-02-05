using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Whiskey2D.Core.Managers.Impl;
using Whiskey2D.Core.Managers;

namespace Whiskey2D.Core
{
    [Serializable]
    public class Light 
    {
        private static Texture2D defaultImage;

       


        public virtual Vector Position { get; set; }


        public virtual float Radius { get; set; }


        public virtual Color Color { get; set; }
        

        public virtual Boolean Visible { get; set; }

        

        public Light()
            : this(Vector.Zero, Color.White)
        { }

        public Light(Vector position)
            : this(position, Color.White)
        { }

        public Light(Light other)
            : this(other.Position, other.Color)
        { }

        public Light(Vector position, Color color) : this(position, color, 512, true)
        { }
        
        public Light(Vector position, Color color, float radius, bool visible)
        {
            Visible = visible;
            Position = position;
            Color = color;
            Radius = radius;
           
        }

        public void render(RenderInfo info)
        {
            if (!Visible)
                return; //do not render if not visible

            if (defaultImage == null){
                defaultImage = LightTextureBuilder.CreatePointLight(info.GraphicsDevice, 512);
            }
           
            
            Vector offset = new Vector(256, 256);

            info.SpriteBatch.Draw(defaultImage, Position, null, Color, 0, offset, Radius / 512f, SpriteEffects.None, 0);
        }


        
    }
}
