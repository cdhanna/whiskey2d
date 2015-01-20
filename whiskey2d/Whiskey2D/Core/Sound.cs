using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Whiskey2D.Core.Managers;

namespace Whiskey2D.Core
{

    [Serializable]
    public class Sound 
    {

        public SoundState State
        {
            get
            {
                return getEffect().State;
            }
        }

        public bool Looped
        {
            get
            {
                return getEffect().IsLooped;
            }
            set
            {
                getEffect().IsLooped = value;
            }
        }

        public float Volume
        {
            get
            {
                return getEffect().Volume;
            }
            set
            {
                float linear = value;

                linear = (float)Math.Pow(10, (-10 + linear * 10));

                getEffect().Volume = linear;
            }
        }

        public float Pan
        {
            get
            {
                return getEffect().Pan;
            }
            set
            {
                getEffect().Pan = value;
            }
        }

        public float Pitch
        {
            get
            {
                return getEffect().Pitch;
            }
            set
            {
                getEffect().Pitch = value;
            }
        }

        public string FilePath { get; private set; }

        [NonSerialized]
        private SoundEffectInstance effect;


        public Sound(string filePath)
        {
            FilePath = filePath;
            getEffect();
        }

        public Sound(Sound sound)
        {
            FilePath = sound.FilePath;
            getEffect();
        }

        public Sound(string filePath, bool looped)
        {
            FilePath = filePath;
            Looped = looped;
        }


        private SoundEffectInstance getEffect()
        {
            if (effect == null)
            {
                effect = GameManager.Resources.loadSound(FilePath).CreateInstance();
            }

            

            return effect;
        }


        public void play()
        {
            getEffect().Play();
        }

        public void stop()
        {
            getEffect().Stop();
        }

        public void pause()
        {
            getEffect().Pause();
        }

        public void resume()
        {
            getEffect().Resume();
        }

        public Sound duplicate()
        {
            return new Sound(FilePath);
        }

    }
}
