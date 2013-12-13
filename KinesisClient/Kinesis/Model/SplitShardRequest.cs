using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class SplitShardRequest : Request
    {
        public override bool IsValidRequest()
        {
            if ((NewStartingHashKey == null) ||
                (ShardToSplit == null || ShardToSplit.Length < 1 || ShardToSplit.Length > 128) ||
                (StreamName == null || StreamName.Length < 1 || StreamName.Length > 128))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// A hash key value for the starting hash key of one of the child shards created by the split.
        /// Required: Yes
        /// </summary>
        public string NewStartingHashKey { get; set; }

        /// <summary>
        /// The shard ID of the shard to split.
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: Yes
        /// </summary>
        public string ShardToSplit { get; set; }

        /// <summary>
        /// The name of the stream for the shard split.
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: Yes
        /// </summary>
        public string StreamName { get; set; }
    }
}
