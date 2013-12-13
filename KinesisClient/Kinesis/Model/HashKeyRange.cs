using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class HashKeyRange
    {
        public string StartingHashKey { get; set; }
        public string EndingHashKey { get; set; }
    }
}
