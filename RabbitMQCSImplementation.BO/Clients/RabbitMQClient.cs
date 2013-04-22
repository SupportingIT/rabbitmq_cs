using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCSImplementation.BO.Clients
{
    public class RabbitMQClient : IRabbitMQClient
    {
        #region Private Properties

        private string hostName;
        private IConnection connection;

        private const bool TurnOffAcknowledgments = false;
        private const bool Durable = true;

        #endregion Private Properties

        #region Public Properties

        public string Exchange { get; private set; }
        public string RoutingKey { get; private set; }
        public IModel Channel { get; private set; }
        public QueueingBasicConsumer Consumer { get; private set; }

        #endregion Public Properties

        #region Public Constructors

        public RabbitMQClient(string hostName)
        {
            this.hostName = hostName;
            this.Exchange = "exch";
            this.RoutingKey = "key";

            Initialize();
        }

        #endregion Public Constructors

        #region Public Methods

        public void CreateQueue(string queueName)
        {
            this.Channel.ExchangeDeclare(this.Exchange, ExchangeType.Direct);
            this.Channel.QueueDeclare(queueName, RabbitMQClient.Durable, false, false, null);
            this.Channel.QueueBind(queueName, this.Exchange, this.RoutingKey, null);
        }

        public void CreateQueueConsumer(string queueName)
        {
            this.Consumer = new QueueingBasicConsumer(this.Channel);
            this.Channel.QueueDeclare(queueName, RabbitMQClient.Durable, false, false, null);
            this.Channel.BasicConsume(queueName, RabbitMQClient.TurnOffAcknowledgments, this.Consumer);
            this.Channel.BasicQos(0, 1, false);
        }

        public void Close()
        {
            this.Channel.Close();
            this.connection.Close();
        }

        #endregion Public Methods

        #region Private Methods

        private void Initialize()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Protocol = Protocols.FromEnvironment();
            factory.HostName = this.hostName;

            this.connection = factory.CreateConnection();
            this.Channel = this.connection.CreateModel();
        }

        #endregion Private Methods
    }
}
