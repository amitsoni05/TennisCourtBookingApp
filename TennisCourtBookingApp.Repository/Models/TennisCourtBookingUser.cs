using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class TennisCourtBookingUser
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public long? PhoneNo { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual TennisCourtBookingRole? Role { get; set; }
    }
}
