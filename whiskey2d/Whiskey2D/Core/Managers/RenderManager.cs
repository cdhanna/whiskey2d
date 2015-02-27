using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Whiskey2D.Core;

namespace Whiskey2D.Core.Managers
{

    /// <summary>
    /// The RenderManager is responsible for drawing everything to the screen
    /// </summary>
    public interface RenderManager
    {

        /// <summary>
        /// Gets or Sets the Camera that the RenderManager is using to display
        /// </summary>
        Camera ActiveCamera { get; set; }

        /// <summary>
        /// Gets the important RenderInfo for the rendering process
        /// </summary>
        RenderInfo RenderInfo { get; }


        /// <summary>
        /// Initializes the RenderManager
        /// </summary>
        void init(GraphicsDevice graphicsDevice);

        /// <summary>
        /// Closes out the RenderManager
        /// </summary>
        void close();

        /// <summary>
        /// Renders the Game
        /// </summary>
        void render();


        /// <summary>
        /// Renders the HUD
        /// </summary>
        void renderHud();


        /// <summary>
        /// Gets a pixel image
        /// </summary>
        /// <returns></returns>
        Texture2D getPixel();

    }
}
