using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQCSImplementation.BO.Clients;
using RabbitMQCSImplementation.BO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCSImplementation.BO.Managers
{
    public class CommunicationManager : ICommunicationManager
    {
        #region Private Properties

        private IRabbitMQClient rabbitMQClient;
        private CommunicationType communicationType;
        private string queueName;

        #endregion Private Properties

        #region Public Constructors

        public CommunicationManager(IRabbitMQClient rabbitMQClient, CommunicationType communicationType, string queueName)
        {
            this.rabbitMQClient = rabbitMQClient;
            this.communicationType = communicationType;
            this.queueName = queueName;

            Initialize();
        }

        #endregion Public Constructrs

        #region Public Methods

        public string Read()
        {
            BasicDeliverEventArgs e = (BasicDeliverEventArgs)this.rabbitMQClient.Consumer.Queue.Dequeue();
            IBasicProperties props = e.BasicProperties;
            byte[] body = e.Body;

            this.rabbitMQClient.Channel.BasicAck(e.DeliveryTag, false);

            return Encoding.UTF8.GetString(body);
        }

        public void Send(string message)
        {
            byte[] messageBody = Encoding.UTF8.GetBytes(message);
            IBasicProperties messageProperties = this.rabbitMQClient.Channel.CreateBasicProperties();
            messageProperties.SetPersistent(true);

            this.rabbitMQClient.Channel.BasicPublish(this.rabbitMQClient.Exchange, this.rabbitMQClient.RoutingKey, messageProperties, messageBody);
        }

        public void Close()
        {
            this.rabbitMQClient.Close();
        }

        #endregion Public Methods


        #region Private Methods

        private void Initialize()
        {
            if (this.communicationType == CommunicationType.Sender)
            {
                this.rabbitMQClient.CreateQueue(this.queueName);
            }
            else if (this.communicationType == CommunicationType.Receiver)
            {
                this.rabbitMQClient.CreateQueueConsumer(this.queueName);
            }
            else
            {
                throw new NotImplementedException(string.Format("communication type: '{0}' is unexpected.", this.communicationType.ToString()));
            }
        }

        #endregion Private Methods
    }
}
