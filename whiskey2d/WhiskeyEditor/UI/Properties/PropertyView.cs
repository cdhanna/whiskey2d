using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.UI.Dockable;
using WhiskeyEditor.UI;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Actions;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.UI.Properties.Editors;


namespace WhiskeyEditor.UI.Properties
{
    public class PropertyView : Control 
    {

       


        private PropertyContent pView;
        public PropertyContent PropertyContent
        {
            get { return pView; }
            private set { pView = value; }
        }

        private Label titleLabel;
        Panel panel = new Panel();
        protected ToolStrip ToolStrip { get; private set; }

       

        public PropertyView()
        {
            
            BackColor = Color.White;
            Size = new Size(150, 50);

           
            initControls();
            addControls();

            setPropertyContent(null);
        }

        public void clearPropertyContent()

        {
            if (IsHandleCreated)
            {
                Invoke(new NoArgFunction(() =>
                {
                    removePropertyContent();
                    ToolStrip.Items.Clear();
                    titleLabel.Text = "";
                }));
            }
            else
            {
                removePropertyContent();
                ToolStrip.Items.Clear();
                titleLabel.Text = "";
            }
            
        }

        private void removePropertyContent()
        {
            if (PropertyContent != null)
            {
                Controls.Remove(PropertyContent);
                PropertyContent = null;
            }
        }

        public void setPropertyContent(PropertyContent content)
        {

            

            if (PropertyContent != null && content.PropertyObject == PropertyContent.PropertyObject)
            {
                PropertyContent.Refresh();
                return;
            }

            if (IsHandleCreated)
            {
                Invoke(new NoArgFunction(() =>
                {
                    setPropertyContentInternal(content);
                }));
            }
            else
            {
                setPropertyContentInternal(content);
            }
            
        }

        private void setPropertyContentInternal(PropertyContent content)
        {
            if (content == null)
            {
                clearPropertyContent();
            }
            else
            {

                PropertyContent oldContent = PropertyContent;
                //removePropertyContent();
                pView = content;
                // PropertyContent = content;

                content.Dock = DockStyle.Fill;

                ToolStrip.Items.Clear();
                ToolStripItemCollection items = content.getToolStripCollection(ToolStrip);
                foreach (ToolStripItem item in items)
                {
                    ToolStrip.Items.Add(item);
                }
                panel.BackColor = UIManager.Instance.DullFlairColor;
                titleLabel.Text = content.Title;

                content.Padding = new Padding(0, panel.Height, 0, 0);

                this.SuspendLayout();
                Visible = false;

                Controls.Remove(oldContent);
                Controls.Remove(panel);
                Controls.Add(content);
                Controls.Add(panel);

                oldContent = null;

                Visible = true;
                this.ResumeLayout();
                Refresh();
            }
            
        }


        private void initControls()
        {
            titleLabel = new Label();
            titleLabel.Margin = new Padding(0);
            titleLabel.Padding = new Padding(2);
            titleLabel.ForeColor = Color.White;

            ToolStrip = new ToolStrip();
            ToolStrip.BackColor = UIManager.Instance.PaleFlairColor;
            ToolStrip.ImageList = new ImageList();
            ToolStrip.ImageList.Images.Add(Assets.AssetManager.ICON_PLUS);
            ToolStrip.ImageList.Images.Add(Assets.AssetManager.ICON_MINUS);


            panel.Size = new Size(150, 48);
            panel.BackColor = Color.White;
            panel.Dock = DockStyle.Top;
            panel.Controls.Add(titleLabel);

            ToolStrip.Dock = DockStyle.Bottom;
            panel.Controls.Add(ToolStrip);



            
            //titleLabel.Font = new Font(titleLabel.Font.FontFamily, titleLabel.Font.Size, FontStyle.Regular);
        }

        private void addControls()
        {
            
            

            Controls.Add(panel);

        }

    }
}
