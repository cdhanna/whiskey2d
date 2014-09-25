using System;
using System.Diagnostics;
using System.Windows.Forms;
using WinFormsGraphicsDevice;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Whiskey2D.Core;


namespace WhiskeyEditor.MonoHelp
{

    /// <summary>
    /// A winforms control that interacts with a WhiskeyGame
    /// </summary>
    public class WhiskeyControl : GraphicsDeviceControl
    {

        Stopwatch timer;

        GameManager gameMan = GameManager.getInstance();
        ContentManager content;
        EditorInputSource inputSource;


        TimeSpan TargetElapsedTime;

        public WhiskeyControl()
        {
            
        }

        protected override void Initialize()
        {
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 8);
            content = new ContentManager(Services);

            gameMan.Initialize(content, GraphicsDevice);
            gameMan.LoadContent();

            inputSource = new EditorInputSource(this);

            InputSourceManager.getInstance().setRegularSource(inputSource);
            InputSourceManager.getInstance().requestRegular();
            

            // Start the animation timer.
            timer = Stopwatch.StartNew();

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { update(); };

            

        }



        protected void update()
        {

            if (timer.ElapsedMilliseconds > TargetElapsedTime.Milliseconds)
            {
                
                gameMan.Update(null); //todo fix nullgametime

              
                timer.Restart();
            }

            Invalidate();   //signals draw

        }

        protected override void Draw()
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            gameMan.Draw(null); //todo fix null gametime
        }


    }

}
