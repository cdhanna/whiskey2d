using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
namespace WhiskeyEditor.UI.Properties.Editors
{
    class LevelDescriptorPropertyEditor : FileDescriptorPropertyEditor<LevelDescriptor>
    {

        private GeneralPropertyDescriptor ColorDesc;

        private GeneralPropertyDescriptor CameraTruePosDesc;
        private GeneralPropertyDescriptor CameraZoomDesc;

        public LevelDescriptorPropertyEditor(LevelDescriptor lDesc)
            : base(lDesc)
        {
            
            Title = "Level Properties";

          

            initControls();
            addControls();
        }

        private void refreshPropertyContent()
        {


            Point p = WhiskeyPropertyListGrid.PropertyGrid.PointToClient(Cursor.Position);
            if (p.X > 0 && p.Y > 0 && p.X < WhiskeyPropertyListGrid.Width && p.Y < WhiskeyPropertyListGrid.Height)
            {

            }
            else
            {
                CameraTruePosDesc.PropValue = Descriptor.Level.Camera.TruePosition;
                CameraZoomDesc.PropValue = Descriptor.Level.Camera.Zoom;
                WhiskeyPropertyListGrid.Refresh();
            }
        }

        private void initControls()
        {

            Stopwatch timer = Stopwatch.StartNew();
            TimeSpan TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 16);
          
            

            //PropertyGrid.PropertyList = Descriptor.getPropertySet();
            Application.Idle += (s, a) =>
            {
                if (timer.ElapsedMilliseconds >= TargetElapsedTime.Milliseconds)
                {

                    refreshPropertyContent();

                    timer.Restart();
                }
            };
            
            ColorDesc = WhiskeyPropertyListGrid.addOtherProperty("Color", "Details", Descriptor.Color);
            CameraZoomDesc = WhiskeyPropertyListGrid.addOtherProperty("Zoom", "Camera", Descriptor.Level.Camera.Zoom);
            CameraTruePosDesc = WhiskeyPropertyListGrid.addOtherProperty("Position", "Camera", Descriptor.Level.Camera.TruePosition);
            ColorDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Color = (Whiskey2D.Core.Color) ColorDesc.PropValue;
            };


            CameraTruePosDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Level.Camera.TruePosition = (Whiskey2D.Core.Vector)CameraTruePosDesc.PropValue;
            };
            CameraZoomDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Level.Camera.Zoom = (float)CameraZoomDesc.PropValue;
            };
            


            WhiskeyPropertyListGrid.PropertyGrid.PropertyValueChanged += (s, a) =>
            {
               // Descriptor.ensureFileExists();
            };
        }

        private void addControls()
        {
            
        }

    }
}
