using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend.Actions
{
    /// <summary>
    /// The Action interface specifies data needed for a repeatable action to run over and over again.
    /// </summary>
    public interface WhiskeyAction : ProgressNotifier
    {
        /// <summary>
        /// The name of the action
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// The image associated with the action, or null, if there is no image
        /// </summary>
        Image Image { get; }

     
        event ActionChangedEventHandler Changed;

        /// <summary>
        /// The actual content of the action. 
        /// </summary>
        OneArgFunction Effect { get; }

        C generateControl<C>();
    }
}
