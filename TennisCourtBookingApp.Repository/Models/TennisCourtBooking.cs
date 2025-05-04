using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class TennisCourtBooking
    {
        public int BookingId { get; set; }
        public int? TennisCourtId { get; set; }
        public int? UserId { get; set; }
        public DateTime? BookingDate { get; set; }
        public TimeSpan? BookingTime { get; set; }
        public int? Status { get; set; }
    }
}
