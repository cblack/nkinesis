using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis
{
    public class MissingAuthenticationTokenException : Exception
    {
        public MissingAuthenticationTokenException(string message) : base(message) { }
    }
}
