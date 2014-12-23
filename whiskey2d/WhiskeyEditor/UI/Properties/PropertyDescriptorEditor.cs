using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend;
using System.Threading;

namespace WhiskeyEditor.UI.Properties
{
    class PropertyDescriptorListEditor : Control
    {

        public PropertyGrid PropertyGrid { get; private set; }
        public PropertyAdapter PropertyAdapter { get; private set; }

        private List<GeneralPropertyDescriptor> OtherProperties { get; set; }

        private List<PropertyDescriptor> props;

        private Thread updateThread;
        private bool updateThreadKey;

        public List<PropertyDescriptor> PropertyList
        {
            get
            {
                return props;
            }
            set
            {

                OtherProperties = null;
                if (PropertyAdapter != null)
                    OtherProperties = PropertyAdapter.OtherProperties;
                PropertyAdapter = new PropertyAdapter(value, PropertyGrid);
                
                if (OtherProperties != null)
                {
      
                    OtherProperties.ForEach((p) =>
                    {
                        PropertyAdapter.OtherProperties.Add(p);
                    });
                }
                PropertyGrid.SelectedObject = PropertyAdapter;
                props = value;
                
                //if (this.IsHandleCreated)
                //    Invoke(new NoArgFunction(() => {
                //        Refresh();
                //    }));
                
            }
        }

        public override void Refresh()
        {
            if (IsHandleCreated)
                Invoke(new NoArgFunction(() =>
                {
                    PropertyGrid.Refresh();
                    base.Refresh();
                }));
            
        }

        public PropertyDescriptorListEditor()
        {
            initControls();
            OtherProperties = new List<GeneralPropertyDescriptor>();
            PropertyList = new List<PropertyDescriptor>();
            addControls();
            
            //updateThreadKey = true;
            //updateThread = new Thread(() =>
            //{
            //    //Invoke( new NoArgFunction(updateLoop ));
            //    while (updateThreadKey)
            //    {
            //        if (!IsDisposed && IsHandleCreated)
            //            Invoke(new NoArgFunction(() =>
            //            {
            //                if (!IsDisposed)
            //                    PropertyGrid.Refresh();
            //            }));
            //        Thread.Sleep(2);
            //    }
            //});
            //updateThread.Name = "PropertyGridUpdateThread";
            

            //UIManager.Instance.Closing += (s, a) =>
            //{
            //    updateThreadKey = false;
            //};

        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            
        }

       

        protected override void Dispose(bool disposing)
        {
            updateThreadKey = false;
            base.Dispose(disposing);
        }

        public GeneralPropertyDescriptor addOtherProperty(string name, string category, object value)
        {
            GeneralPropertyDescriptor p = new GeneralPropertyDescriptor(name);
            p.PropCategory = category;
            p.PropValue = value;
            PropertyAdapter.OtherProperties.Add(p);
            return p;
        }
        public void removeOtherProperty(GeneralPropertyDescriptor p)
        {
            PropertyAdapter.OtherProperties.Remove(p);
        }


        private void initControls()
        {
            PropertyGrid = new PropertyGrid();
            PropertyGrid.PropertySort = PropertySort.Categorized;
            //PropertyGrid.Cate
            PropertyGrid.ToolbarVisible = false;
            PropertyGrid.PropertyValueChanged += (s, a) =>
            {
                PropertyGrid.Refresh();
            };
            PropertyGrid.CategoryForeColor = UIManager.Instance.DullFlairColor;
            PropertyGrid.CategorySplitterColor = UIManager.Instance.PaleFlairColor;
            BackColor = UIManager.Instance.FlairColor;
        }


        private void addControls()
        {
            PropertyGrid.Dock = DockStyle.Fill;
            Controls.Add(PropertyGrid);
        }

    }
}
