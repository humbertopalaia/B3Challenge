using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static B3Challenge.Rabbit.RabbitConsumer;

namespace B3Challenge.Rabbit
{
    public interface IRabbitConsumer
    {
        public event OnReceivedEventHandler OnReceived;

        public void ConsumeQueue(string queueName);
    }
}
