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

        private ListBox list;
        private IWindowsFormsEditorService service;

        public TypePicker()
        {
            list = new ListBox();
            foreach (string name in (WhiskeyEditor.Backend.TypeNameBank.Instance.getTypeDisplayNames()))
            {
                list.Items.Add(name);
            }

            list.SelectedIndexChanged += (s, a) =>
            {
                if (service != null)
                {
                    service.CloseDropDown();
                    service = null;
                }
            };

        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
           service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            // show the list box
            if (service != null)
            {
                service.DropDownControl(list);

                if (list.SelectedItem != null)
                    return list.SelectedItem;
            }

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
            //return base.GetEditStyle(context);
        }

    }
}
