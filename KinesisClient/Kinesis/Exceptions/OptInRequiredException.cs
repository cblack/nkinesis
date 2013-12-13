using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis
{
    public class OptInRequiredException : Exception       
    {
        public OptInRequiredException(string message) : base(message) { }
    }
}
