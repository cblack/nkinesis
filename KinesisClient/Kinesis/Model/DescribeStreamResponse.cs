using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class DescribeStreamResponse : Response
    {
        /// <summary>
        /// Contains the current status of the stream, the stream ARN, an array of shard objects 
        /// that comprise the stream, and states whether there are more shards available.
        /// </summary>
        public StreamDescription StreamDescription { get; set; }
    }
}
