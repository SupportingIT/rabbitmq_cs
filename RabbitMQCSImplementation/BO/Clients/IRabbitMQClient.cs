using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCSImplementation.BO.Clients
{
    public interface IRabbitMQClient
    {
        string Exchange { get; }
        string RoutingKey { get; }
        IModel Channel { get; }
        QueueingBasicConsumer Consumer { get; }

        void CreateQueue(string queueName);
        void CreateQueueConsumer(string queueName);
        void Close();
    }
}