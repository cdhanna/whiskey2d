using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.TextEditor.Gui;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using Whiskey2D.Core;

namespace WhiskeyEditor.UI.Documents.Completion
{
    class DataProvider
    {

        List<Completor> completors;

        public DataProvider()
        {
            completors = new List<Completor>();

            completors.Add(new Completor(typeof(Vector)));
            completors.Add(new Completor(typeof(Color)));
            completors.Add(new Completor(typeof(GameObject)));
            completors.Add(new Completor(typeof(Microsoft.Xna.Framework.Input.Keys)));
            completors.Add(new Completor(typeof(Whiskey2D.Core.Managers.ObjectManager)));
            completors.Add(new Completor(typeof(Whiskey2D.Core.Managers.InputManager)));
        }


        public ICompletionData[] getData(string token, string tokenType)
        {
            List<ICompletionData> data = new List<ICompletionData>();

            foreach (Completor c in completors)
            {
                data.AddRange(c.getData(token, tokenType));
            }

            return data.ToArray();
        }

    }
}
