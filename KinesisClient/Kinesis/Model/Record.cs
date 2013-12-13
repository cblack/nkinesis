using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class Record
    {
        public string Data { get; set; }

        public string PartitionKey { get; set; }

        public string SequenceNumber { get; set; }
    }
}
