using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend.Managers;
using WhiskeyEditor.Backend.Actions;
using System.Threading;

namespace WhiskeyEditor.UI.Status
{
    public class WhiskeyStatusBar : StatusBar
    {
        private WhiskeyProgressBar progressBar;
        private Panel mainPanel;
        private Label messageLabel;
        private Thread progressToZeroThread;
        private int time = 0;
        private bool progressToZeroFlag = false;

        public WhiskeyStatusBar()
        {

            BackColor = Color.Gray;

            this.SetStyle(ControlStyles.DoubleBuffer |
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint,
            true);
            this.UpdateStyles();

           
            //this.def

            initControls();
            addControls();
        }

        protected override void Dispose(bool disposing)
        {
            progressToZeroThread.Abort();
            base.Dispose(disposing);
        }
        private void launchThreads()
        {
            if (progressToZeroThread != null)
            {
                progressToZeroThread.Abort();
            }
            progressToZeroThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        time--;

                        if (time <= 0 && progressToZeroFlag)
                        {
                            progressBar.Invoke(new NoArgFunction(() =>
                            {
                                progressBar.Value = 0;
                                progressBar.Visible = false;
                                mainPanel.Visible = true;
                            }));
                        }

                        Thread.Sleep(5);
                    }
                }
                catch (ThreadAbortException e)
                {
                    Console.Write("progress bar thread aborted");
                }
            });
            progressToZeroThread.Name = "ProgressBarToZero";
            progressToZeroThread.Start();
        }

        private void initControls()
        {
            mainPanel = new Panel();
            mainPanel.AutoSize = true;
            mainPanel.BackColor = Color.Transparent;
            mainPanel.Visible = true;


            launchThreads();
            progressBar = new WhiskeyProgressBar();
            progressBar.Visible = false;
            ActionManager.Instance.ActionChanged += (s, a) =>
            {
                Invoke( new NoArgFunction( ()=> {
                    if (a != null && a.Action != null)
                    {
                        messageLabel.Text = a.Action.Name;
                        progressBar.Visible = true;
                        mainPanel.Visible = true;
                        progressToZeroFlag = false;
                        progressBar.Value = (int)(a.Action.Progress * 100);
                    }
                    else
                    {
                        progressToZeroFlag = true;
                        time = 100;
                        
                    }
                }));
            };

            messageLabel = new Label();
            //messageLabel.Margin = new Padding(0, 30,100, 0);
            messageLabel.Text = "hello world";
           
            messageLabel.TextAlign = ContentAlignment.MiddleLeft;
            messageLabel.ForeColor = Color.White;
        }

        private void addControls()
        {

            
            messageLabel.Dock = DockStyle.Left;
            mainPanel.Controls.Add(messageLabel);

            progressBar.Dock = DockStyle.Right;
            mainPanel.Controls.Add(progressBar);

            mainPanel.Dock = DockStyle.Right;
            Controls.Add(mainPanel);


            
        }

        public void setMessage(string msg)
        {
            this.Invoke(new NoArgFunction(() =>
            {
               if (!progressBar.Visible)
                {
                    messageLabel.Text = msg;
                }
            }));
            

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Gray), 0, 0, this.Width,
            this.Height);
            //e.Graphics.DrawString("customized drawing", new Font("Arial", 10), new
            //SolidBrush(Color.Red), this.ClientRectangle, new StringFormat());
            ////ControlPaint.DrawBorder(e.Graphics, 0, 0, this.Width, this.Height);
            base.OnPaint(e);
        }

    }
}
