using System;
using System.Collections.Generic;

#nullable disable

namespace HorseRidingAPI.Models
{
    public partial class User
    {
        public User()
        {
            Seances = new HashSet<Seance>();
            Tasks = new HashSet<Task>();
        }

        public short UserId { get; set; }
        public string SessionToken { get; set; }
        public string UserEmail { get; set; }
        public string UserPasswd { get; set; }
        public byte? AdminLevel { get; set; }
        public DateTime LastLoginTime { get; set; }
        public byte IsActive { get; set; }
        public string UserFname { get; set; }
        public string UserLname { get; set; }
        public string Description { get; set; }
        public string UserType { get; set; }
        public string Userphoto { get; set; }
        public DateTime ContractDate { get; set; }
        public string UserPhone { get; set; }
        public string DisplayColor { get; set; }

        public virtual ICollection<Seance> Seances { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
