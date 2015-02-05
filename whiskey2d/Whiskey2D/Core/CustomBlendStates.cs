using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.Core
{
    //taken from http://www.catalinzima.com/xna/samples/dynamic-2d-shadows/
    public class CustomBlendStates
    {
        static CustomBlendStates()
        {
            Multiplicative = new BlendState();
            Multiplicative.ColorSourceBlend = Multiplicative.AlphaSourceBlend = Blend.Zero;
            Multiplicative.ColorDestinationBlend = Multiplicative.AlphaDestinationBlend = Blend.SourceColor;
            Multiplicative.ColorBlendFunction = Multiplicative.AlphaBlendFunction = BlendFunction.Add;

            MultiplyShadows = new BlendState();
            MultiplyShadows.ColorWriteChannels = ColorWriteChannels.Alpha;
            MultiplyShadows.AlphaDestinationBlend = Blend.SourceAlpha;
            MultiplyShadows.AlphaSourceBlend = Blend.Zero;
            MultiplyShadows.AlphaBlendFunction = BlendFunction.Add;

            MultiplyWithAlpha = new BlendState();
            MultiplyWithAlpha.ColorDestinationBlend = MultiplyWithAlpha.AlphaDestinationBlend = Blend.One;
            MultiplyWithAlpha.ColorSourceBlend = MultiplyWithAlpha.AlphaSourceBlend = Blend.DestinationAlpha;

            AlphaOnly = new BlendState();
            AlphaOnly.ColorWriteChannels = ColorWriteChannels.Alpha;

        }
        public static BlendState Multiplicative { get; private set; }
        public static BlendState MultiplyShadows { get; private set; }
        public static BlendState MultiplyWithAlpha { get; private set; }
        public static BlendState AlphaOnly { get; private set; }



    }
}
