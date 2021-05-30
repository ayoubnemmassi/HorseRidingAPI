using System;
using System.Collections.Generic;

#nullable disable

namespace HorseRidingAPI.Models
{
    public partial class Note
    {
        public int Id { get; set; }
        public string Notes { get; set; }
        public DateTime? Date { get; set; }
        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
