using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class Member
    {
        public int Empno { get; set; }
        public string? Ename { get; set; }
        public int? Sal { get; set; }
        public int? Comm { get; set; }
        public int? Mgr { get; set; }
    }
}
