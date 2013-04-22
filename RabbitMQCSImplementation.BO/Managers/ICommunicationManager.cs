using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCSImplementation.BO.Managers
{
    public interface ICommunicationManager
    {
        string Read();
        void Send(string message);
        void Close();
    }
}
