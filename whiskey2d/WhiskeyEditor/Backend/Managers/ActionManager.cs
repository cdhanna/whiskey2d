using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend.Actions;
using System.Threading;

namespace WhiskeyEditor.Backend.Managers
{

    public delegate void ActionChangedEventHandler (object sender, ActionChangedEventArgs args);
    public class ActionChangedEventArgs : EventArgs
    {
        public WhiskeyAction Action { get; private set; }
        public ActionChangedEventArgs(WhiskeyAction action)
        {
            Action = action;
        }
    }

    class ActionManager
    {

        private static ActionManager instance = new ActionManager();
        public static ActionManager Instance { get { return instance; } }


        public event ActionChangedEventHandler ActionChanged = new ActionChangedEventHandler((s, a) => { });
        private Thread backActions;
        private Queue<WhiskeyAction> actions;

        private ActionManager()
        {
            actions = new Queue<WhiskeyAction>();
           // backActions = new Thread(process);
            //backActions.Name = "ActionThread";
          //  backActions.Start();
        }

        private void process()
        {
            while (true)
            {

                if (actions.Count > 0)
                {
                    WhiskeyAction action = actions.Dequeue();
                    action.Progress = 0;
                    ActionChanged(this, new ActionChangedEventArgs(action));

                    ActionChangedEventHandler actionHand = new ActionChangedEventHandler((s, a) =>
                    {
                        ActionChanged(this, a);
                    });

                    action.Changed += actionHand;
                    action.Effect();
                    action.Changed -= actionHand;
                    ActionChanged(this, new ActionChangedEventArgs(null));


                }
            }

        }

        /// <summary>
        /// Run an action
        /// </summary>
        /// <param name="action"></param>
        public void run(WhiskeyAction action)
        {
            if (action == null)
                throw new ArgumentNullException("Action");
           // actions.Enqueue(action);
            //TODO: put the action on a thread system (so that only one runs at a time)
            action.Progress = 0;
            ActionChanged(this, new ActionChangedEventArgs(action));

            ActionChangedEventHandler actionHand = new ActionChangedEventHandler((s, a) =>
            {
                ActionChanged(this, a);
            });

            action.Changed += actionHand;

            Thread actionThread = new Thread(() =>
            {
                action.Effect();
                action.Changed -= actionHand;

                action.Progress = 1;
                ActionChanged(this, new ActionChangedEventArgs(action));
                ActionChanged(this, new ActionChangedEventArgs(null));

            });
            actionThread.Name = "ACTION: " + action.Name;
            actionThread.Start();

            

        }

        

    }
}
