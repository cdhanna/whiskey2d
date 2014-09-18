using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Expr
{


    public abstract class Gob
    {

        List<Worker<Gob>> workers;

        public Gob()
        {
            workers = new List<Worker<Gob>>();
        }

        public void getWorker()
        {
        }

       


        //public List<W> getWorkers<W>() where W : Worker<Gob>, new(){

        //    List<W> gobs = new List<W>();

        //    return gobs;

        //}

    }
}
