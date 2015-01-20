using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;

using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend
{
    class TypeBank
    {
        private static TypeBank instance = new TypeBank();
        public static TypeBank Instance { get { return instance; } }



        //private List<RealType> primitives;
        private Dictionary<string, RealType> primitiveMap;


        private TypeBank()
        {
            primitiveMap = new Dictionary<string, RealType>();

            registerPrimitive("Int", new RealType(typeof(Int32), 0));
            registerPrimitive("String", new RealType(typeof(String), ""));
            registerPrimitive("Boolean", new RealType(typeof(Boolean), false));
            registerPrimitive("Char", new RealType(typeof(Char), '?'));
            registerPrimitive("Key", new RealType(typeof(Microsoft.Xna.Framework.Input.Keys), Microsoft.Xna.Framework.Input.Keys.A));
            registerPrimitive("Float", new RealType(typeof(float), 0f));
            registerPrimitive("Double", new RealType(typeof(double), 0.0));
            registerPrimitive("Color", new RealType(typeof(Color), Color.White));
            registerPrimitive("Vector", new RealType(typeof(Vector), Vector.One));
            registerPrimitive("Sprite", new RealType(typeof(Sprite), new Sprite()));
        }


        private TypeVal lookUpAndClone(string displayName)
        {
            //check primitives
            if (primitiveMap.ContainsKey(displayName))
                return primitiveMap[displayName].clone();

            //check other types
            List<TypeDescriptor> types = getTypeDescriptors();

            foreach (TypeDescriptor type in types)
            {
                if (type.ClassName.Equals(displayName))
                {
                    TypeVal tv = new InstanceType(type, type.createNull() );
                    return tv;
                }
            }

            return null;

            
        }

        private void registerPrimitive(string displayName, RealType prim)
        {
            if (primitiveMap.ContainsKey(displayName))
                primitiveMap[displayName] = prim;
            else primitiveMap.Add(displayName, prim);
        }

        private List<TypeDescriptor> getTypeDescriptors()
        {
            List<FileDescriptor> typeFiles = FileManager.Instance.FileDescriptors.Where(f => f is TypeDescriptor).ToList();
            List<TypeDescriptor> types = new List<TypeDescriptor>();
            typeFiles.ForEach(f => types.Add((TypeDescriptor)f));
            return types;
        }

        public string[] getPrimitiveNames()
        {
            return primitiveMap.Keys.ToArray();
        }
        public string[] getObjectNames()
        {


            //let the jenkyness roll like bob dole 

            List<TypeDescriptor> typeFiles = getTypeDescriptors();
            List<string> names = new List<string>();
            typeFiles.ForEach(t => names.Add( t.ClassName));

            return names.ToArray();


        }

        //public TypeVal createNewInstance(TypeVal tv)
        //{
        //    return tv.clone();
        //}
        public TypeVal createNewInstance(string name)
        {
            TypeVal tv = lookUpAndClone(name);
            return tv;
            //return tv.clone();
        }

    }
}
