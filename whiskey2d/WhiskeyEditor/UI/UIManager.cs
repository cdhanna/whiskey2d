using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.UI.Documents;
using WhiskeyEditor.UI.Properties;
using WhiskeyEditor.UI.Properties.Editors;
using WhiskeyEditor.UI.Documents.ContentFactories;
using WhiskeyEditor.UI.Documents.Info;
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
        public const string VIEW_NAME_PROPERTIES = "props";

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

            DocumentContentInfoFactory = new DocumentInfoFactory();
        }


        public CompileManager Compiler { get { return CompileManager.Instance; } }
        public FileManager Files { get { return FileManager.Instance; } }
        public InstanceManager GobInstances { get { return InstanceManager.Instance; } }


        public TopView TopView { get; private set; }

        public IDocumentContentFactoryBuilder DocumentContentFactoryBuilder { get; private set; }
        public DocumentInfoFactory DocumentContentInfoFactory { get; private set; }

        public event EventHandler Closing = new EventHandler((s, a) => { });

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

        public void setDocumentView(DocumentView docView)
        {
            DocumentContentFactoryBuilder = new DocumentContentFactoryBuilder(docView);
        }

        public IDocumentContentFactory getDocumentFactory(Descriptor descriptor)
        {
            DocumentContentInfo info = DocumentContentInfoFactory.generateDocumentContentInfo(descriptor);
            IDocumentContentFactory factory = DocumentContentFactoryBuilder.generateDocumentContentFactory(info);
            return factory;
        }

        public string normalizePath(string path)
        {
            while (path.Contains("\\\\"))
                path.Replace("\\\\", "\\");
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
            Closing(this, new EventArgs());
            TopView.Invoke(new NoArgFunction(() =>
            {
                TopView.Close();
            }));
        }



        public static Color convertColor(Whiskey2D.Core.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
        public static Whiskey2D.Core.Color convertColor(Color color)
        {
            return new Whiskey2D.Core.Color(color.R, color.G, color.B, color.A);
        }

    }
}
