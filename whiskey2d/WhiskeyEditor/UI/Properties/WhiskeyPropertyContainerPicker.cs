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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.Design;


namespace WhiskeyEditor.UI.Properties
{
    class WhiskeyPropertyContainerPicker : UITypeEditor
    {


        public static Dictionary<string, UITypeEditor> editors = new Dictionary<string, UITypeEditor>();
        static WhiskeyPropertyContainerPicker()
        {
            editors.Add("Color", new ColorPicker());
            editors.Add("Sprite", new SpritePathPicker());
        }

        private UITypeEditor lookUpEditor(ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor is GeneralPropertyDescriptor)
            {
                GeneralPropertyDescriptor gpd = (GeneralPropertyDescriptor)context.PropertyDescriptor;

                if (gpd.PropValue != null && typeof(WhiskeyPropertyContainer).IsAssignableFrom(gpd.PropValue.GetType()))
                {
                    WhiskeyPropertyContainer wpc = (WhiskeyPropertyContainer)gpd.PropValue;
                    //if (editors.ContainsKey(wpc.TypeName))
                    //{
                    //    return editors[wpc.TypeName];
                    //}
                    return WhiskeyTypeEditors.lookUp(wpc.TypeName);
                    
                }
            }
            return null;
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            UITypeEditor editor = lookUpEditor(context);
            if (editor != null)
            {
                return editor.GetPaintValueSupported(context);
            }
            return base.GetPaintValueSupported(context);
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
           
            UITypeEditor editor = lookUpEditor(e.Context);
            if (editor != null)
            {
                if (typeof(WhiskeyPropertyContainer).IsAssignableFrom(e.Value.GetType()))
                {
                    WhiskeyPropertyContainer wpc = (WhiskeyPropertyContainer)e.Value;
                    PaintValueEventArgs e2 = new PaintValueEventArgs(e.Context, wpc.Value, e.Graphics, e.Bounds);
                    editor.PaintValue(e2);
                    return;
                }
            }
            
            base.PaintValue(e);
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            UITypeEditor editor = lookUpEditor(context);
            if (editor != null)
            {
                if (typeof(WhiskeyPropertyContainer).IsAssignableFrom(value.GetType()))
                {
                    WhiskeyPropertyContainer wpc = (WhiskeyPropertyContainer)value;
                    
                    object newValue = editor.EditValue(context, provider, wpc.Value);

                    wpc.Value = newValue;

                    return value;
                }
            }
            
            
            return base.EditValue(context, provider, value);
            
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            UITypeEditor editor = lookUpEditor(context);
            if (editor != null)
            {
                return editor.GetEditStyle(context);
            }
            else
            {
                return UITypeEditorEditStyle.None;
            }
        }

    }
}
