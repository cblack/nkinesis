using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class GetNextRecordsRequest : Request
    {
        public override bool IsValidRequest()
        {
            if ((ShardIterator == null || ShardIterator.Length < 1 || ShardIterator.Length > 512) ||
                (Limit != null && (Limit < 1 || Limit > 10000)))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The maximum number of records to return, which can be set to a value of up to 10,000.
        /// Required: No
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// The position in the shard from which you want to start sequentially reading data records.
        /// Length constraints: Minimum length of 1. Maximum length of 512.
        /// Required: Yes
        /// </summary>
        public string ShardIterator { get; set; }
    }
}
