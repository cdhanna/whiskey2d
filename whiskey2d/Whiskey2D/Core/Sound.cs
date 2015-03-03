using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Whiskey2D.Core.Managers;
using IrrKlang;

namespace Whiskey2D.Core
{

    [Serializable]
    public class Sound 
    {

        private static ISoundEngine engine = new ISoundEngine();
        
        //public SoundState State
        //{
        //    get
        //    {
        //        return getEffect().State;
        //    }
        //}

        public bool Looped
        {
            get
            {
                return getEffect().Looped;
            }
            set
            {
                getEffect().Looped = value;
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
                getEffect().Volume = value;
            }
        }

        public float Pan
        {
            get
            {
                return getEffect().Pan * 2;
            }
            set
            {
                
                 float pan = MathHelper.Clamp(value, -1, 1);
                 getEffect().Pan = pan / 2f;
            }
        }

        //public float Pitch
        //{
        //    get
        //    {
        //        return getEffect().Pitch;
        //    }
        //    set
        //    {
        //        getEffect().Pitch = value;
        //    }
        //}

        public string FilePath { get; private set; }

        //[NonSerialized]
        //private ISoundSource effect;

        [NonSerialized]
        private ISound sound;

        public Sound(Sound other)
            : this(other.FilePath, false)
        {

        }

        public Sound(string filePath) 
            : this(filePath, false)
        {
            
        }

       
        public Sound(string filePath, bool looped)
        {
            FilePath =  filePath;
           
        }


        private ISound getEffect()
        {
            if (sound == null)
            {
                //ISoundSource src = engine.AddSoundSourceFromFile("media/" + FilePath);

                sound = engine.Play2D("media/"+FilePath, false, true);
                
            }
            
            //sound.SoundEffectControl.EnableEchoSoundEffect()
            return sound;
        }

        public Sound setPan(float x)
        {
            Pan = x;
            return this;
        }
        public Sound setVolume(float x)
        {
            Volume = x;
            return this;
        }

        public Sound setLooped(bool x)
        {
            Looped = x;
            return this;
        }

        public Sound play()
        {

            if (getEffect().Finished)
            {
                sound = engine.Play2D("media/" + FilePath, false, false);
            }
            else
            {
                getEffect().Paused = false;
            }

            return this;
        }

        public Sound stop()
        {
            getEffect().Stop();
            return this;
        }

        public Sound pause()
        {
            getEffect().Paused = true;
            return this;
        }

        public Sound resume()
        {
            getEffect().Paused = false;
            return this;
        }

        public Sound duplicate()
        {
            return new Sound(this);
        }

    }
}
