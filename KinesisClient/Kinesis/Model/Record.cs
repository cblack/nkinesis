using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class Record
    {
        /// <summary>
        /// The data blob. The data in the blob is both opaque and immutable to the Amazon Kinesis service, 
        /// which does not inspect, interpret, or change the data in the blob in any way. The maximum size of 
        /// the data blob (the payload after Base64-decoding) is 50 kilobytes (KB)
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Identifies which shard in the stream the data record is assigned to.
        /// </summary>
        public string PartitionKey { get; set; }

        /// <summary>
        /// The unique identifier for the record in the Amazon Kinesis stream.
        /// </summary>
        public string SequenceNumber { get; set; }
    }
}
