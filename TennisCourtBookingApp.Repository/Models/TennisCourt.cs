using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class TennisCourt
    {
        public int TennisCourtId { get; set; }
        public string? TennisCourtName { get; set; }
        public string? TennisCourtAddress { get; set; }
        public int? TennisCourtCapacity { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
