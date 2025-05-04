using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourtBookingApp.Common.BusinessEntities
{
    public class TennisCourtBookingRoleModel
    {
        public TennisCourtBookingRoleModel()
        {
            TennisCourtBookingUsersModel = new HashSet<TennisCourtBookingUserModel>();
        }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public virtual ICollection<TennisCourtBookingUserModel> TennisCourtBookingUsersModel { get; set; }
    }
}
