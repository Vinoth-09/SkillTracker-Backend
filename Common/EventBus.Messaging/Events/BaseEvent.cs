using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messaging.Events
{
    public class BaseEvent
    {
        public BaseEvent()
        {
            Id = Guid.NewGuid();
        }

        public BaseEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
