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

            WriteToAlpha = new BlendState();
            WriteToAlpha.ColorWriteChannels = ColorWriteChannels.Alpha;

            MultiplyWithAlpha = new BlendState();
            MultiplyWithAlpha.ColorDestinationBlend = MultiplyWithAlpha.AlphaDestinationBlend = Blend.One;
            MultiplyWithAlpha.ColorSourceBlend = MultiplyWithAlpha.AlphaSourceBlend = Blend.DestinationAlpha;
        }
        public static BlendState Multiplicative { get; private set; }
        public static BlendState WriteToAlpha { get; private set; }
        public static BlendState MultiplyWithAlpha { get; private set; }



    }
}
