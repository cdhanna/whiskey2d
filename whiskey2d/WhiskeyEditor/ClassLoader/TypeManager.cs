using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Whiskey2D.Core;
using System.CodeDom;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;



namespace WhiskeyEditor.ClassLoader
{

    delegate void DescriptorAddedListener(GameObjectDescriptor descr, Type type);

    class TypeManager
    {

        public List<GameObjectDescriptor> descritors;
        List<Type> gobTypes;
        Dictionary<GameObjectDescriptor, Type> descToTypeMap;
        Dictionary<Type, GameObjectDescriptor> typeToDescMap;


        List<DescriptorAddedListener> addListeners;

        private static TypeManager instance = new TypeManager();
        public static TypeManager getInstance()
        {
            return instance;
        }

        private TypeManager()
        {
            descritors = new List<GameObjectDescriptor>();
            gobTypes = new List<Type>();
            addListeners = new List<DescriptorAddedListener>();
            descToTypeMap = new Dictionary<GameObjectDescriptor, Type>();
            typeToDescMap = new Dictionary<Type, GameObjectDescriptor>();
        }



        public Type updateDescriptor(GameObjectDescriptor descr)
        {
            if (descritors.Contains(descr)){

                gobTypes.Remove( descToTypeMap[descr] );
                typeToDescMap.Remove(descToTypeMap[descr]);
                Type oldType = descToTypeMap[descr];

                Type type = convertDescriptorToType(descr);
                convertDescriptorToFile(descr);
                gobTypes.Add(type);
                typeToDescMap.Add(type, descr);
                descToTypeMap[descr] = type;

                //update type+instances
                if (GameManager.Objects != null && GameManager.Objects is EditorObjectManager)
                {
                    replace(oldType, type);
                    EditorObjectManager eom = (EditorObjectManager)GameManager.Objects;
                    List<GameObject> olds = eom.getAllObjectsNotOfType<EditorObjects.EditorGameObject>();
                    //
                    olds.ForEach((g) => { g.close(); });
                    List<GameObject> gobs = updateObjects(olds);
                    
                    //eom.addObjects(gobs);
                }

                foreach (DescriptorAddedListener da in addListeners)
                {
                    da(descr, type);
                }

                return type;

            } else {
                return addDescriptor(descr);
            }

        }

        public Type addDescriptor(GameObjectDescriptor descr)
        {
            descritors.Add(descr);
            Type type = convertDescriptorToType(descr);
            convertDescriptorToFile(descr);
            gobTypes.Add(type);
            descToTypeMap.Add(descr, type);
            typeToDescMap.Add(type, descr);
            foreach (DescriptorAddedListener da in addListeners)
            {
                da(descr, type);
            }
            return type;
        }

        public void addDescriptorAddedListener( DescriptorAddedListener addedListener )
        {
            addListeners.Add(addedListener);
        }

        public void replace(Type oldType, Type newType)
        {
            List<Type> badTypes = new List<Type>();

            foreach (Type type in gobTypes)
            {
                PropertyInfo[] props = type.GetProperties();
              
                foreach (PropertyInfo prop in props)
                {
                    if (prop.PropertyType.Equals(oldType) || prop.PropertyType.IsSubclassOf(oldType))
                    {
                        badTypes.Add(type);
                        break;
                    }
                }

            }

            foreach (Type type in badTypes)
            {
                //recreate this one
                Type modifiedType = recreate(type, oldType, newType);
                replace(type, modifiedType);
            }


        }

        private Type recreate(Type type, Type oldType, Type newType)
        {

            GameObjectDescriptor descr = typeToDescMap[type];

            foreach (PropertyDescriptor p in descr.Properties)
            {

                if (p.Type.Equals(oldType) || p.Type.IsSubclassOf(oldType))
                {
                    //convertProperty(p, oldType, newType);
                    p.Value = updateObject(p.Value, oldType, newType);
                    p.Type = newType;
                }

            }


            Type modType = updateDescriptor(descr);

            return modType;
        }

        private void convertProperty(PropertyDescriptor prop, Type oldType, Type newType)
        {
           
            object oldValue = prop.Value;

            object newValue = instantiate(newType);

            PropertyInfo[] newProperties = newType.GetProperties();
            PropertyInfo[] oldProperties = oldType.GetProperties();

            foreach (PropertyInfo newProp in newProperties)
            {
                foreach (PropertyInfo oldProp in oldProperties)
                {
                    if (oldProp.Name.Equals(newProp.Name) && oldProp.PropertyType.Equals(newProp.PropertyType))
                    {
                        if (newProp.SetMethod != null)
                        {
                            newProp.SetValue(newValue, oldProp.GetValue(oldValue));
                        }
                        break;
                    }

                }
            }

            prop.Value = newValue;
            prop.Type = newType;

        }

        public List<GameObject> updateObjects(List<GameObject> objList)
        {

            List<GameObject> newObjList = new List<GameObject>();

            foreach (GameObject obj in objList)
            {
                Type type = obj.GetType();

                foreach (Type gobType in gobTypes)
                {
                    if (gobType.FullName.Equals(type.FullName))
                    {

                        if (gobType.Equals(type))
                        {
                            newObjList.Add(obj);
                        }
                        else
                        {
                            Console.WriteLine(type.FullName + " convert to " + gobType.FullName);
                            newObjList.Add((GameObject)updateObject(obj, type, gobType));
                        }
                    }
                }


            }


            return newObjList;

        }

        private object updateObject(object oldValue, Type oldType, Type newType)
        {
            object newValue = instantiate(newType);
            PropertyInfo[] newProperties = newType.GetProperties();
            PropertyInfo[] oldProperties = oldType.GetProperties();

            foreach (PropertyInfo newProp in newProperties)
            {
                foreach (PropertyInfo oldProp in oldProperties)
                {
                    if (oldProp.Name.Equals(newProp.Name) && oldProp.PropertyType.Equals(newProp.PropertyType))
                    {
                        if (newProp.SetMethod != null)
                        {
                            newProp.SetValue(newValue, oldProp.GetValue(oldValue));
                        }
                        break;
                    }

                }
            }


            return newValue;
        }


        //public void recReplace(GameObjectDescriptor oldDesc, GameObjectDescriptor newDesc, GameObjectDescriptor currentDescr)
        //{

        //    List<PropertyDescriptor> props = currentDescr.Properties;

        //    foreach (PropertyDescriptor prop in props)
        //    {
        //        if (prop.Type.IsSubclassOf(typeof(GameObject)))
        //        {

        //            //get descriptor for propType

        //            GameObjectDescriptor propDescr = typeToDescMap[ prop.Type] ;
        //            Type oldType = descToTypeMap[oldDesc];

        //            if (prop.Type.Equals(oldType))
        //            {
        //                //do something

        //                //prop.Type = newDesc;
        //                //prop.Value = convertValue(prop.Value, oldDesc, newDesc);
        //                Console.WriteLine("CHANGE " + currentDescr.Name + " . " + prop.Name);

        //                //Type newType = addDescriptor(newDesc);
        //                //prop.Type = newType;
        //                //prop.Value = convertValue(prop.Value, oldType, newType);

        //            }
        //            else
        //            {
        //                recReplace(oldDesc, newDesc, propDescr);

        //            }


        //        }

        //    }

        //}


        ///// <summary>
        ///// Replaces all references to the old type, with a reference to the new type.
        ///// After this function runs, all of the descriptors that contained a property to the old type, will be updated.
        ///// </summary>
        ///// <param name="oldType"></param>
        ///// <param name="type"></param>
        //public void replace(Type oldType, Type type)
        //{
        //    //all gobds
        //    foreach (GameObjectDescriptor desc in descritors)
        //    {
        //        List<PropertyDescriptor> badProps = new List<PropertyDescriptor>();

        //        //all props
        //        foreach (PropertyDescriptor prop in desc.Properties)
        //        {
        //            //is the property of oldtype?
        //            if (prop.Type.Equals(oldType))
        //            {
        //                badProps.Add(prop);
        //            }
        //        }

        //        //remove bad properties, add a new one of correct type
        //        foreach (PropertyDescriptor prop in badProps)
        //        {
        //            desc.Properties.Remove(prop);

        //            object newValue = convertValue(prop.Value, oldType, type);
        //            PropertyDescriptor newProp = new PropertyDescriptor(prop.Name, type, newValue);
        //            desc.Properties.Add(newProp);
        //        }

        //        //if (badProps.Count > 0)
        //        //{
        //        //    updateDescriptor(desc);
        //        //}

        //    }

        //    ////all descriptors have been updated
        //    foreach (GameObjectDescriptor descr in descritors)
        //    {
        //        updateDescriptor(descr);
        //    }

        //}

        ///// <summary>
        ///// Takes a value of an old type, and tries to convert it to a value of the new type. 
        ///// This will return null if nothing can be done
        ///// </summary>
        ///// <param name="value"></param>
        ///// <param name="oldType"></param>
        ///// <param name="newType"></param>
        ///// <returns></returns>
        //public object convertValue(object value, Type oldType, Type newType)
        //{

        //    object newValue = instantiate(newType);

        //    PropertyInfo[] newProps = newType.GetProperties();
        //    PropertyInfo[] oldProps = oldType.GetProperties();
        //    foreach (PropertyInfo newProp in newProps)
        //    {
        //        foreach (PropertyInfo oldProp in oldProps)
        //        {
        //            if (oldProp.Name.Equals(newProp.Name) && oldProp.PropertyType.Equals(newProp.PropertyType))
        //            {
        //                if (newProp.SetMethod != null)
        //                {
        //                    newProp.SetValue(newValue, oldProp.GetValue(value));
        //                }
        //            }

        //        }


        //    }

        //    return newValue;
        //}


        public static object instantiate(Type gobType)
        {
           
            try
            {
                object obj = gobType.GetConstructor(new Type[] { }).Invoke(new object[] { });
                return obj;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                } else throw e;

            }
        }


        public static Type convertDescriptorToType(GameObjectDescriptor desc)
        {
            Assembly asm = desc.generateSourceInMem();
            Type type = asm.GetType(desc.QualifiedName);

            return type;

        }




        public static string convertDescriptorToFile(GameObjectDescriptor desc)
        {

            string filePath = desc.QualifiedName;
            filePath = filePath.Replace('.', '\\');
            filePath = filePath + ".cs";

            desc.generateSource(filePath);

            return filePath;
        }

        public static Type convertFileToType(string filePath)
        {

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters options = new CompilerParameters();
            options.GenerateInMemory = true;
            options.GenerateExecutable = false;
            // options.LinkedResources.Add("Whiskey2D.Core");
            options.ReferencedAssemblies.Add("Whiskey2D_core.dll");
            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.Add("System.Linq.dll");


            CompilerResults results = provider.CompileAssemblyFromFile(options, filePath);
            foreach (String line in results.Output)
            {
                Console.WriteLine(line);
            }

            Assembly asm = results.CompiledAssembly;

            string className = filePath.Replace('\\', '.');
            className = className.Replace(".cs", "");

            return asm.GetType(className);
        }

        public static GameObjectDescriptor convertTypeToDescriptor(Type gobType)
        {

            if (gobType.IsSubclassOf(typeof(GameObject))){


                string nameSpace = gobType.Namespace;
                string name = gobType.Name;

                GameObjectDescriptor gobd = new GameObjectDescriptor(nameSpace, name);
                GameObject gobInstance = (GameObject)gobType.GetConstructor(new Type[] { }).Invoke(new object[] { });


                PropertyInfo[] pis = gobType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                
                foreach (PropertyInfo p in pis)
                {

                    string pName = p.Name;
                    Type pType = p.PropertyType;
                    object pValue = p.GetValue(gobInstance);
                    

                    PropertyDescriptor pd = new PropertyDescriptor(pName, pType, pValue );
                    gobd.addProperty(pd);
                }

                gobInstance.close();
                return gobd;

            }


            return null;
        }


    }
}
