using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WhiskeyEditor.Backend.Managers;
//using Whiskey2D.Core;
using System.Drawing.Design;
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

        [STAThread]
        public static void Main()
        {
           //[TypeConverter (typeof(WhiskeyEditor.UI.Properties.Converters.LayerTypeConverter))]
    //[Editor (typeof(WhiskeyEditor.UI.Properties.LayerPicker), typeof(UITypeEditor))]//

            System.ComponentModel.TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Layer),
                new TypeConverterAttribute(typeof(WhiskeyEditor.UI.Properties.Converters.LayerTypeConverter)),
                new EditorAttribute(typeof(WhiskeyEditor.UI.Properties.LayerPicker), typeof(UITypeEditor)));

            System.ComponentModel.TypeDescriptor.AddAttributes(typeof(InstanceDescriptor), new TypeConverterAttribute(typeof(WhiskeyInstanceTypeConverter)));
            
            System.ComponentModel.TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Sprite), new TypeConverterAttribute(typeof(ExpandableObjectConverter)));
            
            System.ComponentModel.TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Color),
                new TypeConverterAttribute(typeof(StructTypeConverter<Whiskey2D.Core.Color>)),
                new EditorAttribute(typeof(ColorPicker), typeof(System.Drawing.Design.UITypeEditor)));

            System.ComponentModel.TypeDescriptor.AddAttributes(typeof(WhiskeyPropertyContainer),
                new EditorAttribute(typeof(WhiskeyPropertyContainerPicker), typeof(System.Drawing.Design.UITypeEditor)));

            System.ComponentModel.TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Vector), new TypeConverterAttribute(typeof(StructTypeConverter<Vector>)));


            try
            {
                string corePath = WhiskeyEditor.compile_types.CoreTypes.corePathTypes;
                   
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
