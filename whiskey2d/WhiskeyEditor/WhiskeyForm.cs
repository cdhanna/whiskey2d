using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Whiskey2D.Service;
using System.Reflection;
using System.Diagnostics;
using Whiskey2D.Core;
using System.IO;
using Whiskey2D.PourGames.Game3;

namespace WhiskeyEditor
{
    public partial class WhiskeyForm : Form
    {
        ProjectManager projMan;
        Project project;
        SimpleTextPopup textPopup;

        

        public WhiskeyForm()
        {
            InitializeComponent();
            textPopup = new SimpleTextPopup();
            projMan = new ProjectManager();

            whiskeyControl.GobGrid = this.gobGrid;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //    DialogResult result = this.openProjectDialog.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        string path = this.openProjectDialog.InitialDirectory + this.openProjectDialog.FileName;

        //        project = projMan.loadProject(path);
        //        projMan.setTreeFor(project, this.directoryTree);
                
        //    }

          

        //}

        //private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    DialogResult result = this.newProjectDialog.ShowDialog();

        //    if (result == DialogResult.OK)
        //    {
        //        string directory = this.newProjectDialog.FileName;
        //        string name = directory.Substring(directory.LastIndexOf('\\') + 1);
        //        //directory = directory.Substring(0, directory.Length - name.Length);

        //        project = projMan.createNewProject(directory, name);
        //        projMan.setTreeFor(project, this.directoryTree);
                

        //    }
        //    else
        //    {
        //    }
        //}

        //private void newProjectDialog_FileOk(object sender, CancelEventArgs e)
        //{
           
        //}

        //private void compileButton_Click(object sender, EventArgs e)
        //{
            
        //    Compiler.getInstance().compileDirectory(project.NameNoExt, project.Directory + "//Src", "compile-lib\\MonoGame.Framework", "compile-lib\\Whiskey.Core");
        //}

        //private void runButton_Click(object sender, EventArgs e)
        //{
            
            
        //   // Compiler.getInstance().compileDirectory("Whiskey.TestImpl", "TestImpl", "MonoGame.Framework", "Whiskey.Core");
        //    //add core to path
        //    Assembly coreAssmebly = Assembly.LoadFrom("Whiskey.Core.dll");

        //    //add game data to path
        //    Assembly gameAssembly = Assembly.LoadFrom(project.NameNoExt + ".dll");


        //    //find gameManager
        //    Type[] coreTypes = coreAssmebly.GetTypes();
        //    foreach (Type type in coreTypes)
        //    {
        //        if (type.Name.Equals("GameManager"))
        //        {
        //            object gameManager = Activator.CreateInstance(type, gameAssembly);
        //            //debug.Text = "found game Manager";
        //            gameManager.GetType().GetMethod("go").Invoke(gameManager, new object[] { });
        //            break;
                    
        //        }
        //        Console.WriteLine(type.Name);
        //    }

           
        //}

        //private void addScriptButton_Click(object sender, EventArgs e)
        //{
        //    if (project != null)
        //    {
        //        textPopup.prompt.Text = "Script Name:";
        //        textPopup.DesktopLocation = new Point(this.DesktopLocation.X + this.addScriptButton.Location.X, this.DesktopLocation.Y + this.addScriptButton.Location.Y);
        //        textPopup.ShowDialog(this.addScriptButton);
        //        Console.WriteLine("dBone. " + textPopup.Results);
        //     //   if (newScriptPopup.DialogResult == DialogResult.OK)
        //        {
        //            project.addNewScript(textPopup.EnteredText);
        //            projMan.setTreeFor(project, directoryTree);
        //            Console.WriteLine("added " + textPopup.EnteredText);
        //        }
        //    }
        //}

        //private void addGameObjectButton_Click(object sender, EventArgs e)
        //{
        //    if (project != null)
        //    {
        //        textPopup.prompt.Text = "Object Name:";
        //        textPopup.DesktopLocation = new Point(this.DesktopLocation.X + this.addScriptButton.Location.X, this.DesktopLocation.Y + this.addScriptButton.Location.Y);
        //        textPopup.ShowDialog(this.addScriptButton);
        //        Console.WriteLine("done. " + textPopup.Results);
        //        //   if (newScriptPopup.DialogResult == DialogResult.OK)
        //        {
        //            project.addNewGameObject(textPopup.EnteredText);
        //            projMan.setTreeFor(project, directoryTree);
        //            Console.WriteLine("added " + textPopup.EnteredText);
        //        }
        //    }
        //}

      

        private void badGuyDrag_MouseDown(object sender, MouseEventArgs e)
        {
            
            badGuyDrag.DoDragDrop( typeof(BadGuy) , DragDropEffects.All);
        }

    
        private void whiskeyControl1_DragDrop(object sender, DragEventArgs e)
        {
            object typeOfDrag = e.Data.GetData(DataFormats.Serializable);
            Console.WriteLine(typeOfDrag);
            Point p = new Point(e.X, e.Y);
            p = this.PointToClient(p);
            whiskeyControl.addNewGameObject((Type)typeOfDrag, p.X, p.Y);

        }

        private void whiskeyControl1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            whiskeyControl.save();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            whiskeyControl.load();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            ProcessStartInfo p = new ProcessStartInfo("Whiskey2D.exe");
            whiskeyControl.save();
            File.Delete("..\\..\\..\\Whiskey2D\\bin\\WindowsGL\\Debug\\game-state.txt");
            File.Copy("game-state.txt", "..\\..\\..\\Whiskey2D\\bin\\WindowsGL\\Debug\\game-state.txt");
            p.WorkingDirectory = "..\\..\\..\\Whiskey2D\\bin\\WindowsGL\\Debug";
            Process.Start(p);
            
        }
    }
}
