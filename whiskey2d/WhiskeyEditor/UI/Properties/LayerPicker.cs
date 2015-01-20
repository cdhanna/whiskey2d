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
    class LayerPicker : UITypeEditor
    {

        IWindowsFormsEditorService service;
        ListBox listBox;
        string result;



        public LayerPicker()
        {
            listBox = new ListBox();
            //comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            //comboBox.SelectedIndexChanged += (s, a) =>
            //{
            //    if (service != null)
            //    {
                    
            //    }
            //};
            listBox.SelectedIndexChanged += (s, a) =>
            {
                if (service != null)
                {
                    object selected = listBox.SelectedItem;
                    if (selected != null)
                    {
                        result = (string)selected;
                    }
                    service.CloseDropDown();
                }
            };

        }

        public void popupateLayers()
        {
            object prevSelection = listBox.SelectedItem;
            
            listBox.Items.Clear();
            foreach (string name in WhiskeyEditor.MonoHelp.WhiskeyControl.Active.Level.Layers.Select(l => l.Name))
            {
                listBox.Items.Add(name);

            }

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
            if (service != null && value is Layer)
            {
                Layer layer = (Layer)value;
                // result = null;
                // setArtBox();
                result = layer.Name;
                popupateLayers();
                service.DropDownControl(listBox);

                Layer newLayer = WhiskeyEditor.MonoHelp.WhiskeyControl.Active.Level.Layers.Find(l => l.Name.Equals(result));
                value = newLayer;
                

            }

            return base.EditValue(context, provider, value);
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
