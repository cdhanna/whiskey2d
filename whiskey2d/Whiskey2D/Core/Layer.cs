using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;



namespace Whiskey2D.Core
{

    [Serializable]
    public enum BlendMode
    {
        NORMAL,
        ADDITIVE,
        ALPHABLEND
    }

    public static class BlendModeConverter
    {
        public static BlendState getState(this BlendMode mode)
        {
            switch (mode)
            {
                case BlendMode.ALPHABLEND:
                    return BlendState.AlphaBlend;
                case BlendMode.ADDITIVE:
                    return BlendState.Additive;
                case BlendMode.NORMAL:
                    return BlendState.NonPremultiplied;
                default:
                    return BlendState.NonPremultiplied;
            }
        }
    }

    [Serializable]
    public class Layer
    {
        public const string DEFAULT_SHADER_MODE = "default";
        public static readonly Layer UNKNOWN = new Layer();

        public Boolean Visible { get; set; }
        public Boolean Locked { get; set; }
        public string Name { get; set; }

        private string shaderMode;
        private string postShader;
        private BlendMode blendMode;
        private Boolean screenShader;
        public string ShaderMode { get { return shaderMode == null ? DEFAULT_SHADER_MODE : shaderMode; } set {shaderMode = value; } }
        public string PostShaderMode { get { return postShader == null ? DEFAULT_SHADER_MODE : postShader; } set { postShader = value; } } 
        public BlendMode BlendMode { get { return blendMode == null ? BlendMode.NORMAL : blendMode; } set { blendMode = value; } }
        public Boolean ScreenShader { get { return screenShader == null ? false : screenShader; } set { screenShader = value; } }


        [NonSerialized]
        private Effect effect;
        public Effect getEffect()
        {
            return effect;
        }
        public Effect setEffect(Effect effect)
        {
            this.effect = effect;
            return getEffect();
        }


        [NonSerialized]
        private Effect postEffect;
        public Effect getPostEffect()
        {
            return postEffect;
        }
        public Effect setPostEffect(Effect post)
        {
            this.postEffect = post;
            return getPostEffect();
        }


        public Layer(string name) : this()
        {
            Name = name;
        }

        public Layer()
        {
            Visible = true;
            Locked = false;
            ShaderMode = DEFAULT_SHADER_MODE;
        }

    }
}
