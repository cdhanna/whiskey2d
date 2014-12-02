using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WhiskeyEditor.Backend.Actions
{
    /// <summary>
    /// The Action interface specifies data needed for a repeatable action to run over and over again.
    /// </summary>
    interface WhiskeyAction
    {
        /// <summary>
        /// The name of the action
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// The image associated with the action, or null, if there is no image
        /// </summary>
        Image Image { get; }

        /// <summary>
        /// The actual content of the action. 
        /// </summary>
        NoArgFunction Effect { get; }

        C generateControl<C>();
    }
}
