using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Actions;

namespace WhiskeyEditor.Backend.Managers
{
    class ActionManager
    {

        private static ActionManager instance = new ActionManager();
        public static ActionManager Instance { get { return instance; } }

        private ActionManager()
        {

        }

        /// <summary>
        /// Run an action
        /// </summary>
        /// <param name="action"></param>
        public void run(WhiskeyAction action)
        {
            if (action == null)
                throw new ArgumentNullException("Action");

            //TODO: put the action on a thread system

            action.Effect();

        }

        

    }
}
