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
using System.ComponentModel;


namespace WhiskeyEditor.UI.Properties
{
    class BoolPicker : UITypeEditor
    {


        private ListBox list;
        private IWindowsFormsEditorService service;


        public BoolPicker()
        {
            list = new ListBox();
            list.Items.Add("true");
            list.Items.Add("false");
            list.Size = new Size(40, 40);
            list.SelectedIndexChanged += (s, a) =>
            {
                if (service != null)
                {
                    service.CloseDropDown();
                    service = null;
                }
            };

        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            // show the list box
            if (service != null)
            {
                service.DropDownControl(list);

                if (list.SelectedItem != null)
                {
                    if (list.SelectedItem.Equals("true"))
                    {
                        return true;
                    }
                    else return false;
                }
            }

            return value;
            
        }

    }
}
