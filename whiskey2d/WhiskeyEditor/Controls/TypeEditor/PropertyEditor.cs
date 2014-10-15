using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Whiskey2D.Core;
using WhiskeyEditor.Controls.ValueEditor;
using WhiskeyEditor.ClassLoader;

namespace WhiskeyEditor.Controls.TypeEditor
{
    public partial class PropertyEditor : UserControl
    {

        ValueEditor.ValueEditor editor;

        private PropertyDescriptor prop;
        public PropertyDescriptor Property { get { return getProperty(); } set { setProperty(value); } }

        public PropertyEditor(PropertyDescriptor property)
        {
            InitializeComponent();
            prop = property;

            typeBox.Items.Clear();
            typeBox.Items.Add(typeof(Int32));
            typeBox.Items.Add(typeof(Vector));
            typeBox.Items.Add(typeof(Sprite));
            typeBox.Items.Add(typeof(float));
            typeBox.SelectedItem = typeof(Single);
            Property = property;
        
        }
        private PropertyDescriptor getProperty()
        {

            prop.Name = nameBox.Text;
            prop.Type = (Type)typeBox.SelectedItem;
            prop.Value = editor.getValue();
            return prop;
            //return new PropertyDescriptor(nameBox.Text, (Type)typeBox.SelectedItem, editor.getValue());
        }

        private void setProperty(PropertyDescriptor prop)
        {
            this.prop = prop;

            nameBox.Text = prop.Name;
            if (!typeBox.Items.Contains(prop.Type))
            {
                typeBox.Items.Add(prop.Type);
            }
            typeBox.SelectedItem = prop.Type;
            //updateEditor();
            editor.setValue(prop.Value);

        }


        private void PropertyEditor_Load(object sender, EventArgs e)
        {
            //prop.Type = (Type)typeBox.SelectedItem;
           // updateEditor();
            //Controls.Add(editor);

        }

        private void updateEditor()
        {
            object oldVal = null;
            if (editor != null)
            {
                vPanel.Controls.Clear();
                oldVal = editor.getValue();
            }



            if (((Type)typeBox.SelectedItem) == typeof(Int32))
            {
                // Controls.Remove(valueEditor);
                editor = new IntEditor();
               
                // Controls.Add(valueEditor);
            }
            else if (((Type)typeBox.SelectedItem) == typeof(Single))
            {
                editor = new FloatEditor();
            }
            else
            {

                //instantiate default
                object val = Activator.CreateInstance(prop.Type);

                editor = new ObjectEditor(val);
                
                
            }

            if (oldVal != null && oldVal.GetType().Equals(prop.Type))
            {
               // editor.setValue(oldVal);
            }

            vPanel.Controls.Add(editor);
        }

        private void closeBt_Click(object sender, EventArgs e)
        {
            //this.Parent.Controls.Remove(this);
        }

        private void typeBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //prop.Type = (Type)typeBox.SelectedItem;
            updateEditor();
        }
    }
}
