using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.CodeDom.Compiler;

namespace WhiskeyEditor.Backend
{
    class ShaderBuilder
    {

        public string Path2MGFX { get; private set; }

        public ShaderBuilder()
            : this("mgfx/2MGFX.exe")
        {
        }

        public ShaderBuilder(String pathTo2MGFX)
        {
            Path2MGFX = pathTo2MGFX;
        }

        public void buildShader(string inputFile, string outputFile)
        {

            inputFile = "\"" + inputFile + "\"";
            outputFile = "\"" + outputFile + "\"";
            var startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = "";
            startInfo.Arguments = inputFile + " " + outputFile;
            startInfo.FileName = Path2MGFX;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            Process proc = Process.Start(startInfo);
            List<CompilerError> errs = new List<CompilerError>();
            String[] lines = proc.StandardError.ReadToEnd().Split('\n');
            foreach (String line in lines)
            {
                if (line.Contains('(')){
                    String lineNumber = line.Substring(line.IndexOf('(') + 1, line.IndexOf(',') - line.IndexOf('(') - 1);
                    CompilerError err = new CompilerError(inputFile, Int16.Parse(lineNumber), 0, "0", line.Substring(line.IndexOf(')') + 1));
                    errs.Add(err);
                }
            }
            CompilerErrorCollection errors = new CompilerErrorCollection(errs.ToArray());
            UI.UIManager.Instance.TopView.Output.setErrors(errors);

        }

    }
}
