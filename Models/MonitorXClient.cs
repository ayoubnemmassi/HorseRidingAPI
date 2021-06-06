using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorseRidingAPI.Models
{
    public class MonitorXClient
    {
        public int SeanceId { get; set; }
        public int SeanceGrpId { get; set; }
        public string ClientId { get; set; }
        public string MonitorId { get; set; }
        public DateTime StartDate { get; set; }
        public byte DurationMinut { get; set; }
        public byte IsDone { get; set; }
        public int? PaymentId { get; set; }
        public string Comments { get; set; }

        public virtual Client Client { get; set; }
    }
}
