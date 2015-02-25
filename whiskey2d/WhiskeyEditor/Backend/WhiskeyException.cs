using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    class WhiskeyException : Exception
    {
        public WhiskeyException(string msg)
            : base(msg)
        {
            
        }

        public virtual void displayMessageBox()
        {
            UI.UIManager.Instance.writeException(this);
        }

    }

    class WhiskeyWarning : WhiskeyException
    {
        public string Warning { get; private set; }
        public WhiskeyWarning(string warning, string msg)
            : base(msg)
        {
            this.Warning = warning;
        }

        public override void displayMessageBox()
        {
            UI.UIManager.Instance.writeWarning(this);
            //base.displayMessageBox();
        }

    }

}
