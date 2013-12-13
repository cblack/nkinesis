using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class SequenceNumberRange
    {
        public string StartingSequenceNumber { get; set; }
        public string EndingSequenceNumber { get; set; }
    }
}
