using System;
using System.Diagnostics;
using System.Windows.Forms;
using WinFormsGraphicsDevice;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers.Impl;
using Whiskey2D.Core.Inputs;
using WhiskeyEditor.Controls;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Drawing.Design;

using Whiskey2D.PourGames.Game3;
namespace WhiskeyEditor.MonoHelp
{

    /// <summary>
    /// A winforms control that interacts with a WhiskeyGame
    /// </summary>
    public class WhiskeyControl : GraphicsDeviceControl, GameController
    {

        Stopwatch timer;

        GameManager gameMan = GameManager.getInstance();
        ContentManager content;
        EditorInputSource inputSource;


        TimeSpan TargetElapsedTime;


        GameObject selectedGob;
        public WhiskeyPropertyGrid GobGrid{get;set;}
        public ScriptCollection GobScriptCollection { get; set; }

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //}

        protected override void Initialize()
        {
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 8);
            content = new ContentManager(Services);

            gameMan.Initialize(this, content, GraphicsDevice,
                DefaultInputManager.getInstance(),
                DefaultInputSourceManager.getInstance(),
                DefaultLogManager.getInstance(),
                new EditorObjectManager(),
                DefaultRenderManager.getInstance(),
                DefaultResourceManager.getInstance()
                );
            gameMan.LoadContent();

            inputSource = new EditorInputSource(this);

            DefaultInputSourceManager.getInstance().setRegularSource(inputSource);
            DefaultInputSourceManager.getInstance().requestRegular();
            

            

            // Start the animation timer.
            timer = Stopwatch.StartNew();

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { update(); };

            //add editor objects
            new EditorObjects.ObjectController();

            //TypeDescriptor.AddAttributes(   typeof(Whiskey2D.Core.Vector),
            //                                new EditorAttribute(typeof(VectorEditor),
            //                                typeof(UITypeEditor)));
            ////TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Vector),
            //                                new TypeConverterAttribute(typeof(ExpandableObjectConverter)));



            GameManager.Objects.getAllObjectsNotOfType<EditorObjects.EditorGameObject>().ForEach((g) => { g.close(); });

            //new Whiskey2D.PourGames.Game3.Game3Launch().start();
            
        }

    

        protected void update()
        {

            if (timer.ElapsedMilliseconds > TargetElapsedTime.Milliseconds)
            {
                
                gameMan.Update(null); //todo fix nullgametime
                ProxyDomainManager.Instance.update();
              
                timer.Restart();
            }

            Invalidate();   //signals draw

        }

        protected override void Draw()
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            gameMan.Draw(null); //todo fix null gametime
        }




        public void addNewGameObject(Type gameObjectType, int x, int y)
        {
            GameObject gob = (GameObject) gameObjectType.GetConstructor(new Type[] { }).Invoke(new object[] { });
            gob.Position = new Vector2(x, y);

        }

        public void save(string stateName)
        {
            State state = GameManager.Objects.getState();
            state.Name = stateName;
            Project.ProjectManager.Instance.ActiveProject.saveState(state);
            //State.serialize(state, "game-state.txt");
        }

        public void load()
        {
            State state = State.deserialize("game-state.txt");
            GameManager.Objects.setState(state);
        }

        GameObject GameController.SelectedGob
        {
            get
            {
                return selectedGob;
            }
            set
            {
                selectedGob = value;
                GobGrid.SelectedObject = value;
                GobGrid.Refresh();

                GobScriptCollection.SelectedObject = value;
                GobScriptCollection.Refresh();

            }
        }
    }

}
