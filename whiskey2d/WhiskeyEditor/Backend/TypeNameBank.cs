using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
namespace WhiskeyEditor.Backend
{
    public class TypeNameBank
    {

        private static TypeNameBank instance = new TypeNameBank();
        public static TypeNameBank Instance { get { return instance; } }

        private Dictionary<string, string> names;
        private Dictionary<string, object> defaults;

        private Dictionary<string, TypeVal> typeVals;

        private TypeNameBank()
        {
            names = new Dictionary<string, string>();
            defaults = new Dictionary<string, object>();
            typeVals = new Dictionary<string, TypeVal>();

            registerType("Int", typeof(Int32), 0);
            registerType("String", typeof(String), "");
            registerType("Boolean", typeof(Boolean), false);
            registerType("Char", typeof(Char), '?');
            registerType("Float", typeof(float), 0f);
            registerType("Double", typeof(double), 0.0);
            
            registerType("Color", typeof(Color), Color.White);
            registerType("Vector", typeof(Vector), Vector.One);
            registerType("Sprite", typeof(Sprite), new Sprite() );


        }


        private void registerType(string displayName, Type type, object defaultValue)
        {
            names.Add(displayName, type.FullName);
            defaults.Add(type.FullName, defaultValue);
            typeVals.Add(displayName, new RealType(type, defaultValue));
        }
        private void registerType(string displayName, TypeDescriptor type)
        {
            throw new NotImplementedException();
        }


        public List<string> getTypeDisplayNames()
        {
            return names.Keys.ToList();
        }

        public List<string> getTypeFullNames()
        {
            return names.Values.ToList();
        }

        public string getFullName(string displayName)
        {
            if (names.ContainsKey(displayName))
            {
                return names[displayName];
            } else throw new WhiskeyException("There is no type name for " + displayName);
        }

        public bool hasDisplayName(string displayName)
        {
            return names.ContainsKey(displayName);
        }


        public TypeVal createTypeVal(string displayName)
        {
            if (hasDisplayName(displayName))
            {

                TypeVal val = typeVals[displayName];

                return val.clone();

            }
            else throw new WhiskeyException("There is no type name for " + displayName);

        }

    }
}
