using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class GetShardIteratorResponse : Response
    {
        /// <summary>
        /// The position in the shard from which to start reading data records sequentially.
        /// </summary>
        public string ShardIterator { get; set; }
    }
}
