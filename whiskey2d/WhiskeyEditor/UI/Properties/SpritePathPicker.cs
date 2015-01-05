using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WhiskeyEditor.UI.Properties.Converters;
using System.Globalization;
using WhiskeyEditor.Backend;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.Design;
using Whiskey2D.Core;
using WhiskeyEditor.Backend.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace WhiskeyEditor.UI.Properties
{
    class SpritePathPicker : UITypeEditor
    {

        SpriteButtonControl control;
        IWindowsFormsEditorService service;
        OpenFileDialog fileDialog;
        string result;

        public SpritePathPicker()
        {

            control = new SpriteButtonControl();

            fileDialog = new OpenFileDialog();
            fileDialog.CheckFileExists = true;
            fileDialog.DefaultExt = ".png";
            fileDialog.Filter = "png files | *.png";


            control.ArtBox.SelectedValueChanged += clickCombo;
            control.PixelButton.Click += clickPixel;
            control.FileButton.Click += clickFile;
        }

        private void clickCombo(object sender, EventArgs args)
        {
            if (service != null)
            {
                object pick = control.ArtBox.SelectedItem;
                if (pick != null)
                {
                    result = (string)pick;
                    service.CloseDropDown();
                }
            }
        
        }


        private void clickPixel(object sender, EventArgs args)
        {
            if (service != null)
            {
                result = Sprite.PIXEL;
                service.CloseDropDown();
            }
        }

        private void clickFile(object sender, EventArgs args)
        {
            if (service != null)
            {
                DialogResult dr = fileDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    result = fileDialog.FileName;
                    result = ProjectManager.Instance.ActiveProject.addMedia(result);
                }
                service.CloseDropDown();
            }
        }

        private void setArtBox()
        {
            control.ArtBox.Items.Clear();
            string[] files = ProjectManager.Instance.ActiveProject.getMedia("png");
            foreach (string file in files)
            {
                control.ArtBox.Items.Add(file);
            }
            control.ArtBox.SelectedIndex = -1;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

             // show the list box
            if (service != null && value is Sprite)
            {
                Sprite sprite = (Sprite)value;
                result = null;
                setArtBox();
                service.DropDownControl(control);

                if (result != null)
                {

                    Vector oldSize = sprite.ImageSize;
                    string oldImage = sprite.ImagePath;
                    sprite.ImagePath = result;
                    Texture2D tex = sprite.getImage();
                    if (tex != null && oldImage.Equals(Sprite.PIXEL))
                    {
                        Vector currentSize = sprite.ImageSize;
                        Vector sizeRatio = new Vector(currentSize.X / oldSize.X, currentSize.Y / oldSize.Y);
                        sprite.Scale = new Vector(sprite.Scale.X / sizeRatio.X, sprite.Scale.Y / sizeRatio.Y);
                    }
                }

               

            }
            
            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
            //return base.GetEditStyle(context);
        }

    }
}
