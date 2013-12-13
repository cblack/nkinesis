using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public abstract class Request
    {
        /// <summary>
        /// Used to validate required parameters
        /// </summary>
        public abstract bool IsValidRequest();
    }
}
