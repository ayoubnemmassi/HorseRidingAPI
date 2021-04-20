using System;
using System.Collections.Generic;

#nullable disable

namespace HorseRidingAPI.Models
{
    public partial class Seance
    {
        public int SeanceId { get; set; }
        public int SeanceGrpId { get; set; }
        public int ClientId { get; set; }
        public short MonitorId { get; set; }
        public DateTime StartDate { get; set; }
        public byte DurationMinut { get; set; }
        public byte IsDone { get; set; }
        public int? PaymentId { get; set; }
        public string Comments { get; set; }

        public virtual Client Client { get; set; }
        public virtual User Monitor { get; set; }
    }
}
