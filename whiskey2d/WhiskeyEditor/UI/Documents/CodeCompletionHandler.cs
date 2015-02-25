using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui;
using ICSharpCode.TextEditor.Gui.CompletionWindow;


namespace WhiskeyEditor.UI.Documents
{
    class CodeCompletionKeyHandler
    {
        Form mainForm;
        TextEditorControl editor;
        CodeCompletionWindow codeCompletionWindow;
        string fileName;

        private CodeCompletionKeyHandler(Form mainForm, TextEditorControl editor, string fileName)
        {
            this.fileName = fileName;
            this.mainForm = mainForm;
            this.editor = editor;
        }

        public static CodeCompletionKeyHandler Attach(Form mainForm, TextEditorControl editor, string fileName)
        {
            CodeCompletionKeyHandler h = new CodeCompletionKeyHandler(mainForm, editor, fileName);



            editor.ActiveTextAreaControl.TextArea.KeyEventHandler += h.TextAreaKeyEventHandler;

            // When the editor is disposed, close the code completion window
            editor.Disposed += h.CloseCodeCompletionWindow;

            return h;
        }

        /// <summary>
        /// Return true to handle the keypress, return false to let the text area handle the keypress
        /// </summary>
        bool TextAreaKeyEventHandler(char key)
        {
            if (codeCompletionWindow != null)
            {
                // If completion window is open and wants to handle the key, don't let the text area
                // handle it
                if (codeCompletionWindow.ProcessKeyEvent(key))
                    return true;
            }
            if (key == '.')
            {
                ICompletionDataProvider completionDataProvider = new CodeProvider();
                codeCompletionWindow = CodeCompletionWindow.ShowCompletionWindow(
                    mainForm,					// The parent window for the completion window
                    editor, 					// The text editor to show the window for
                    fileName,		// Filename - will be passed back to the provider
                    completionDataProvider,		// Provider to get the list of possible completions
                    key							// Key pressed - will be passed to the provider
                );
                if (codeCompletionWindow != null)
                {
                    // ShowCompletionWindow can return null when the provider returns an empty list
                    codeCompletionWindow.Closed += new EventHandler(CloseCodeCompletionWindow);
                }
            }
            return false;
        }

        void CloseCodeCompletionWindow(object sender, EventArgs e)
        {
            if (codeCompletionWindow != null)
            {
                codeCompletionWindow.Closed -= new EventHandler(CloseCodeCompletionWindow);
                codeCompletionWindow.Dispose();
                codeCompletionWindow = null;
            }
        }
    }
}
