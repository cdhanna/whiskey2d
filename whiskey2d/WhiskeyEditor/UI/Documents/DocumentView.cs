using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using WhiskeyEditor.UI.Dockable;
using WhiskeyEditor.UI.Assets;

namespace WhiskeyEditor.UI.Documents
{
    class DocumentView : Control
    {

        public TabControl Tabs {get; private set;}

        private Dictionary<string, DocumentTab> tabMap;

        public DocumentView()
        {
            tabMap = new Dictionary<string, DocumentTab>();

            BackColor = UIManager.Instance.DullFlairColor;
            Size = new Size(50, 50);

            

            initControls();
            configureControls();
            addControls();

        }

        public void focusDocument(string fileName)
        {
            if (tabMap.ContainsKey(fileName))
            {
                Tabs.SelectedTab = tabMap[fileName];
            }
        }

        public void openDocument(string fileName)
        {
            if (!tabMap.ContainsKey(fileName))
            {
                DocumentTab dt = UIManager.Instance.getDocumentTabFor(this, UIManager.Instance.Files.lookUp(fileName));
                //DocumentTab dt = new DocumentTab(title, this);
                tabMap.Add(fileName, dt);
                dt.open();
            }
            focusDocument(fileName);
            
        }

        public void saveCurrent()
        {
            DocumentTab tab = (DocumentTab) Tabs.SelectedTab;
            if (tab != null)
            {
                tab.save();
            }
        }

        public void closeDocument(string fileName)
        {

            if (tabMap.ContainsKey(fileName))
            {
                DocumentTab dt = tabMap[fileName];
                tabMap.Remove(fileName);
                dt.close();
                Tabs.TabPages.Remove(dt);
                refreshTabNumbers();
                

            }


        }

        public void refreshTabNumbers()
        {
            int num = 0;
            foreach (DocumentTab tab in tabMap.Values)
            {
                tab.TabNumber = num++;
            }
        }


        private void initControls()
        {
            Tabs = new TabControl();
            Tabs.ImageList = new ImageList();
            Tabs.ImageList.Images.Add(AssetManager.ICON_FILE);
            Tabs.ImageList.Images.Add(AssetManager.ICON_CLOSE);

            Tabs.Click += (s, a) =>
            {
                DocumentTab closed = null;
                foreach (DocumentTab tab in tabMap.Values)
                {
                    if (tab.mouseInIcon())
                    {
                        closed = tab;
                        break;
                    }
                }
                if (closed != null)
                {
                    closed.close();
                }

            };


           
        }

        private void configureControls()
        {

        }

        private void addControls()
        {
            Tabs.Dock = DockStyle.Fill;
            Controls.Add(Tabs);
        }


    }
}
