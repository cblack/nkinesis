using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis
{
    public class MissingParameterException : Exception       
    {
        public MissingParameterException(string message) : base(message) { }
    }
}
