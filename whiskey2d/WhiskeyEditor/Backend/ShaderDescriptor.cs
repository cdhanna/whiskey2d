using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Managers;
using System.IO;

namespace WhiskeyEditor.Backend
{
    [Serializable]
    public class ShaderDescriptor : MediaDescriptor
    {

        public bool ShouldBeLoadedAgain { get; set; }

        public ShaderDescriptor(String name)
            : base(ProjectManager.Instance.ActiveProject.PathMedia
                + Path.DirectorySeparatorChar + name + ".hlsl")
        {

        }

        public override void createFile()
        {
            //create basic template for hlsl file
            ShouldBeLoadedAgain = true;
            string baseFldr = FilePath.Substring(0, FilePath.LastIndexOf("\\"));
            Directory.CreateDirectory(baseFldr);

            FileStream fileStream = File.Create(FilePath);
            StreamWriter writer = new StreamWriter(fileStream);

            writer.WriteLine("sampler2D sample;");
            writer.WriteLine("float time; //this value is auto-set by Whiskey2D");
            writer.WriteLine("float2 mousePosition; //this value is auto-set by Whiskey2D");
            writer.WriteLine("float2 cameraTranslation; //this value is auto-set by Whiskey2D");
            writer.WriteLine("float cameraZoom; //this value is auto-set by Whiskey2D");
            writer.WriteLine("");
            writer.WriteLine("//transform a regular position to a game position");
            writer.WriteLine("float2 translate (float2 v){");
            writer.WriteLine("\t return (v - cameraTranslation) / cameraZoom;");
            writer.WriteLine("}");
            writer.WriteLine("");
            writer.WriteLine("struct PSInput");
            writer.WriteLine("{");
            writer.WriteLine("\tfloat2 Texcoord : TEXCOORD0;");
            writer.WriteLine("};");
            writer.WriteLine("");
            writer.WriteLine("float4 ps_main( PSInput PSin )  : COLOR0");
            writer.WriteLine("{");
            writer.WriteLine("\tfloat4 color = tex2D(sample, PSin.Texcoord);");
            writer.WriteLine("\treturn color;");
            writer.WriteLine("}");
            writer.WriteLine("");
            writer.WriteLine("technique DefaultTechnique");
            writer.WriteLine("{");
            writer.WriteLine("\tpass p0");
            writer.WriteLine("\t{");
            writer.WriteLine("\t\tPixelShader = compile ps_2_0 ps_main();");
            writer.WriteLine("\t}");
            writer.WriteLine("}");


            writer.Close();

            ShaderBuilder sb = new ShaderBuilder();
            sb.buildShader(FilePath, FilePath.Replace(".hlsl", ".mgfx"));

        }

        public override string ToString()
        {
            return Name.Replace(".hlsl", "");
        }

    }
}
