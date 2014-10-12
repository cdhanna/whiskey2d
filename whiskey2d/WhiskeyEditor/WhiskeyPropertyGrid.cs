using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;

namespace WhiskeyEditor
{
    public class WhiskeyPropertyGrid : PropertyGrid
    {
        public WhiskeyPropertyGrid() : base()
        {
            //this.HiddenProperties = new string[] {"ImageSize" };
            
        }

        //protected override void applyFilters(object obj)
        //{

        //    List<string> hidePropList = new List<string>();

        //    PropertyInfo[] propInfos = obj.GetType().GetProperties();
        //    foreach (PropertyInfo p in propInfos)
        //    {
        //        MethodInfo m = p.GetSetMethod();
        //        if (m == null)
        //        {
        //            hidePropList.Add(p.Name);

        //        }
        //    }


        //    //this.HiddenProperties = hidePropList.ToArray();

        //}


    }
}
