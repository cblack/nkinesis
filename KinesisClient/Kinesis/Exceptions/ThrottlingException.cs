using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis
{
    public class ThrottlingException : Exception       
    {
        public ThrottlingException(string message) : base(message) { }
    }
}
