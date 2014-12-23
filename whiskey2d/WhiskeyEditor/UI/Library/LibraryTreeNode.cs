using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using WhiskeyEditor.UI.Assets;

namespace WhiskeyEditor.UI.Library
{
    public class LibraryTreeNode : TreeNode
    {

        public string FilePath { get; private set; }
        public bool IsFile { get; private set; }

        public LibraryTreeNode(string text, string filePath)
            : base(text)
        {
            FilePath = UIManager.Instance.normalizePath(clearExtraSlashes(filePath)) ;
            IsFile = false;



            ImageIndex = AssetManager.indexOf(AssetManager.FILE_ICON_FLDR);
            SelectedImageIndex = AssetManager.indexOf(AssetManager.FILE_ICON_FLDR); ;
        }

        

        public void populate()
        {

            string[] filePaths = Directory.GetFiles(FilePath);
            foreach (string path in filePaths)
            {
                LibraryTreeNode node = new LibraryTreeNode(getFileNameWithoutExtension(path), clearExtraSlashes(path));

                int index = AssetManager.indexOf(AssetManager.FILE_ICON_FILE);
                if (path.EndsWith(".state"))
                {
                    index = AssetManager.indexOf(AssetManager.FILE_ICON_FILE_LEVEL);
                }
                if (path.EndsWith(".cs"))
                {
                    index = AssetManager.indexOf(AssetManager.FILE_ICON_FILE_TYPE);
                }
                if (path.EndsWith(".png"))
                {
                    index = AssetManager.indexOf(AssetManager.FILE_ICON_FILE_PICTURE);
                }

                node.ImageIndex = index;
                node.SelectedImageIndex = index;
                
                node.IsFile = true;
                Nodes.Add(node);
            }

            string[] dirPaths = Directory.GetDirectories(FilePath);
            foreach (string dir in dirPaths)
            {
                LibraryTreeNode node = new LibraryTreeNode(getFileName(dir), dir);
                node.IsFile = false;
                node.ImageIndex = AssetManager.indexOf(AssetManager.FILE_ICON_FLDR);
                node.SelectedImageIndex = AssetManager.indexOf(AssetManager.FILE_ICON_FLDR); ;
                
                Nodes.Add(node);
                node.populate();

            }
            
        }

        private string clearExtraSlashes(string path)
        {
            while (path.Contains(@"\\\"))
                path = path.Replace(@"\\\", @"\\");
            return path;
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
