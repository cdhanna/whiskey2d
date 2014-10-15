﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhiskeyEditor.ClassLoader;
using Whiskey2D.Core;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace WhiskeyEditor.Controls
{
    public partial class Editor : Form
    {

        

        public Editor()
        {
            InitializeComponent();

            TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Sprite), new TypeConverterAttribute(typeof(ExpandableObjectConverter)));
            TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Vector), new TypeConverterAttribute(typeof(ValueTypeTypeConverter<Vector>)));
            TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Color), new TypeConverterAttribute(typeof(ValueTypeTypeConverter<Whiskey2D.Core.Color>)));
            TypeDescriptor.AddAttributes(typeof(GameObject), new TypeConverterAttribute(typeof(GetSetTypeConverter)));
            TypeDescriptor.AddAttributes(typeof(GameObjectDescriptor), new TypeConverterAttribute(typeof(GetSetTypeConverter)));
            TypeDescriptor.AddAttributes(typeof(GameObjectDescriptor), new TypeConverterAttribute(typeof(ExpandableObjectConverter)));
            TypeDescriptor.AddAttributes(typeof(List<ClassLoader.PropertyDescriptor>), new TypeConverterAttribute(typeof(ExpandableObjectConverter)));
            TypeDescriptor.AddAttributes(typeof(Whiskey2D.Core.Sprite), new TypeConverterAttribute(typeof(GetSetTypeConverter)));

            textEditorControl1.SetHighlighting("C#");
            
        }

        private void whiskeyControl_DragDrop(object sender, DragEventArgs e)
        {
            object typeOfDrag = e.Data.GetData(DataFormats.Serializable);
            Console.WriteLine(typeOfDrag);
            Point p = new Point(e.X, e.Y);
            p = gameSpace.PointToClient(p);
            gameSpace.addNewGameObject((Type)typeOfDrag, p.X, p.Y);
        }

        private void whiskeyControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void typeEditorButtonPanel1_Load(object sender, EventArgs e)
        {

        }

        private void playTestBtn_Click(object sender, EventArgs e)
        {

            String localPath = "..\\..\\..\\whiskey2d\\Whiskey2D\\bin\\WindowsGL\\Debug\\";
            String dest = @"C:\Users\cdhan_000\Documents\Dev Projects\Whiskey\whiskey2d\whiskey2d\Whiskey2D\bin\WindowsGL\Debug\";
            gameSpace.save();
            File.Delete(dest + "game-state.txt");
            File.Copy("game-state.txt", dest + "game-state.txt");
            foreach (GameObjectDescriptor desc in GameObjectDescriptor.descToAsmMap.Keys)
            {
                Assembly asm = GameObjectDescriptor.descToAsmMap[desc];
                string target = dest + asm.FullName.Substring(0, asm.FullName.IndexOf(','))+".dll";
                File.Delete(target);
                File.Copy(asm.Location, target);

            }
            ProcessStartInfo procInfo = new ProcessStartInfo();
            procInfo.WorkingDirectory = dest;
            procInfo.FileName = "Whiskey2D.exe";
            Process.Start(procInfo);
            

        }

    }
}