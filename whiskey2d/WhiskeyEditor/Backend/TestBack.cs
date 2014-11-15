using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend
{
    class TestBack
    {

        [STAThread]
        public static void Main()
        {

            TypeDescriptor t = new TypeDescriptor("rightHere.txt", "TestMe");
            ScriptDescriptor s = new ScriptDescriptor("rightThere.txt", "RunMe", "TestMe");

            CompileManager.Instance.compile();
        }

    }
}
