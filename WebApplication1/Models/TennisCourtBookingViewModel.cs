using TennisCourtBookingApp.Common.BusinessEntities;
using TennisCourtBookingApp.Repository.Models;

namespace WebApplication1.Models
{
    public class TennisCourtBookingViewModel
    {
        public TennisCourtModel TennisCourtModel { get; set; }
        public List<TennisCourtModel> TennisCourtModels { get; set; }
        public TennisCourtBookingModel TennisCourtBookingModel { get; set; }
        public List<TennisCourtBookingModel> TennisCourtBookingModels { get; set; }
        public TennisCourtBookingRoleModel TennisCourtBookingRoleModel { get; set; }
        public TennisCourtBookingUserModel TennisCourtBookingUserModel { get; set; }
        public List<TennisCourtBookingUserModel> TennisCourtBookingUserModels { get; set; }
       public List<TennisCourtBookingViewModel> TempList { get; set; }
        public List<UserBookingDetailsModel> UserBookingDetailsModels { get; set; }

        public bool IsEdit { get; set; }
        public bool IsId { get; set; }
        public bool IsUser { get; set; }
        public bool IsDelete { get; set; }
        public string Message { get; set; }
        public string IsStatus { get; set; }
        public bool IsAdd { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsView { get; set; }
        public bool IsSuccess { get; set; }
        public int TennisCourtId { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan BookingTime { get; set; }
        public string tempDataKey { get; set; }
        public IFormFile Image { get; set; }
        public String UserName { get; set; }
        public string UserEmail { get; set; }
        public string CaptchaCode { get; set; }
        public string CaptchaImage { get; set; }

    }
}
