using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourtBookingApp.Common.BusinessEntities
{
    public  class TennisCourtBookingModel
    {
        
        public int BookingId { get; set; }
        public int? TennisCourtId { get; set; }
        public int? UserId { get; set; }
        [Required]
        public DateTime? BookingDate { get; set; }
        [Required]
        public TimeSpan? BookingTime { get; set; }
        public virtual TennisCourtModel? TennisCourtModel { get; set; }
        public int? Status { get; set; }
    }
}
