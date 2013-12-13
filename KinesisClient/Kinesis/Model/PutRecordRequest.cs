using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ontrack.AWS.Kinesis.Model
{
    public class PutRecordRequest : Request
    {
        public override bool IsValidRequest()
        {
            if ((Data == null || Data.Length > 51200) ||
                (PartitionKey == null || PartitionKey.Length < 1 || PartitionKey.Length > 128) ||
                (StreamName == null || StreamName.Length < 1 || StreamName.Length > 128))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The data blob to put into the record, which must be Base64 encoded. 
        /// Length constraints: Minimum length of 0. Maximum length of 51200.
        /// Required: Yes
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// The sequence number from a previous call to PutRecord that is used to keep the put data 
        /// record in order with the data record put in the previous call.
        /// Required: No
        /// </summary>
        public string ExclusiveMinimumSequenceNumber { get; set; }

        /// <summary>
        /// The hash value used to explicitly determine the shard the data record is assigned to by 
        /// overriding the partition key hash.
        /// Required: No
        /// </summary>
        public string ExplicitHashKey { get; set; }

        /// <summary>
        /// Determines which shard in the stream the data record is assigned to.
        /// Length constraints: Minimum length of 1. Maximum length of 256.
        /// Required: Yes
        /// </summary>
        public string PartitionKey { get; set; }

        /// <summary>
        /// The name of the stream to put the data record into.
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: Yes
        /// </summary>
        public string StreamName { get; set; }
    }
}
