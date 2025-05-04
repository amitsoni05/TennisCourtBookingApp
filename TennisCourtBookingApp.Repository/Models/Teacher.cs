using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class Teacher
    {
        public int Tid { get; set; }
        public string? Tname { get; set; }
        public int? Doj { get; set; }
        public int? Subject { get; set; }
    }
}
