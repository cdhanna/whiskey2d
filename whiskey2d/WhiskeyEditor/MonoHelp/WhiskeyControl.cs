using System;
using System.Diagnostics;
using System.Windows.Forms;
using WinFormsGraphicsDevice;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers.Impl;
using Whiskey2D.Core.Inputs;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Drawing.Design;
using WhiskeyEditor.Backend.Managers;
using Whiskey2D.PourGames.Game3;
using WhiskeyEditor.Backend;

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

        EditorRenderManager renderer;

        TimeSpan TargetElapsedTime;


        GameObject selectedGob;
        EditorObjects.ObjectController oc;

        private Level level;
        public Level Level
        {
            get { return level; }
            set
            {
                level = value;
                if (oc != null)
                    oc.CurrentLevel = level;
            }
        }
      //  public WhiskeyPropertyGrid GobGrid{get;set;}
      //  public ScriptCollection GobScriptCollection { get; set; }

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
                new DefaultObjectManager(),
                DefaultRenderManager.getInstance(),
                DefaultResourceManager.getInstance()
                );
            gameMan.CurrentScene = null;
            //gameMan.LoadContent();

            inputSource = new EditorInputSource(this);

            DefaultInputSourceManager.getInstance().setRegularSource(inputSource);
            DefaultInputSourceManager.getInstance().requestRegular();


            renderer = new EditorRenderManager();
            renderer.init(GraphicsDevice);

            // Start the animation timer.
            timer = Stopwatch.StartNew();

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { update(); };

            //add editor objects
            oc = new EditorObjects.ObjectController();
            oc.CurrentLevel = Level;
            //TypeDescriptor.AddAttributes(   typeof(Whiskey2D.Core.Vector),
            //                                new EditorAttribute(typeof(VectorEditor),
            //                                typeof(UITypeEditor)));
            ////TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Vector),
            //                                new TypeConverterAttribute(typeof(ExpandableObjectConverter)));



            //GameManager.Objects.getAllObjectsNotOfType<EditorObjects.EditorGameObject>().ForEach((g) => { g.close(); });

            //new Whiskey2D.PourGames.Game3.Game3Launch().start();
            
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
            GraphicsDevice.Clear(Whiskey2D.Core.Color.CornflowerBlue);
            gameMan.Draw(null);
            if (Level != null)
            {
                renderer.render(Level.Descriptors);
            }
            else
            {
                GraphicsDevice.Clear(Whiskey2D.Core.Color.Red);

            }
            //gameMan.Draw(null); //todo fix null gametime
        }




        //public void addNewGameObject(Type gameObjectType, int x, int y)
        //{
        //    GameObject gob = (GameObject) gameObjectType.GetConstructor(new Type[] { }).Invoke(new object[] { });
        //    gob.Position = new Vector2(x, y);

        //}

        //public void save(string stateName)
        //{
        //    State state = GameManager.Objects.getState();
        //    state.Name = stateName;
        //    ProjectManager.Instance.ActiveProject.saveState(state);
        //    //State.serialize(state, "game-state.txt");
        //}

        //public void load()
        //{
        //    State state = State.deserialize("game-state.txt");
        //    GameManager.Objects.setState(state);
        //}

        GameObject GameController.SelectedGob
        {
            get
            {
                return selectedGob;
            }
            set
            {
                selectedGob = value;
                //GobGrid.SelectedObject = value;
                //GobGrid.Refresh();

                //GobScriptCollection.SelectedObject = value;
                //GobScriptCollection.Refresh();

            }
        }

        //public GameObject SelectedGob
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }

}
