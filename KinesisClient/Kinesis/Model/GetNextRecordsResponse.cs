using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ontrack.AWS.Kinesis.Model
{
    public class GetNextRecordsResponse : Response
    {
        /// <summary>
        /// The next position in the shard from which to start sequentially reading data records. 
        /// </summary>
        public string NextShardIterator { get; set; }

        /// <summary>
        /// Type: array of Record objects
        /// </summary>
        public List<Record> Records { get; set; }
    }
}
