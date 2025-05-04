using System;
using System.Collections.Generic;

namespace TennisCourtBookingApp.Repository.Models
{
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        public int Sid { get; set; }
        public string? StateName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
