using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Whiskey2D.Core.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.Core
{
    /// <summary>
    /// An Animation holds data about how to play a certain segment of frames in a Sprite
    /// An example of an Animation being used in a Script may look like
    /// 
    /// Animation a = Gob.Sprite.createAnimation(0, 12);
    /// 
    /// public override void onUpdate(){
    ///     a.advanceFrame();
    /// }
    /// 
    /// 
    /// </summary>
    [Serializable]
    public class Animation
    {

        private Sprite sprite;

        private int ticks = 0;

        private int currentFrame, startFrame, endFrame;

        /// <summary>
        /// Create an Animation from a Sprite
        /// </summary>
        /// <param name="sprite">Some sprite</param>
        public Animation(Sprite sprite)
        {
            if (sprite == null) throw new ArgumentNullException("sprite");

            this.sprite = sprite;
        }

        /// <summary>
        /// Get or Set the current frame of the Animation
        /// </summary>
        public int CurrentFrame
        {
            get
            {
                return currentFrame;
            }
            set
            {
                currentFrame = MathHelper.Clamp(value, 0, sprite.FrameCount);
            }
        }

        /// <summary>
        /// Get or Set the start frame of the Animation
        /// </summary>
        public int StartFrame
        {
            get
            {
                return startFrame;
            }
            set
            {
                startFrame = MathHelper.Clamp(value, 0, sprite.FrameCount - 1);
            }
        }

        /// <summary>
        /// Get or Set the end frame of the Animation
        /// </summary>
        public int EndFrame
        {
            get
            {
                return endFrame;
            }
            set
            {
                endFrame = MathHelper.Clamp(value, 0, sprite.FrameCount - 1);
            }
        }

        /// <summary>
        /// Get or set the Speed of the Animation. The lower the speed, the fast the animation will play
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Get or set the automatic loopness of the Animation
        /// </summary>
        public bool Looped { get; set; }

        /// <summary>
        /// Advance the frame if enough time has gone by. Call this to use the Animation
        /// </summary>
        public void advanceFrame()
        {


            if (sprite.ActiveAnimation != this)
            {
                sprite.ActiveAnimation = this;
                ticks = Speed;
            }

            ticks++;
            if (ticks >= Speed)
            {
                ticks = 0;


                int direction = Math.Sign(EndFrame - StartFrame);

                CurrentFrame += direction;


                if (CurrentFrame > EndFrame && direction == 1)
                {
                    if (Looped)
                        CurrentFrame = StartFrame;
                    else CurrentFrame = EndFrame;
                }
                if (CurrentFrame < StartFrame && direction == -1)
                {
                    if (Looped)
                        CurrentFrame = EndFrame;
                    else CurrentFrame = StartFrame;
                }


                sprite.Frame = CurrentFrame;


            }

        }
    }
}
