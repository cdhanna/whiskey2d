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
    class WhiskeyPropertyValuePicker : UITypeEditor
    {


        private UITypeEditor lookUpEditor(ITypeDescriptorContext context)
        {
            
                if (context != null && context.Instance != null && typeof(WhiskeyPropertyContainer).IsAssignableFrom(context.Instance.GetType()))
                {
                    WhiskeyPropertyContainer wpc = (WhiskeyPropertyContainer)context.Instance;
                    return WhiskeyTypeEditors.lookUp(wpc.TypeName);

                }
            
            return null;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            UITypeEditor editor = lookUpEditor(context);
            if (editor != null)
            {
                return editor.EditValue(context, provider, value);
            }
            
            return base.EditValue(context, provider, value);
        }

        public override void PaintValue(PaintValueEventArgs e)
        {

            UITypeEditor editor = lookUpEditor(e.Context);
            if (editor != null)
            {

                
                PaintValueEventArgs e2 = new PaintValueEventArgs(e.Context, e.Value, e.Graphics, e.Bounds);
                editor.PaintValue(e2);
                return;
                
                //return editor.GetPaintValueSupported(context);
            }
            base.PaintValue(e);
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

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {

            UITypeEditor editor = lookUpEditor(context);
            if (editor != null)
            {
                return editor.GetEditStyle(context);
            }
            else return base.GetEditStyle(context);
        }

    }
}
