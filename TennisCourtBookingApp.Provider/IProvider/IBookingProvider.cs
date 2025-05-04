using TennisCourtBookingApp.Common.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtBookingApp.Common.BusinessEntities;
using System.Data;

namespace TennisCourtBookingApp.Provider.IProvider
{
    public interface IBookingProvider
    {
        public string SaveBooking(TennisCourtBookingModel model,int? userId , int? courtId );
        //public List<TennisCourtBookingModel> GetBookCourtDetails(int? userId);
        public TennisCourtBookingModel GetBookingById(int bookingId);
        public DataTable GetBookingDetailsById(int bookingId);
        public void DeleteBookingByCourtId(TennisCourtModel model);
        //public List<TennisCourtBookingModel> GetAllBookings(string Status);
        public void DeleteBooking(int bookingId);
        public (int Count, int? Capacity) CheckSlotAvailability(TennisCourtBookingModel model, int courtId);
        //public DatatablePageResponseModel<TennisCourtBookingModel> GetUserBookings(DatatablePageRequestModel datatablePageRequest,int? userId);

        public ResponseModel EditBookStatus(int Status , int bookingId);
    }
}
