using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class Employee
    {
        public int EmpId { get; set; }
        public string? EmpName { get; set; }
        public DateTime? EmpDob { get; set; }
        public string? EmpCity { get; set; }
        public string? EmpState { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
