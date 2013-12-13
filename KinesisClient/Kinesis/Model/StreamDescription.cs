using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class StreamDescription
    {
        public bool IsMoreDataAvailable { get; set; }
        
        public List<Shard> Shards { get; set; }
        
        public string StreamARN { get; set; }

        public string StreamName { get; set; }

        public string StreamStatus { get; set; }
    }
}
