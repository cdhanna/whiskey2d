using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Whiskey2D.Core.Managers
{

    /// <summary>
    /// The ResourceManager can load in Textures and Sounds
    /// </summary>
    public interface ResourceManager
    {

        /// <summary>
        /// Gets the MonoGame ContentManager
        /// </summary>
        ContentManager Content { get; }

        /// <summary>
        /// Initializes the ResourceManager
        /// </summary>
        /// <param name="content">The content pipeline to use for loading resources</param>
        void init(ContentManager content);

        /// <summary>
        /// Closes the ResourceManager
        /// </summary>
        void close();

        /// <summary>
        /// Loads an Image
        /// </summary>
        /// <param name="filePath">The filepath to an image</param>
        /// <returns>The Image</returns>
        Texture2D loadImage(string filePath);

        /// <summary>
        /// Loads a sound effect
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        SoundEffect loadSound(string filePath);

        Effect loadEffect(string filePath);

        /// <summary>
        /// Get the default font
        /// </summary>
        /// <returns></returns>
        SpriteFont getDefaultFont();



    }
}
