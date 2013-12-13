using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis
{
    public class ProvisionedThroughputExceededException : Exception
    {
        public ProvisionedThroughputExceededException(string message) : base(message) { }
    }
}
