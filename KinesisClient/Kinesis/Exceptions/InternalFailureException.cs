using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis
{
    public class InternalFailureException : Exception       
    {
        public InternalFailureException(string message) : base(message) { }
    }
}
