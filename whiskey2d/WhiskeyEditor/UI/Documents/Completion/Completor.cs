using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.TextEditor.Gui;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using System.Reflection;

namespace WhiskeyEditor.UI.Documents.Completion
{
    class Completor
    {
        private static List<ICompletionData> emptyData = new List<ICompletionData>();

        private List<ICompletionData> staticData;
        private List<ICompletionData> instanceData;

        private string staticTrigger;
        private Type target;

        public Completor(Type targetType)
        {
            target = targetType;
            staticData = new List<ICompletionData>();
            instanceData = new List<ICompletionData>();

            refreshData();

            

        }

        private void addData(string text)
        {
            this.addData(text, "");
        }
        private void addData(string text, string desc)
        {
            this.addData(text, desc, 0);
        }
        private void addData(string text, string desc, int imgIndex)
        {
            DefaultCompletionData data = new DefaultCompletionData(text, desc, imgIndex);
            instanceData.Add(data);
            
        }
        private void addStaticData(string text)
        {
            this.addStaticData(text, "");
        }
        private void addStaticData(string text, string desc)
        {
            this.addStaticData(text, desc, 0);
        }
        private void addStaticData(string text, string desc, int imgIndex)
        {
            DefaultCompletionData data = new DefaultCompletionData(text, desc, imgIndex);
            staticData.Add(data);
        }


        public void refreshData()
        {
            staticData.Clear();
            instanceData.Clear();
            
            staticTrigger = target.Name;
            if (!target.IsEnum)
            {
                //get Properties
                PropertyInfo[] staticProps = target.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
                foreach (PropertyInfo p in staticProps)
                {
                    addStaticData(p.Name);
                }
                PropertyInfo[] instProps = target.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (PropertyInfo p in instProps)
                {
                    addData(p.Name);
                }

                //get methods
                MethodInfo[] staticMethods = target.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
                foreach (MethodInfo m in staticMethods)
                {
                    addStaticData(m.Name + "()");
                }

                MethodInfo[] instMethods = target.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (MethodInfo m in instMethods)
                {
                    addData(m.Name + "()");
                }
            }

            //get enumdata
            if (target.IsEnum)
            {
                
                foreach (object o in target.GetEnumValues())
                {
                    addStaticData(o.ToString());
                }
            }

        }


        public List<ICompletionData> getData(string token, string tokenType)
        {
            if (token != null && token.EndsWith(staticTrigger))
            {
                return staticData;
            }
            else if (tokenType != null && tokenType.Equals(staticTrigger))
            {
                return instanceData;
            }
            else return emptyData;
        }


    }
}
