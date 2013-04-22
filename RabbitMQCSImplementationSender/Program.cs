using RabbitMQCSImplementation.BO.Clients;
using RabbitMQCSImplementation.BO.Enums;
using RabbitMQCSImplementation.BO.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQCSImplementationSender
{
    class Program
    {
        static void Main(string[] args)
        {
            IRabbitMQClient rabbitMQClient = new RabbitMQClient("localhost");
            string queueName = "dev";

            RabbitMQCSImplementation.BO.Managers.ICommunicationManager communicationManager = new CommunicationManager(rabbitMQClient, CommunicationType.Sender, queueName);
            for (int i = 0; i < 20; i++)
            {
                string message = string.Format("H: {0} !!!", i.ToString());
                communicationManager.Send(message);
                Console.WriteLine(message);
                Thread.Sleep(2000);
            }
            communicationManager.Close();

            Console.ReadKey();
        }
    }
}
