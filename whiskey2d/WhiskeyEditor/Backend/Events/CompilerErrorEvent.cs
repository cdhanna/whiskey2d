using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallMVC;
using System.CodeDom.Compiler;

namespace WhiskeyEditor.Backend.Events
{
    class CompilerPostEvent : Event
    {

        private CompilerErrorCollection errors;

        public CompilerPostEvent(CompilerErrorCollection errors)
        {
            this.errors = errors;
        }

        public CompilerErrorCollection Errors { get { return this.errors; } }

    }
}
