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



namespace WhiskeyEditor.UI.Properties
{
    class TypePicker : UITypeEditor
    {

       // private ListBox list;
        private TypePickerControl control;
        private IWindowsFormsEditorService service;
        private object selectedName;
        
        public TypePicker()
        {
            control = new TypePickerControl();
            control.PrimitiveBox.SelectedIndexChanged += clickedPrimitive;
            control.ObjectsBox.SelectedIndexChanged += clickedObject;
            //list = new ListBox();
            //foreach (string name in (WhiskeyEditor.Backend.TypeNameBank.Instance.getTypeDisplayNames()))
            //{
            //    list.Items.Add(name);
            //}

            //list.SelectedIndexChanged += (s, a) =>
            //{
            //    if (service != null)
            //    {
            //        service.CloseDropDown();
            //        service = null;
            //    }
            //};

        }

        private void clickedPrimitive(object sender, EventArgs args)
        {
            if (service == null) 
                return;

            string primName = (string)control.PrimitiveBox.SelectedItem;

            if (primName != null)
            {
                selectedName = primName;
            }

            service.CloseDropDown();

        }

        private void clickedObject(object sender, EventArgs args)
        {
            if (service == null)
                return;

            string objName = (string)control.ObjectsBox.SelectedItem;
            if (objName != null)
            {
                selectedName = objName;
            }

            service.CloseDropDown();

        }


        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
           service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            // show the list box
            if (service != null)
            {
                object oldSelected = selectedName;
                selectedName = value;
                control.updateBoxes();

                if (control.PrimitiveBox.Items.Contains(value))
                    control.PrimitiveBox.SelectedItem = value;
                if (control.ObjectsBox.Items.Contains(value))
                    control.ObjectsBox.SelectedItem = value;

                service.DropDownControl(control);
                //service.DropDownControl(list);
                if (selectedName != null )
                {
                    value = selectedName;
                }
                //if (list.SelectedItem != null)
                //    return list.SelectedItem;
            }

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
            //return base.GetEditStyle(context);
        }

    }
}
