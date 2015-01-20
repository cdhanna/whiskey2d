using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WhiskeyEditor.UI.Properties.Converters;
using System.Globalization;
using WhiskeyEditor.Backend;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.Design;
using Whiskey2D.Core;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.UI.Properties
{
    using XnaKeys = Microsoft.Xna.Framework.Input.Keys;

    class KeyTypeEditor : UITypeEditor
    {
        IWindowsFormsEditorService service;
        ListBox listBox;
        XnaKeys result;



        public KeyTypeEditor()
        {
            listBox = new ListBox();

            listBox.SelectedIndexChanged += (s, a) =>
            {
                if (service != null)
                {
                    object selected = listBox.SelectedItem;
                    if (selected != null)
                    {
                        result = (XnaKeys)selected;
                    }

                    //Console.WriteLine("KEY VALUE IS : " + result);

                    service.CloseDropDown();
                }
            };

        }

        public void popupateLayers()
        {
            object prevSelection = listBox.SelectedItem;
            
            listBox.Items.Clear();
            foreach (XnaKeys k in Enum.GetValues(typeof(XnaKeys)))
            {
                listBox.Items.Add(k);
            }
            //listBox.Items.Add(Keys.Can

            if (prevSelection != null && listBox.Items.Contains(prevSelection))
            {
                listBox.SelectedItem = prevSelection;
            }
            else listBox.SelectedItem = listBox.Items[0];

        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            // show the list box
            if (service != null)
            {
                XnaKeys k = (XnaKeys)value;

                result = k;
                popupateLayers();
                service.DropDownControl(listBox);

               

                value = result;
                return value;

            }
            else return XnaKeys.None;

           // return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (context != null)
            {
                return UITypeEditorEditStyle.DropDown;
            }
            return base.GetEditStyle(context);
        }

    }
}
