using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messaging.Events
{
    public class AddProfileEvent : BaseEvent
    {
        public string Data { get; set; }
    }
}
