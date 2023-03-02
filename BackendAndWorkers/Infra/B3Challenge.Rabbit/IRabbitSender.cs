using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Rabbit
{
    public interface IRabbitSender
    {
        void QueueMessage(OperationMessage operationMessage, string queueName);
    }
}
