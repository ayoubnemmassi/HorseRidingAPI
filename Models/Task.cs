using System;
using System.Collections.Generic;

#nullable disable

namespace HorseRidingAPI.Models
{
    public partial class Task
    {
        public int TaskId { get; set; }
        public DateTime StartDate { get; set; }
        public byte DurationMinut { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime IsDone { get; set; }
        public short UserFk { get; set; }

        public virtual User UserFkNavigation { get; set; }
    }
}
