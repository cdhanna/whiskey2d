﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;
using WhiskeyEditor.Backend;

using ICSharpCode.TextEditor;
using WhiskeyEditor.Backend.Managers;
using System.IO;

namespace WhiskeyEditor.UI.Documents
{
    class CodeDocument : DocumentTab
    {

        private TextEditorControl editor;
        private CodeDescriptor desc;
        private FileChangedEventHandler discHandler;
        private string lastText;

        public CodeDocument(CodeDescriptor desc, DocumentView parent)
            : base(desc.FilePath, parent)
        {
            this.desc = desc;
            Text = desc.Name;
            BackColor = UIManager.Instance.DullFlairColor;



            initControls();
            configureControls();
            addControls();

            discHandler = new FileChangedEventHandler(discListener);
            Dirty = false;
        }

        public override void open()
        {
            refreshFromDisc(desc);
            UIManager.Instance.Files.FileChanged += discHandler;
            lastText = editor.Text;
            base.open();
        }

        public override void save()
        {
            string code = editor.Text;
            FileStream fileStream = File.Create(desc.FilePath);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.WriteLine(code);
            writer.Flush();
            writer.Close();
            fileStream.Close();

            lastText = editor.Text;

            base.save();
        }

        public override void close()
        {
            UIManager.Instance.Files.FileChanged -= discHandler;
            base.close();
        }

        private void discListener(object sender, FileEventArgs args)
        {
            refreshFromDisc(desc);
        }

        public void refreshFromDisc(CodeDescriptor desc)
        {
            this.Invoke(new NoArgFunction(() =>
            {
                TextLocation l = editor.ActiveTextAreaControl.Caret.Position;

                Dirty = false;
                string code = "";
                foreach (string line in desc.readAllLines())
                {
                    code += line + Environment.NewLine;
                }
                editor.Text = code;
                editor.ActiveTextAreaControl.Caret.Position = l;
                Dirty = false;
            }));
            
        }

        private void initControls()
        {
            editor = new TextEditorControl();
            editor.SetHighlighting("C#");

            editor.ShowLineNumbers = true;

        }

        private void configureControls()
        {
            editor.TextChanged += (s, a) =>
            {
                Dirty = (editor.Text != lastText);
            };
        }

        private void addControls()
        {
            editor.Dock = DockStyle.Fill;
            
            ContentPanel.Controls.Add(editor);
        }

    }
}