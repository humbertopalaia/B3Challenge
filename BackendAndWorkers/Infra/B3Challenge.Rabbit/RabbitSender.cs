using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace B3Challenge.Rabbit
{
    public class RabbitSender : RabbitBase, IRabbitSender
    {
        public RabbitSender(string host, string username, string password) : base(host, username, password)
        {
        }

        public void QueueMessage(OperationMessage operationMessage, string queueName)
        {
            Connect();

            if (_channel != null && _channel.IsOpen)
            {
                _channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                var message = JsonSerializer.Serialize(operationMessage);
                var body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(exchange: string.Empty,
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}