using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.TestFrame;
using WhiskeyEditor;
using WhiskeyEditor.ClassLoader;


namespace WhiskeyEditor.TestFrame.Tests
{
    class ConfigTest : Tester
    {

        [Test]
        public void testMethod()
        {

            GameObjectDescriptor desc = new GameObjectDescriptor("Project", "ConfigTest");

            TypeManager.convertDescriptorToFile(desc);
           
            
            TypeManager.convertDescriptorToType(desc);

        }



        protected override Project.Project getStartProject()
        {
            return Project.ProjectManager.Instance.createNewProject("TestProject", "testproj");
        }
    }
}
