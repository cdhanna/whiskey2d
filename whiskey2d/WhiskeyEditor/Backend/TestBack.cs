using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WhiskeyEditor.Backend.Managers;
//using Whiskey2D.Core;
using System.CodeDom.Compiler;
using WhiskeyEditor.UI;
using System.Windows.Forms;
using System.Threading;
using System.Dynamic;
using WhiskeyEditor.UI.Properties;
using System.Drawing;
using System.ComponentModel;
using Whiskey2D.Core;
using WhiskeyEditor.UI.Properties.Converters;
using WhiskeyEditor.UI.Properties.TypeConverters;

namespace WhiskeyEditor.Backend
{
    class TestBack
    {

        
       

        class House
        {
            public System.Drawing.Color Color { get; set; }
        }

        [STAThread]
        public static void Main()
        {
            System.ComponentModel.TypeDescriptor.AddAttributes(typeof(InstanceDescriptor), new TypeConverterAttribute(typeof(WhiskeyInstanceTypeConverter)));
            
            System.ComponentModel.TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Sprite), new TypeConverterAttribute(typeof(ExpandableObjectConverter)));
            
            System.ComponentModel.TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Color),
                new TypeConverterAttribute(typeof(StructTypeConverter<Whiskey2D.Core.Color>)),
                new EditorAttribute(typeof(ColorPicker), typeof(System.Drawing.Design.UITypeEditor)));

            System.ComponentModel.TypeDescriptor.AddAttributes(typeof(WhiskeyPropertyContainer),
               // new TypeConverterAttribute(typeof(ExpandableObjectConverter)),
                new EditorAttribute(typeof(WhiskeyPropertyContainerPicker), typeof(System.Drawing.Design.UITypeEditor)));

            System.ComponentModel.TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Vector), new TypeConverterAttribute(typeof(StructTypeConverter<Vector>)));
           // System.ComponentModel.TypeDescriptor.AddAttributes(typeof(WhiskeyPropertyContainer), new TypeConverterAttribute(typeof(Whiskey)));
            //System.ComponentModel.TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Color), new TypeConverterAttribute(typeof(ColorTypeConverter)));

          //  Application.EnableVisualStyles();
          //  Application.SetCompatibleTextRenderingDefault(false);


          //  Form f = new Form();

          //  PropertyDescriptor pd = new PropertyDescriptor(false, "Var", new RealType(typeof(Whiskey2D.Core.Color), Whiskey2D.Core.Color.Red));
          //  PropertyDescriptor pd2 = new PropertyDescriptor(true, "Prim", new RealType(typeof(int), 32));
          //  PropertyDescriptor pd3 = new PropertyDescriptor("Boo", new RealType(typeof(Vector), Vector.One));

          // // TypeDescriptor t = new TypeDescriptor("GOBMAN");

          ////  PropertyDescriptor pd4 = new PropertyDescriptor("Inst", new InstanceType(t, new InstanceDescriptor(t)));


          //  //PropertyDescriptor pd2 = new PropertyDescriptor("Foo", new RealType(typeof(Vector), new Vector(32, 64)));
          //  List<PropertyDescriptor> pdList = new List<PropertyDescriptor>();
          //  pdList.Add(pd);
          //  pdList.Add(pd2);
          //  pdList.Add(pd3);
          ////  pdList.Add(pd4);

            

         
            
            
          //  PropertyDescriptorEditor pEditor = new PropertyDescriptorEditor();
          //  pEditor.PropertyList = pdList;
          //  //PropertyGrid pg = new PropertyGrid();
          //  //pg.PropertySort = PropertySort.NoSort;
          //  //pg.ToolbarVisible = false;
          //  //pg.PropertyValueChanged += (s, a) =>
          //  //{
          //  //    pg.Refresh();
          //  //};
          //  //PropertyAdapter ap = new PropertyAdapter(t.getPropertySetClone(), pg);


          //  //pg.SelectedObject = ap;

          //  pEditor.Dock = DockStyle.Fill;
          //  f.Controls.Add(pEditor);



          //  Application.Run(f);




            try
            {
                Project proj = ProjectManager.Instance.openProject(Settings.CurrentProject);
                ProjectManager.Instance.ActiveProject = proj;
            }
            catch (Exception e)
            {
                ProjectManager.Instance.ActiveProject = new NoProject();
            }
            UIManager.Instance.startup();

            return;
            
        }

        
    }
}
