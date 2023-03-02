using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Challenge.Rabbit
{
    public abstract class RabbitBase
    {
        protected readonly string _host;
        protected readonly string _username;
        protected readonly string _password;
        protected IConnection _connection;
        protected IModel _channel;


        public RabbitBase(string host, string username, string password)
        {
            _host = host;
            _username = username;
            _password = password;

         
        }

        protected void Connect()
        {
            var factory = new ConnectionFactory { HostName = _host, UserName = _username, Password = _password };


            if (_connection == null || !_connection.IsOpen)
            {
                _connection = factory.CreateConnection();
            }

            if (_channel == null || !_channel.IsOpen)
            {
                _channel = _connection.CreateModel();
            }
        }

    }
}
