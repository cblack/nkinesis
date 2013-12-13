using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class Shard
    {
        public string AdjacentParentShardId { get; set; }

        public HashKeyRange HashKeyRange { get; set; }

        public string ParentShardId { get; set; }

        public SequenceNumberRange SequenceNumberRange { get; set; }

        public string ShardId { get; set; }
    }
}
