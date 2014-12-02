using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.UI.Documents;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace WhiskeyEditor.UI
{
    class UIManager
    {

        public const string VIEW_NAME_OUTPUT = "output";
        public const string VIEW_NAME_LIBRARY = "lib";
        public const string VIEW_NAME_DOCUMENTS = "docs";

        public const string COMMAND_SAVE = "save";
        public const string COMMAND_PLAY = "play";
        public const string COMMAND_COMPILE = "compile";

        private static UIManager instance = new UIManager();
        public static UIManager Instance { get { return instance; } }
        private UIManager()
        {
            FlairColor = Color.Orange;//Color.FromArgb(128, 200, 255);
            ErrorColor = Color.LightPink;
            WarningColor = Color.LightSalmon;
        }


        public CompileManager Compiler { get { return CompileManager.Instance; } }
        public FileManager Files { get { return FileManager.Instance; } }
        public InstanceManager GobInstances { get { return InstanceManager.Instance; } }


        public TopView TopView { get; private set; }


        private Color flairColor;
        public Color FlairColor
        {
            get
            {
                return flairColor;
            }
            set
            {
                flairColor = value;
                DullFlairColor = Color.FromArgb(flairColor.R / 2, flairColor.G / 2, flairColor.B / 2);
                PaleFlairColor = Color.FromArgb( (flairColor.R+255)/2, (flairColor.G+255)/2, (flairColor.B+255)/2 );
            }
        }
        public Color DullFlairColor { get; private set; }
        public Color PaleFlairColor { get; private set; }


        public Color ErrorColor { get; private set; }
        public Color WarningColor { get; private set; }


        public DocumentTab getDocumentTabFor(DocumentView parent, FileDescriptor file)
        {
            if (file is CodeDescriptor)
            {
                return new CodeDocument( (CodeDescriptor) file, parent);
            }
            else if (file is LevelDescriptor)
            {
                return new LevelDocument((LevelDescriptor)file, parent);
            }
            else return new DocumentTab(file.FilePath, parent);
        }

        public string normalizePath(string path)
        {
            path = Path.GetFullPath(path).ToLower();
            if (path.Contains(Path.DirectorySeparatorChar))
            {
                int i = path.LastIndexOf(Path.DirectorySeparatorChar);
                if (i == path.Length - 1)
                {
                    path = path.Substring(0, 1).ToUpper() + path.Substring(1);
                }
                else
                {
                    path = path.Substring(0, i + 1) + path.Substring(i + 1, 1).ToUpper() + path.Substring(i+2);
                }
            }
            else if (path.Length > 0)
            {
                path = path.Substring(0, 1).ToUpper() + path.Substring(1);
            }

            return path;
        }

        public void startup()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TopView = new TopView();
            Application.Run(TopView);
        }

        public void requestClose()
        {
            TopView.Close();
        }


    }
}
