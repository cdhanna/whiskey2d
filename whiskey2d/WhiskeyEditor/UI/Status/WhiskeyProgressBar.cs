using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WhiskeyEditor.UI.Status
{
    public class WhiskeyProgressBar : ProgressBar
    {

        public WhiskeyProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);


            this.Maximum = 100;
            this.Minimum = 0;
            this.Style = ProgressBarStyle.Continuous;
           
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            if(ProgressBarRenderer.IsSupported)
               ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), 1, 1, Width-2, rec.Height-2);
        
            rec.Height = rec.Height - 4;
        
            e.Graphics.FillRectangle(new SolidBrush(UIManager.Instance.FlairColor), 2, 2, rec.Width, rec.Height);
        }

        

    }
}
