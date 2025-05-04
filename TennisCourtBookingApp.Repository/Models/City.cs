using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class City
    {
        public int Cid { get; set; }
        public string? CityName { get; set; }
        public int? Sid { get; set; }

        public virtual State? SidNavigation { get; set; }
    }
}
