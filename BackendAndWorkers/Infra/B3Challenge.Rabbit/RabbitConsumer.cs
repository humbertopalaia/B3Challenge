using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace B3Challenge.Rabbit
{

    public class ReceivedEventArgs : EventArgs
    {
        public string Message
        {
            get; set;
        }
    }

    public class RabbitConsumer : RabbitBase, IRabbitConsumer
    {
        public delegate void OnReceivedEventHandler(object sender, ReceivedEventArgs e);

        public event OnReceivedEventHandler OnReceived;
        public RabbitConsumer(string host, string username, string password) : base(host, username, password)
        {
        }

        public void ConsumeQueue(string queueName)
        {
            _channel.QueueDeclare(queue: queueName,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
           arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;
            _channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            if (OnReceived != null)
                OnReceived(this, new ReceivedEventArgs { Message = Encoding.UTF8.GetString(e.Body.ToArray()) });
        }
    }
}
