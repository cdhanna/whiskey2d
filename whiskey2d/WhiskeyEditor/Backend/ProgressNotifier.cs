using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    public class DefaultProgressNotifier : ProgressNotifier
    {
        public float Progress { get; set; }
    }
    public interface ProgressNotifier
    {
        float Progress { get; set; }
    }
}
