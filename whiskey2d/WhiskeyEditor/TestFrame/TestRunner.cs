using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.TestFrame.Tests;

namespace WhiskeyEditor.TestFrame
{
    class TestRunner
    {
        [STAThread]
        public static void Main()
        {
            Tester tester = new ConfigTest();
            tester.runTests();
        }

    }
}
