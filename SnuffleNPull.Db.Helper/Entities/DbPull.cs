using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleNPull.Db.Helper.Entities
{
    public class DbPull
    {
        public long PullId { get; set; }
        public string CallerName { get; set; }
        public string PulledName { get; set; }
        public string TeamId { get; set; }
        public string ChannelId { get; set; }
        public string PullMessage { get; set; }
        public DateTime PullDate { get; set; }
    }
}
