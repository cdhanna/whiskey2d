using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.TextEditor.Gui;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using System.Drawing;
using System.Windows.Forms;



namespace WhiskeyEditor.UI.Documents
{
    class CodeProvider : ICompletionDataProvider
    {

        private ImageList images;

        public CodeProvider()
        {
            images = new ImageList();
            images.Images.Add(WhiskeyEditor.UI.Assets.AssetManager.ICON_SAVE);
        }

        public int DefaultIndex
        {
            get { return 0; }
        }

        public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
        {

            DefaultCompletionData data1 = new DefaultCompletionData("Fear", "yikes", 0);
            DefaultCompletionData data2 = new DefaultCompletionData("Fart", "smelly", 0);
            return new ICompletionData[] { data1, data2 };

        }

        public ImageList ImageList
        {
            get { return images; }
        }

        public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
        {

            textArea.InsertString(data.Text);

            return false;
        }

        public string PreSelection
        {
            get { return "Pre"; }
        }

        public CompletionDataProviderKeyResult ProcessKey(char key)
        {
            return CompletionDataProviderKeyResult.InsertionKey;
        }
    }
}
