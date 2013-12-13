using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis
{
    public class MissingActionException : Exception       
    {
        public MissingActionException(string message) : base(message) { }
    }
}
