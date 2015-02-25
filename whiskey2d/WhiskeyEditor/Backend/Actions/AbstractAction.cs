using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.Backend.Managers;
using System.Reflection;

namespace WhiskeyEditor.Backend.Actions
{
    /// <summary>
    /// The AbstractAction holds the default data structure to comply with the Action interface.
    /// All other actions may extend from AbstractAction to comply with the Action interface
    /// </summary>
    abstract class AbstractAction : WhiskeyAction
    {

        

        protected string text;
        protected Image image;
        protected OneArgFunction effect;
        private float progress;

        public AbstractAction(string text)
            : this(text, null) { }

        public AbstractAction(string text, Image image)
            : this(text, image, new OneArgFunction((x) => { }))
        {
            
            effect = new OneArgFunction(runSrc );
        }

        public AbstractAction(string text, Image image, OneArgFunction effect)
        {
            this.text = text;
            this.image = image;
            this.effect = effect;
            this.progress = 0;
        }

        public event ActionChangedEventHandler Changed = new ActionChangedEventHandler((s, a) => { });

        protected event EventHandler ActionStarted = new EventHandler((s, a) => { });
        protected event EventHandler ActionEnded = new EventHandler((s, a) => { });

        protected void startAction()
        {
            ActionStarted(this, new EventArgs());
        }

        protected void endAction()
        {
            ActionEnded(this, new EventArgs());
        }

        public string Name { get { return text; } set { text = value; } }
        public Image Image { get { return image; } set { image = value; } }
        public float Progress { get { return progress; } set { progress = value; Changed(this, new ActionChangedEventArgs(this)); } }
        public OneArgFunction Effect { get { return effect; } }

        /// <summary>
        /// The effect of the action, if no effect is specified
        /// </summary>
        protected abstract void run();

        protected virtual void runSrc(Object src)
        {
            startAction();
            run();
            endAction();
        }

        protected void disableSrc(object src)
        {
            if (src is ToolStripButton)
            {
                ToolStripButton b = (ToolStripButton)src;
                
                b.Owner.Invoke(new NoArgFunction(() => {
                    b.Enabled = false;
                }));
            }
        }

        protected void enableSrc(object src)
        {
            if (src is ToolStripButton)
            {
                ToolStripButton b = (ToolStripButton)src;

                if (b.Owner != null)
                {
                    b.Owner.Invoke(new NoArgFunction(() =>
                        {
                            b.Enabled = true;
                        }));
                }
            }
        }
        
        public C generateControl<C>() 
        {
            C control;
            Type controlType = typeof(C);
            ToolStripButton b;
            

            if (controlType == typeof(ToolStripDropDownButton))
            {
                ToolStripDropDownButton btn = new ToolStripDropDownButton(Name, Image, (s, a) => { ActionManager.Instance.run(this); });
                setupDropDown(btn);
            
                
                C btn2 = (C)Convert.ChangeType(btn, typeof(C));
                return btn2;
            }

            try
            {
                control = (C) controlType.GetConstructor(new Type[] { }).Invoke(new object[] { });

            }
            catch (Exception e)
            {
                throw new WhiskeyException("Cannot generate Control out of " + controlType + " Because it doesn't have a no-arg constructor");
            }
            writeValue("Text", control, Name);
            writeValue("Image", control, Image);
            //writeValue("Enabled", control, false);
            try
            {
                controlType.GetEvent("Click").AddEventHandler(control, new EventHandler((s, a) => {
                    ActionManager.Instance.run(this, control);
                }));

                ActionStarted += (s, a) =>
                {
                    disableSrc(control);
                };
                ActionEnded += (s, a) =>
                {
                    enableSrc(control);
                };

            }
            catch (Exception e)
            {
                throw new WhiskeyException("Cannot generate Control out of " + controlType + " because it doesn't have a Click Event");
            }

            //if (typeof(C).Equals(typeof(ToolStripDropDownButton)))
            //{

            //    ToolStripDropDownButton btn = (ToolStripDropDownButton)Convert.ChangeType(control, typeof(ToolStripDropDownButton));
            //    setupDropDown(btn);
            //}


            return control;
        }
        private void writeValue(string propName, object src, object value)
        {
            PropertyInfo prop;
            
            try
            {
                prop = src.GetType().GetProperty(propName);
            }
            catch (Exception e)
            {
                throw new WhiskeyException("Cannot generate control from " + src.GetType() + " Because it doesnt have a " + propName + " Property");
            }
            
            
            try
            {

                
                prop.SetValue(src, value);
                
            }
            catch (Exception e)
            {

                PropertyInfo[] p = src.GetType().GetProperties();

                throw new WhiskeyException("Cannot generate control from " + src.GetType() + " Because it doesnt have a " + propName + " Property");
            }
            
        }
        protected virtual void setupDropDown(ToolStripDropDownButton btn)
        {
        }
    }
}
