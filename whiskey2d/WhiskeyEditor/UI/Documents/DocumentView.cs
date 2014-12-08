using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.UI.Dockable;
using WhiskeyEditor.UI.Assets;

namespace WhiskeyEditor.UI.Documents
{
    class DocumentView : Control
    {

        public TabControl Tabs {get; private set;}

        private Dictionary<string, DocumentTab> tabMap;


        public event PropertyChangeRequestEventHandler PropertyChangeRequested = new PropertyChangeRequestEventHandler( (s, a) => {});

        public DocumentView()
        {
            tabMap = new Dictionary<string, DocumentTab>();

            BackColor = UIManager.Instance.DullFlairColor;
            Size = new Size(50, 50);
            ProjectManager.Instance.ProjectChanged += (s, a) =>
            {
                closeAll();
            };

            initControls();
            configureControls();
            addControls();

        }

        public void focusDocument(string fileName)
        {
            if (tabMap.ContainsKey(fileName))
            {
                Tabs.SelectedTab = tabMap[fileName];
                Tabs.SelectedTab.Refresh();
                
            }
        }

        public void openDocument(string fileName)
        {
            if (!tabMap.ContainsKey(fileName))
            {
                DocumentTab dt = UIManager.Instance.getDocumentTabFor(this, UIManager.Instance.Files.lookUp(fileName));
                //DocumentTab dt = new DocumentTab(title, this);
                tabMap.Add(fileName, dt);

                dt.PropertyChangeRequested += (s, a) =>
                {
                    PropertyChangeRequested(s, a);
                };

                dt.open();
                dt.Refresh();
              //  dt.Font = new Font(Font, FontStyle.Bold);
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

        public void closeAll()
        {
            while (tabMap.Keys.Count > 0)
            {
                closeDocument(tabMap.Keys.ElementAt(0));
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
            Tabs.SizeMode = TabSizeMode.Normal;
            
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

            Tabs.SelectedIndexChanged += (s, a) =>
            {
                if (Tabs.SelectedTab != null)
                    Tabs.SelectedTab.Refresh();
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
