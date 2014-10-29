using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;


namespace WhiskeyEditor
{
    class Settings
    {
        private const string PROP_CURRENT_PROJ = "PROP_CURRENT_PROJ";


        static PropertiesFiles props;

        static Settings()
        {
            props = new PropertiesFiles(ResourceFiles.SettingsFile);
        }


        public static string CurrentProject
        {
            get
            {
                return props.get(PROP_CURRENT_PROJ);
            }
            set
            {
                props.set(PROP_CURRENT_PROJ, value);
                props.Save();
            }
        }


    }
}
