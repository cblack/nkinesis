using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class MergeShardsRequest : Request
    {
        public override bool IsValidRequest()
        {
            if ((AdjacentShardToMerge == null || AdjacentShardToMerge.Length < 1 || AdjacentShardToMerge.Length > 128) ||
                (ShardToMerge == null || ShardToMerge.Length < 1 || ShardToMerge.Length > 128) ||
                (StreamName == null || StreamName.Length < 1 || StreamName.Length > 128))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The shard ID of the adjacent shard for the merge.
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: Yes
        /// </summary>
        public string AdjacentShardToMerge { get; set; }

        /// <summary>
        /// The shard ID of the shard to combine with the adjacent shard for the merge.
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: Yes
        /// </summary>
        public string ShardToMerge { get; set; }

        /// <summary>
        /// The name of the stream for the merge.
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: Yes
        /// </summary>
        public string StreamName { get; set; }
    }
}
