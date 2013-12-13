using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class CreateStreamRequest : Request
    {
        public override bool IsValidRequest()
        {
            if ((StreamName == null || StreamName.Length < 1 || StreamName.Length > 128) ||
                (ShardCount < 1 || ShardCount > 2))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The number of shards that the stream will use. 
        /// Required: Yes
        /// </summary>
        public int ShardCount { get; set; }

        /// <summary>
        /// A name to identify the stream. 
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: Yes
        /// </summary>
        public string StreamName { get; set; }
    }
}
