using RabbitMQCSImplementation.BO.Clients;
using RabbitMQCSImplementation.BO.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQCSImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            IRabbitMQClient rabbitMQClient = new RabbitMQClient("localhost");
            string queueName = "dev";

            if (args != null && args.Any() && args[0].Equals("Sender", StringComparison.InvariantCultureIgnoreCase))
            {
                ICommunicationManager communicationManager = new CommunicationManager(rabbitMQClient, BO.Enums.CommunicationType.Sender, queueName);
                for (int i = 0; i < 20; i++)
                {
                    string message = string.Format("H: {0} !!!", i.ToString());
                    communicationManager.Send(message);
                    Console.WriteLine(message);
                    if (args[1] != null)
                    {
                        int sleepTime = int.Parse(args[1]);
                        Thread.Sleep(sleepTime);
                    }
                }
                communicationManager.Close();
            }
            else if (args != null && args.Any() && args[0].Equals("Receiver", StringComparison.InvariantCultureIgnoreCase))
            {
                ICommunicationManager communicationManager = new CommunicationManager(rabbitMQClient, BO.Enums.CommunicationType.Receiver, queueName);
                while (true)
                {
                    if (args[1] != null)
                    {
                        int sleepTime = int.Parse(args[1]);
                        Thread.Sleep(sleepTime);
                    }
                    Console.WriteLine(communicationManager.Read());
                }
                //communicationManager.Close();
            }

            
            Console.ReadKey();

        }
    }
}
