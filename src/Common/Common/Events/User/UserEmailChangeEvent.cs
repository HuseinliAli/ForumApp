using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Events.User
{
    public class UserEmailChangeEvent
    {
        public string OldEmailAddress { get; set; }
        public string NewEmailAddress { get; set; }
    }
}
