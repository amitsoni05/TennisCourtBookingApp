using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtBookingApp.Common.BusinessEntities;
using TennisCourtBookingApp.Common.CommonEntities;
using TennisCourtBookingApp.Repository.Models;

namespace TennisCourtBookingApp.Provider.IProvider
{
    public interface IUserProvider
    {
        public TennisCourtBookingUserModel GetUserById(int? userId);
        public int UpdateUser(TennisCourtBookingUserModel model);
        //public List<TennisCourtBookingUserModel> GetUsersList();
        public void UpdateUserImage(TennisCourtBookingUserModel model, int? userId);
        public DatatablePageResponseModel<UserBookingDetailsModel> GetUserBooking(DatatablePageRequestModel datatablePageRequest, int? userId , int status,string searchText);
        public ResponseModel ChangePassword(TennisCourtBookingUserModel model);
    }
}
