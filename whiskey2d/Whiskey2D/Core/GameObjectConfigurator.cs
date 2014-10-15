using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Whiskey2D.Core
{
    public class GameObjectConfigurator
    {

        private class GameObjectPropertyPair
        {
            public string typeName;
            public string propName;

            public GameObjectPropertyPair(string typeName, string propName)
            {
                this.typeName = typeName;
                this.propName = propName;
            }

            public override bool Equals(object obj)
            {
                if (obj is GameObjectPropertyPair){
                    GameObjectPropertyPair other = (GameObjectPropertyPair) obj;
                    return other.typeName.Equals(typeName) && other.propName.Equals(propName);
                }
                return false;
            }

            public override int GetHashCode()
            {
                return typeName.GetHashCode() * propName.GetHashCode();
            }
        }


        private static GameObjectConfigurator instance = new GameObjectConfigurator();
        public static GameObjectConfigurator getInstance()
        {
            return instance;
        }


        private Dictionary<GameObjectPropertyPair, object> valueTable;       

        private GameObjectConfigurator()
        {
            valueTable = new Dictionary<GameObjectPropertyPair, object>();
            try
            {
                Sprite s = new Sprite();
                s.Scale *= 50;
                setInitialValueFor("GameObject", "Sprite", s);
                setInitialValueFor("GameObject", "X", 0f);
                setInitialValueFor("GameObject", "Y", 0f);
                setInitialValueFor("GameObject", "ID", 0);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static object getInitialValueFor(string qualifiedName, string propertyName)
        {
            try
            {
                object obj = getInstance().getInitialValueFor_(qualifiedName, propertyName);
                //object newObj = NClone.Clone.ObjectIgnoringConventions(obj);
                object newObj = Nuclex.Cloning.ReflectionCloner.ShallowFieldClone(obj);
                return newObj;


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public object getInitialValueFor_(string qualifiedName, string propertyName)
        {
           
            GameObjectPropertyPair pair = new GameObjectPropertyPair(qualifiedName, propertyName);
            if (valueTable.ContainsKey(pair))
            {
                return valueTable[pair];
            }
            else
            {
                return null;
            }

        }

        public void setInitialValueFor(string qualifiedName, string propertyName, object value)
        {
            GameObjectPropertyPair pair = new GameObjectPropertyPair(qualifiedName, propertyName);
            if (valueTable.ContainsKey(pair))
            {
                valueTable[pair] = value;
            }
            else
            {
                valueTable.Add(pair, value);
            }

        }


    }
}
