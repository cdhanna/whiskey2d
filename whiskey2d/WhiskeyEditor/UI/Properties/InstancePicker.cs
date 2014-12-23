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
    class InstancePicker : UITypeEditor
    {

        InstancePickerControl control;
        IWindowsFormsEditorService service;

        public InstancePicker()
        {
            control = new InstancePickerControl();
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            // show the list box
            if (service != null && value is Sprite)
            {
                Sprite sprite = (Sprite)value;
               // result = null;
               // setArtBox();
                service.DropDownControl(control);

                //if (result != null)
                //{
                //    sprite.ImagePath = result;
                //}



            }

            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal ;
            //return base.GetEditStyle(context);
        }

    }
}
