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
        private GeneralPropertyDescriptor AmbientDesc;

        private GeneralPropertyDescriptor CameraTruePosDesc;
        private GeneralPropertyDescriptor CameraZoomDesc;


        
        private GeneralPropertyDescriptor ShaderBloomThresholdDesc;
        private GeneralPropertyDescriptor ShaderBlurdDesc;
        private GeneralPropertyDescriptor ShaderBloomDesc;
        private GeneralPropertyDescriptor ShaderBaseDesc;
        private GeneralPropertyDescriptor ShaderBloomSatDesc;
        private GeneralPropertyDescriptor ShaderBaseSatDesc;


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
            AmbientDesc = WhiskeyPropertyListGrid.addOtherProperty("Ambient Light", "Details", Descriptor.Level.AmbientLight);

            CameraZoomDesc = WhiskeyPropertyListGrid.addOtherProperty("Zoom", "Camera", Descriptor.Level.Camera.Zoom);
            CameraTruePosDesc = WhiskeyPropertyListGrid.addOtherProperty("Position", "Camera", Descriptor.Level.Camera.TruePosition);

            ShaderBloomThresholdDesc = WhiskeyPropertyListGrid.addOtherProperty("Bloom Threshold", "Effects", Descriptor.Level.BloomSettings.BloomThreshold);
            ShaderBlurdDesc = WhiskeyPropertyListGrid.addOtherProperty("Blur Amount", "Effects", Descriptor.Level.BloomSettings.BlurAmount);
            ShaderBloomDesc = WhiskeyPropertyListGrid.addOtherProperty("Bloom Intensity", "Effects", Descriptor.Level.BloomSettings.BloomIntensity);
            ShaderBaseDesc = WhiskeyPropertyListGrid.addOtherProperty("Base Intensity", "Effects", Descriptor.Level.BloomSettings.BaseIntensity);
            ShaderBloomSatDesc = WhiskeyPropertyListGrid.addOtherProperty("Bloom Saturation", "Effects", Descriptor.Level.BloomSettings.BloomSaturation);
            ShaderBaseSatDesc = WhiskeyPropertyListGrid.addOtherProperty("Base Saturation", "Effects", Descriptor.Level.BloomSettings.BaseSaturation);

            ShaderBloomThresholdDesc.CustomTypeEditor = new UI.Properties.RestrictedFloatPicker(ShaderBloomThresholdDesc, 0, 1, .05f);
            ShaderBlurdDesc.CustomTypeEditor = new UI.Properties.RestrictedFloatPicker(ShaderBlurdDesc, 0, 4, .1f);
            ShaderBloomDesc.CustomTypeEditor = new UI.Properties.RestrictedFloatPicker(ShaderBloomDesc, 0, 2, .1f);
            ShaderBaseDesc.CustomTypeEditor = new UI.Properties.RestrictedFloatPicker(ShaderBaseDesc, 0, 2, .1f);
            ShaderBloomSatDesc.CustomTypeEditor = new UI.Properties.RestrictedFloatPicker(ShaderBloomSatDesc, 0, 1, .05f);
            ShaderBaseSatDesc.CustomTypeEditor = new UI.Properties.RestrictedFloatPicker(ShaderBaseSatDesc, 0, 1, .05f);
            

            ColorDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Color = (Whiskey2D.Core.Color) ColorDesc.PropValue;
            };
            AmbientDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Level.AmbientLight = (Whiskey2D.Core.Color)AmbientDesc.PropValue;
            };

            CameraTruePosDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Level.Camera.TruePosition = (Whiskey2D.Core.Vector)CameraTruePosDesc.PropValue;
            };
            CameraZoomDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Level.Camera.Zoom = (float)CameraZoomDesc.PropValue;
            };

            ShaderBloomThresholdDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Level.BloomSettings.BloomThreshold = (float)ShaderBloomThresholdDesc.PropValue;
                WhiskeyEditor.MonoHelp.WhiskeyControl.forceRedraw();
            };
            ShaderBlurdDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Level.BloomSettings.BlurAmount = (float)ShaderBlurdDesc.PropValue;
                WhiskeyEditor.MonoHelp.WhiskeyControl.forceRedraw();
            };
            ShaderBloomDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Level.BloomSettings.BloomIntensity = (float)ShaderBloomDesc.PropValue;
                WhiskeyEditor.MonoHelp.WhiskeyControl.forceRedraw();
            };
            ShaderBaseDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Level.BloomSettings.BaseIntensity = (float)ShaderBaseDesc.PropValue;
                WhiskeyEditor.MonoHelp.WhiskeyControl.forceRedraw();
            };
            ShaderBloomSatDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Level.BloomSettings.BloomSaturation = (float)ShaderBloomSatDesc.PropValue;
                WhiskeyEditor.MonoHelp.WhiskeyControl.forceRedraw();
            };
            ShaderBaseSatDesc.ValueChanged += (s, a) =>
            {
                Descriptor.Level.BloomSettings.BaseSaturation = (float)ShaderBaseSatDesc.PropValue;
                WhiskeyEditor.MonoHelp.WhiskeyControl.forceRedraw();
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
