using RabbitMQCSImplementation.BO.Clients;
using RabbitMQCSImplementation.BO.Enums;
using RabbitMQCSImplementation.BO.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQCSImplementationReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            IRabbitMQClient rabbitMQClient = new RabbitMQClient("localhost");
            string queueName = "dev";

            ICommunicationManager communicationManager = new CommunicationManager(rabbitMQClient, CommunicationType.Receiver, queueName);
            while (true)
            {
                Console.WriteLine(communicationManager.Read());
                Thread.Sleep(2000);
            }
            //communicationManager.Close();

            //Console.ReadKey();
        }
    }
}
