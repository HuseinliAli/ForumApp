using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constants
{
    public class RabbitMQConstants
    {
        public const string RabbitMQHost = "localhost";
        public const string DefaultExchangeType = "direct";
        public const string UserEchangeName = "userExchange";
        public const string UserEmailChangedQueueName = "userEmailChangedQueue";
    }
}
