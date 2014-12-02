using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WhiskeyEditor.UI.Assets;

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
        protected NoArgFunction effect;

        public AbstractAction(string text)
            : this(text, null) { }

        public AbstractAction(string text, Image image)
            : this(text, image, new NoArgFunction(() => { }))
        {
            effect = new NoArgFunction(run);
        }

        public AbstractAction(string text, Image image, NoArgFunction effect)
        {
            this.text = text;
            this.image = image;
            this.effect = effect;
        }
        
     

        public string Name { get { return text; } }
        public Image Image { get { return image; } }
        public NoArgFunction Effect { get { return effect; } }

        /// <summary>
        /// The effect of the action, if no effect is specified
        /// </summary>
        protected abstract void run();

        /// <summary>
        /// Generate a button from the Action. The button's text, image, and click event
        /// will all be set to correspond to this Action.
        /// </summary>
        /// <returns>a new button created from the Action</returns>
        public Button generateButton()
        {
            Button button = new Button();
            button.Text = Name;
            button.Image = Image;
            button.Click += new EventHandler((s, a) => { Effect(); });
            return button;
        }

        public C generateControl<C>() 
        {
            C control;
            Type controlType = typeof(C);

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
            
            try
            {
                //System.Reflection.MethodInfo m = control.GetType().GetMethod("Invoke");
                //System.Reflection.MemberInfo[] mi = control.GetType().GetMember("Invoke");
                controlType.GetEvent("Click").AddEventHandler(control, new EventHandler((s, a) => {
                    //System.Reflection.MethodInfo[] ma = control.GetType().GetMethods();
                    //m = controlType.GetMethod("Invoke");
                    effect();
                    //controlType.GetMethod("Invoke").Invoke(control, new object[] { effect }); 
                }));
            }
            catch (Exception e)
            {
                throw new WhiskeyException("Cannot generate Control out of " + controlType + " because it doesn't have a Click Event");
            }
            return control;
        }
        private void writeValue(string propName, object src, object value)
        {
            try
            {
                src.GetType().GetProperty(propName).SetValue(src, value);
            }
            catch (Exception e)
            {
                throw new WhiskeyException("Cannot generate control from " + src.GetType() + " Because it doesnt have a " + propName + " Property");
            }
        }

    }
}
