using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.UI.Library;
using WhiskeyEditor.UI.Documents.Actions;
using Whiskey2D.Core;
using WhiskeyEditor.UI.Properties.Converters;
using WhiskeyEditor.UI.Properties;
namespace WhiskeyEditor.UI.Properties.Editors
{
    class InstanceDescriptorPropertyEditor : PropertyEditor
    {

        protected PropertyDescriptorListEditor PropertyGrid { get; private set; }
        protected PropertyAdapter Adapter { get; private set; }
        protected InstanceDescriptor Descriptor { get; private set; }


        protected List<GeneralPropertyDescriptor> ScriptProperties;

        

        protected EditScriptsAction editScriptsAction;
        protected DeleteInstanceAction deleteAction;

        /// <summary>
        /// A handler for drag drop
        /// </summary>
        private DragEventHandler dragEnterHandler;
        private DragEventHandler dragDropHandler;


        public InstanceDescriptorPropertyEditor(InstanceDescriptor desc) : base(desc)
        {
            Title = "Instance Properties";
            Descriptor = desc;
            ScriptProperties = new List<GeneralPropertyDescriptor>();
            initControls();
            addControls();
            AllowDrop = true;
            dragEnterHandler = new DragEventHandler(dragEnter);
            dragDropHandler = new DragEventHandler(dragDrop);
            DragDrop += dragDropHandler;
            DragEnter += dragEnterHandler;
        }

        protected override void Dispose(bool disposing)
        {
            DragDrop -= dragDropHandler;
            DragEnter -= dragEnterHandler;
            base.Dispose(disposing);
        }


      

        public void refreshScripts()
        {
            ScriptProperties.ForEach((s) => { PropertyGrid.removeOtherProperty(s); });
            ScriptProperties.Clear();
            Descriptor.getScriptNames().ForEach((s) =>
            {
                InstanceScriptRhapper dumb = new InstanceScriptRhapper();
                dumb.Descriptor = Descriptor;
                dumb.ScriptName = s;
                GeneralPropertyDescriptor gpd = PropertyGrid.addOtherProperty(s, "Scripts", dumb);
                gpd.CustomTypeEditor = WhiskeyTypeEditors.lookUp("InstanceScriptRhapper");
               
                //gpd.PropIsReadOnly = true;
                ScriptProperties.Add(gpd);
            });
        }

        public override void Refresh()
        {
            refreshScripts();
            

            PropertyGrid.Refresh();
            base.Refresh();
        }

        private void initControls()
        {
            


            PropertyGrid = new PropertyDescriptorListEditor();
            List<PropertyDescriptor> pList = Descriptor.getPropertySet();
            PropertyGrid.PropertyList = pList;


            

           
       

            PropertyGrid.addOtherProperty("Type", "\tBasic", Descriptor.TypeDescriptorInFileManager.Name).PropIsReadOnly = true;

            PropertyGrid.PropertyAdapter.PropertyValueChanged += (s, a) =>
            {

            };

            editScriptsAction = new AddRemoveInstanceScriptsAction(Descriptor, this);
            ToolStripItems.Add(editScriptsAction.generateControl());
            //ToolStripContentPanel

            deleteAction = new DeleteInstanceAction(Descriptor);
            ToolStripItems.Add(deleteAction.generateControl<ToolStripButton>());
          


            refreshScripts();
        }

        private void addControls()
        {
            PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(PropertyGrid);
        }


        private void dragEnter(object sender, DragEventArgs args)
        {
            if (args.Data.GetData(typeof(LibraryTreeNode)) is LibraryTreeNode)
            {
                LibraryTreeNode node = (LibraryTreeNode)args.Data.GetData(typeof(LibraryTreeNode));



                FileDescriptor fDesc = FileManager.Instance.lookUp(node.FilePath);
                if (fDesc != null && fDesc is ScriptDescriptor)
                {

                    ScriptDescriptor sDesc = (ScriptDescriptor)fDesc;
                    if (sDesc.TargetTypeName.Equals(Descriptor.TypeDescriptorInFileManager.Name))
                    {
                        args.Effect = DragDropEffects.All;
                    }
                }
            }

            
        }
        private void dragDrop(object sender, DragEventArgs args)
        {

            LibraryTreeNode node = (LibraryTreeNode)args.Data.GetData(typeof(LibraryTreeNode));
            FileDescriptor fDesc = FileManager.Instance.lookUp(node.FilePath);
            if (fDesc is ScriptDescriptor)
            {

                ScriptDescriptor sDesc = (ScriptDescriptor)fDesc;
                if (sDesc.TargetTypeName.Equals(Descriptor.TypeDescriptorInFileManager.Name))
                {


                    Descriptor.addScript(sDesc.Name);

                    Refresh();
                }
            }

            //LibraryTreeNode node = (LibraryTreeNode)args.Data.GetData(typeof(LibraryTreeNode));


            //FileDescriptor fDesc = FileManager.Instance.lookUp(node.FilePath);
            //if (fDesc is TypeDescriptor)
            //{
            //    TypeDescriptor tDesc = (TypeDescriptor)fDesc;

            //    InstanceDescriptor inst = new InstanceDescriptor(tDesc, Descriptor.Level);
            //    inst.Sprite = new Sprite(WhiskeyControl.Renderer, inst.Sprite);


            //    //inst.Sprite.Scale *= 50;
            //    Point p = PointToClient(new Point(args.X, args.Y - ToolStrip.Height));

            //    inst.Position = new Vector(p.X, p.Y) - inst.Bounds.Size / 2;

            //    inst.X = inst.Position.X;
            //    inst.Y = inst.Position.Y;

            //    //Descriptor.Level.Descriptors.Add(inst);
            //    Dirty = true;
            //}


            // save(new DefaultProgressNotifier());

        }


    }
}
