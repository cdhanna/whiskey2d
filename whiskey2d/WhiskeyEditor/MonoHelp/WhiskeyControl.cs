
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

        GameManager gameMan = GameManager.getInstance<EditorGameManager>();
        ContentManager content;
        EditorInputSource inputSource;

        public WhiskeyControl()
        {
            
        }

        protected override void Initialize()
        {

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


        //protected override void OnHandleDestroyed(System.EventArgs e)
        //{
        //    gameMan.UnloadContent();
        //    base.OnHandleDestroyed(e);
        //}

        protected void update()
        {
         
            

            gameMan.Update(null); //todo fix nullgametime

            Invalidate();   //signals draw
        }

        protected override void Draw()
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            gameMan.Draw(null); //todo fix null gametime
        }


    }

}
