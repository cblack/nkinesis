using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class DeleteStreamRequest : Request
    {
        public override bool IsValidRequest()
        {
            if ((StreamName == null || StreamName.Length < 1 || StreamName.Length > 128))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The name of the stream to delete.
        /// Length constraints: Minimum length of 1. Maximum length of 128.
        /// Required: Yes
        /// </summary>
        public string StreamName { get; set; }
    }
}
