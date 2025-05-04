using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class TennisCourtBookingRole
    {
        public TennisCourtBookingRole()
        {
            TennisCourtBookingUsers = new HashSet<TennisCourtBookingUser>();
        }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public virtual ICollection<TennisCourtBookingUser> TennisCourtBookingUsers { get; set; }
    }
}
