using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourtBookingApp.Common.BusinessEntities
{
    public class UserBookingDetailsModel
    {
        public int? UserId { get; set; }
        public int? TennisCourtId { get; set; }
        public int BookingId { get; set; }
        public string UserName { get; set; }
        public string TennisCourtName { get; set; }
        public string TennisCourtAddress { get; set; }
        public int? TennisCourtCapacity { get; set; }
        public string StatusString { get; set; }
        public DateTime? BookingDate { get; set; }
        public TimeSpan? BookingTime { get; set; }
        public int? Status { get; set; }
        public string? InvoiceAttachmentBase { get; set; }

    }
}
