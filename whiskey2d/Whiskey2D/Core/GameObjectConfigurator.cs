using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Whiskey2D.Core
{

    /// <summary>
    /// The GameObjectConfigurator assigns values to game objects upon initialization 
    /// </summary>
    public class GameObjectConfigurator
    {

        /// <summary>
        /// this inner class combines a property and an object type. Used for hashing
        /// </summary>
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
                //assign default values for base game object
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

        /// <summary>
        /// Gets the initial value for a property
        /// </summary>
        /// <param name="qualifiedName">the type name of the game object </param>
        /// <param name="propertyName">the name of the property</param>
        /// <returns>the default value of the property </returns>
        public static object getInitialValueFor(string qualifiedName, string propertyName)
        {
            try
            {
                object obj = getInstance().getInitialValueFor_(qualifiedName, propertyName);
                object newObj = Nuclex.Cloning.ReflectionCloner.ShallowFieldClone(obj);
                return newObj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets the initial value for a property
        /// </summary>
        /// <param name="qualifiedName">the type name of the game object </param>
        /// <param name="propertyName">the name of the property</param>
        /// <returns>the default value of the property </returns>
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

        /// <summary>
        /// Set the initial value for a property
        /// </summary>
        /// <param name="qualifiedName">the type name of the game object</param>
        /// <param name="propertyName">the name of the property</param>
        /// <param name="value">the value to set the property to</param>
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
