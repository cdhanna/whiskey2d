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
using WhiskeyEditor.Project;


namespace WhiskeyEditor.ClassLoader
{

    /// <summary>
    /// used to notify listeners
    /// </summary>
    /// <param name="descr">The descriptor that has changed</param>
    /// <param name="type">The type of that descriptor </param>
    delegate void DescriptorAddedListener(GameObjectDescriptor descr, Type type);

    /// <summary>
    /// The Tyep Manager is responsible for converting descriptors to/from sourcecode and assemblies 
    /// </summary>
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


        /// <summary>
        /// Update the descriptor. 
        /// If the descriptor is new, this method will call addDescriptor on it. 
        /// Otherwise, it will auto-update all class references and instances of the old type. 
        /// </summary>
        /// <param name="descr"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add the descriptor to the type manager's set of descriptors. Creates the type and soruce code. 
        /// </summary>
        /// <param name="descr"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add a listener that will be notified everytime the gameobject descriptors are added/updated
        /// </summary>
        /// <param name="addedListener"></param>
        public void addDescriptorAddedListener( DescriptorAddedListener addedListener )
        {
            addListeners.Add(addedListener);
        }

        /// <summary>
        /// replace all old type refs with the new type. 
        /// This does not replace instances of the old type
        /// </summary>
        /// <param name="oldType">An old type that is a subclass of GameObject</param>
        /// <param name="newType">the updated type that is still a subclass of GameObject</param>
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

        /// <summary>
        /// Convert all of the gameobjects of an outdated type to the updated type
        /// </summary>
        /// <param name="objList">A set of gameObjects to update </param>
        /// <returns>An updated list</returns>
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

        /// <summary>
        /// Create a GameObject of the given type
        /// </summary>
        /// <param name="gobType">A type that is a subclass of GameObject</param>
        /// <returns>A gameObject </returns>
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


        /// <summary>
        /// Convert the given descriptor to a compiled Type
        /// </summary>
        /// <param name="desc">A non null gameobject descr</param>
        /// <returns>the compiled type of the given descr</returns>
        public static Type convertDescriptorToType(GameObjectDescriptor desc)
        {
            Assembly asm = desc.generateSourceInMem();
            Type type = asm.GetType(desc.QualifiedName);

            return type;

        }

        /// <summary>
        /// Convert the given descriptor to readable sourcecode.
        /// The source code will be put in a folder equal to the desc's nameSpace.Name
        /// </summary>
        /// <param name="desc">A non null gameobject descr</param>
        /// <returns>the filepath to the source code</returns>
        public static string convertDescriptorToFile(GameObjectDescriptor desc)
        {

            string filePath = ProjectManager.Instance.ActiveProject.PathSrc + Path.DirectorySeparatorChar + desc.QualifiedName;
            filePath = filePath.Replace('.', '\\');
            filePath = filePath + ".cs";

            desc.generateSource(filePath);

            return filePath;
        }

        /// <summary>
        /// Convert a .cs file at the given path to a compiled Type
        /// </summary>
        /// <param name="filePath">The path to a .cs file</param>
        /// <returns>The compiled type of the code </returns>
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

        /// <summary>
        /// Convert the given type into a gameobject descr
        /// </summary>
        /// <param name="gobType">A type that is a subclass of GameObject</param>
        /// <returns>A new GameObject descr</returns>
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
