using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Properties;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend;
using WhiskeyEditor.UI.Properties.Dictionary;
using System.Dynamic;

namespace WhiskeyEditor.UI.Properties.Editors
{
   
    class FileDescriptorPropertyEditor<F> : FileDescriptorPropertyEditor where F : FileDescriptor
    {
        public FileDescriptorPropertyEditor(F descriptor)
            : base(descriptor)
        {
            Descriptor = descriptor;
        }

        protected new F Descriptor { get; private set; }

    }

    class FileDescriptorPropertyEditor : PropertyContent
    {

        protected PropertyDescriptorListEditor WhiskeyPropertyListGrid { get; private set; }

        protected FileDescriptor Descriptor { get; private set; }

        

        public FileDescriptorPropertyEditor(FileDescriptor fileDesc) : base(fileDesc)
        {
            Title = "File Properties ";
            Descriptor = fileDesc;
            initControls();
            addControls();
        }


        private void initControls()
        {
            WhiskeyPropertyListGrid = new PropertyDescriptorListEditor();
            List<PropertyDescriptor> pList = new List<PropertyDescriptor>();
            WhiskeyPropertyListGrid.PropertyList = pList;

            string projPath = WhiskeyEditor.Backend.Managers.ProjectManager.Instance.ActiveProject.PathBase.ToLower();

            WhiskeyPropertyListGrid.addOtherProperty("Name", "\tFile Properties", Descriptor.Name).PropIsReadOnly = true;
            WhiskeyPropertyListGrid.addOtherProperty("Path", "\tFile Properties", Descriptor.FilePath.Replace(projPath, "")).PropIsReadOnly = true;
            //Adapter = new PropertyAdapter(pList, PropertyGrid);

            

            //PropertyGrid.SelectedObject = Adapter;
        }

        private void addControls()
        {
            WhiskeyPropertyListGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(WhiskeyPropertyListGrid);
        }

    }
}
