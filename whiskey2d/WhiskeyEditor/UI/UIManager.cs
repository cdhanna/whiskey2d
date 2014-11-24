using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.UI.Documents;
using System.Drawing;

namespace WhiskeyEditor.UI
{
    class UIManager
    {

        public const string VIEW_NAME_OUTPUT = "output";
        public const string VIEW_NAME_LIBRARY = "lib";
        public const string VIEW_NAME_DOCUMENTS = "docs";

        public const string COMMAND_SAVE = "save";
        public const string COMMAND_PLAY = "play";

        private static UIManager instance = new UIManager();
        public static UIManager Instance { get { return instance; } }
        private UIManager()
        {
            FlairColor = Color.FromArgb(128, 200, 255);
            ErrorColor = Color.LightPink;
            WarningColor = Color.LightSalmon;
        }

        public CompileManager Compiler { get { return CompileManager.Instance; } }
        public FileManager Files { get { return FileManager.Instance; } }
        public InstanceManager GobInstances { get { return InstanceManager.Instance; } }



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
            else return new DocumentTab(file.FilePath, parent);

        }

    }
}
