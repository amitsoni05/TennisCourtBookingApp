using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourtBookingApp.Common.BusinessEntities
{
    public  class TennisCourtModel
    {
        public TennisCourtModel()
        {
            TennisCourtBookings = new HashSet<TennisCourtBookingModel>();
        }

        public int TennisCourtId { get; set; }
        [Required]
        public string? TennisCourtName { get; set; }
        [Required]
        public string? TennisCourtAddress { get; set; }
        [Required]
        public int? TennisCourtCapacity { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public virtual ICollection<TennisCourtBookingModel> TennisCourtBookings { get; set; }
    }
}
