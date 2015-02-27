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
    /// <summary>
    /// Light is a visual object that illuminates the world, and can cast shadows.
    /// </summary>
    [Serializable]
    public class Light 
    {
        private static Texture2D defaultImage;

       

        /// <summary>
        /// Gets or sets the light position
        /// </summary>
        public virtual Vector Position { get; set; }

        /// <summary>
        /// Gets or Sets the light radius
        /// </summary>
        public virtual float Radius { get; set; }

        /// <summary>
        /// Gets or Sets the light Color
        /// </summary>
        public virtual Color Color { get; set; }
        
        /// <summary>
        /// Gets or Sets if the light is visible
        /// </summary>
        public virtual Boolean Visible { get; set; }

        
        /// <summary>
        /// Create a white light at Position={0,0}
        /// </summary>
        public Light()
            : this(Vector.Zero, Color.White)
        { }

        /// <summary>
        /// Create a white light at the given position
        /// </summary>
        /// <param name="position">some position</param>
        public Light(Vector position)
            : this(position, Color.White)
        { }

        /// <summary>
        /// Create a light, exactly like another light
        /// </summary>
        /// <param name="other">another light</param>
        public Light(Light other)
            : this(other.Position, other.Color, other.Radius, other.Visible)
        { }

        /// <summary>
        /// Create a light
        /// </summary>
        /// <param name="position">some position</param>
        /// <param name="color">some color</param>
        public Light(Vector position, Color color) : this(position, color, 512, true)
        { }
        
        /// <summary>
        /// Create a light
        /// </summary>
        /// <param name="position">some position</param>
        /// <param name="color">some color</param>
        /// <param name="radius">some radius</param>
        /// <param name="visible">should the light be visible?</param>
        public Light(Vector position, Color color, float radius, bool visible)
        {
            Visible = visible;
            Position = position;
            Color = color;
            Radius = radius;
           
        }

        /// <summary>
        /// Renders the light
        /// </summary>
        /// <param name="info">The info needed to render the light</param>
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
