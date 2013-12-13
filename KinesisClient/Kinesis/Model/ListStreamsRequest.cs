using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ontrack.AWS.Kinesis.Model
{
    public class ListStreamsRequest : Request
    {
        public override bool IsValidRequest()
        {
            if (ExclusiveStartStreamName != null && (ExclusiveStartStreamName.Length < 1 || ExclusiveStartStreamName.Length > 128))
                return false;
            
            return true;
        }

        /// <summary>
        /// The name of the stream to start the list with.
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: No
        /// </summary>
        public string ExclusiveStartStreamName { get; set; }

        /// <summary>
        /// The maximum number of streams to list.
        /// Required: No
        /// </summary>
        public string Limit { get; set; }
    }
}
