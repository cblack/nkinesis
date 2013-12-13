using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class DescribeStreamRequest : Request
    {
        public override bool IsValidRequest()
        {
            if ((StreamName == null || StreamName.Length < 1 || StreamName.Length > 128) ||
                (ExclusiveStartShardId != null && (ExclusiveStartShardId.Length < 1 || ExclusiveStartShardId.Length > 128)))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The shard ID of the shard to start with for the stream description.
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: No
        /// </summary>
        public string ExclusiveStartShardId { get; set; }

        /// <summary>
        /// The maximum number of shards to return.
        /// Required: No
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// The name of the stream to describe.
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: Yes
        /// </summary>
        public string StreamName { get; set; }
    }
}
