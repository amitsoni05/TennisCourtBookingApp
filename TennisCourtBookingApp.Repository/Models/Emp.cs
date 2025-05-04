using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class Emp
    {
        public int Empno { get; set; }
        public string? Ename { get; set; }
        public string? Job { get; set; }
        public decimal? Sal { get; set; }
        public int? Comm { get; set; }
        public int? Deptno { get; set; }
    }
}
