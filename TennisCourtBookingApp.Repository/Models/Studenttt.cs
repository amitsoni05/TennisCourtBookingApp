using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class Studenttt
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte[]? Photo { get; set; }
        public decimal Height { get; set; }
        public float Weight { get; set; }
        public int? GradeGradeId { get; set; }
    }
}
