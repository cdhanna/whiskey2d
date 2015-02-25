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
using WhiskeyEditor.UI.Documents.Completion;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;
using Whiskey2D.Core;
using System.Reflection;

namespace WhiskeyEditor.UI.Documents
{
    class CodeProvider : ICompletionDataProvider
    {

        private ImageList images;
        private DataProvider dp;

        public CodeProvider()
        {
            dp = new DataProvider();
            images = new ImageList();
            images.Images.Add(WhiskeyEditor.UI.Assets.AssetManager.ICON_SAVE);
        }

        public int DefaultIndex
        {
            get { return 0; }
        }

        public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
        {
            char separator = ';';

            string allText = textArea.MotherTextEditorControl.Text;
            int caretIndex = textArea.Caret.Offset;
            string preText = allText.Substring(0, caretIndex);
            
            //turn into lines
            string[] allLines = preText.Split('\n', '\r');
            //reject empty lines and comment lines
            List<String> goodLines = new List<string>();
            foreach (string line in allLines)
            {
                if (!line.StartsWith("//") && !line.Equals(""))
                {
                    goodLines.Add(line);
                }
                
            }
            
            //tokenize
            List<String> tokens = new List<String>();
            foreach (string line in goodLines)
            {
                foreach (String token in line.Split(' ', ';', '(', ')', '{', '}'))
                {
                    if (!token.Equals(""))
                    {
                        tokens.Add(token.Replace("\t", ""));
                    }
                }
            }

            //get last token
            string lastToken = tokens[tokens.Count - 1];

            //determine type of last token
            string lastTokenType = null;

            //move backwards, checking for variable declarations
            for (int i = tokens.Count - 2; i > 0; i--)
            {
                string token = tokens[i];
                string prevToken = tokens[i - 1];

                if (token.Equals(lastToken))
                {
                    lastTokenType = prevToken;
                    break;
                }

            }

            //check for already existing variables
            if (lastTokenType == null)
            {
                ScriptDescriptor sDesc = FileManager.Instance.lookUpFileByPath<ScriptDescriptor>(fileName);
                if (sDesc != null) //its a script
                {
                    foreach (PropertyInfo p in typeof(Script).GetProperties())
                    {
                        if (p.Name.Equals(lastToken))
                        {
                            lastTokenType = p.PropertyType.Name;
                            //if (lastTokenType.Equals("GameObject"))
                            //{
                            //    lastTokenType = sDesc.TargetTypeName;
                            //}
                            break;
                        }
                    }
                }
            }

            return dp.getData(lastToken, lastTokenType);

          

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
            get;
            set;
        }

        public CompletionDataProviderKeyResult ProcessKey(char key)
        {
            return CompletionDataProviderKeyResult.NormalKey;
        }
    }
}
