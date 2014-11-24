using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
namespace WhiskeyEditor.UI.Library
{
    public class LibraryTreeNode : TreeNode
    {

        public string FilePath { get; private set; }
        public bool IsFile { get; private set; }

        public LibraryTreeNode(string text, string filePath)
            : base(text)
        {
            FilePath = filePath;
            IsFile = false;
        }

        

        public void populate()
        {

            string[] filePaths = Directory.GetFiles(FilePath);
            foreach (string path in filePaths)
            {
                LibraryTreeNode node = new LibraryTreeNode(getFileNameWithoutExtension(path), path);
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
                node.IsFile = true;
                Nodes.Add(node);
            }

            string[] dirPaths = Directory.GetDirectories(FilePath);
            foreach (string dir in dirPaths)
            {
                LibraryTreeNode node = new LibraryTreeNode(getFileName(dir), dir);
                node.IsFile = false;
                Nodes.Add(node);
                node.populate();

            }
            
        }

        private string getFileName(string filePath)
        {
            if (filePath.Contains(Path.DirectorySeparatorChar))
            {
                return filePath.Substring(filePath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            }
            else return filePath;
        }
        private string getFileNameWithoutExtension(string filePath)
        {
            filePath = getFileName(filePath);
            if (filePath.Contains("."))
            {
                return filePath.Substring(0, filePath.LastIndexOf("."));
            }
            else return filePath;
        }
        
    }
}
