using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public static class ShardIteratorTypes
    {
        /// <summary>
        /// Start reading exactly from the position denoted by a specific sequence number.
        /// </summary>
        public static string AT_SEQUENCE_NUMBER = "AT_SEQUENCE_NUMBER";
        /// <summary>
        /// Start reading right after the position denoted by a specific sequence number.
        /// </summary>
        public static string AFTER_SEQUENCE_NUMBER = "AFTER_SEQUENCE_NUMBER";
        /// <summary>
        /// Start reading at the last untrimmed record in the shard in the system, which is the oldest data record in the shard.
        /// </summary>
        public static string TRIM_HORIZON = "TRIM_HORIZON";
        /// <summary>
        ///  Start reading just after the most recent record in the shard, so that you always read the most recent data in the shard.
        /// </summary>
        public static string LATEST = "LATEST";
    }

    public class GetShardIteratorRequest : Request
    {
        private HashSet<string> iteratorTypes = new HashSet<string>() 
        { 
            ShardIteratorTypes.AT_SEQUENCE_NUMBER,
            ShardIteratorTypes.AFTER_SEQUENCE_NUMBER,
            ShardIteratorTypes.TRIM_HORIZON,
            ShardIteratorTypes.LATEST
        };

        public override bool IsValidRequest()
        {
            if ((ShardId == null || ShardId.Length < 1 || ShardId.Length > 128) ||
                (ShardIteratorType == null || !iteratorTypes.Contains(ShardIteratorType)) ||
                (StreamName == null || StreamName.Length < 1 || StreamName.Length > 128))

            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The shard ID of the shard to get the iterator for.
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: Yes
        /// </summary>
        public string ShardId { get; set; }

        /// <summary>
        /// Determines how the shard iterator is used to start reading data records from the shard.
        /// Valid Values: AT_SEQUENCE_NUMBER | AFTER_SEQUENCE_NUMBER | TRIM_HORIZON | LATEST
        /// Required: Yes
        /// </summary>
        public string ShardIteratorType { get; set; }

        /// <summary>
        /// The sequence number of the data record in the shard from which to start reading from.
        /// Required: No
        /// </summary>
        public string StartingSequenceNumber { get; set; }

        /// <summary>
        /// The name of the stream.
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: Yes
        /// </summary>
        public string StreamName { get; set; }
    }
}
