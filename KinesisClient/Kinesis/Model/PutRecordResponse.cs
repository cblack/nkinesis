using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ontrack.AWS.Kinesis.Model
{
    public class PutRecordResponse : Response
    {
        /// <summary>
        /// The sequence number identifier that was assigned to the put data record. 
        /// </summary>
        public string SequenceNumber { get; set; }

        /// <summary>
        /// The shard ID of the shard where the data record was placed.
        /// </summary>
        public string ShardId { get; set; }
    }
}
