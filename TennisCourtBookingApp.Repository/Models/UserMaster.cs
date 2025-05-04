using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class UserMaster
    {
        public int UserMasterId { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public string? Role { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? StudentId { get; set; }
        public byte[]? StuImage { get; set; }

        public virtual Student? Student { get; set; }
    }
}
