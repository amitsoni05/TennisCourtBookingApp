using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public string? GradeName { get; set; }
        public string? Section { get; set; }
    }
}
