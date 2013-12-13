using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ontrack.AWS.Kinesis.Model
{
    public class ListStreamsResponse : Response
    {
        /// <summary>
        /// If set to true, there are more streams available to list.
        /// </summary>
        public bool IsMoreDataAvailable { get; set; }

        /// <summary>
        /// The names of the streams that are associated with the AWS account making the ListStreams request.
        /// </summary>
        public List<string> StreamNames { get; set; }
    }
}
