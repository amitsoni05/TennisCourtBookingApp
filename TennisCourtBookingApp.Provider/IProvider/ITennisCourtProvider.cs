using TennisCourtBookingApp.Common.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtBookingApp.Common.BusinessEntities;
using TennisCourtBookingApp.Common.Utility;

namespace TennisCourtBookingApp.Provider.IProvider
{
    public interface ITennisCourtProvider
    {
        public void SaveTennisCourt(TennisCourtModel courtDetails,int userId);
        //public List<TennisCourtModel> GetTennisCourtList();
        public DatatablePageResponseModel<TennisCourtModel> GetList(DatatablePageRequestModel datatablePageRequest,string searchText , int length);
        public DatatablePageResponseModel<UserBookingDetailsModel> GetBookingList(DatatablePageRequestModel datatablePageRequest,int status , string searchText);
        public List<UserBookingDetailsModel> ShowPreviousBookings();
        public TennisCourtModel GetCourtById(int? courtID);
        public void UpdateCourt(TennisCourtModel model, int userId);
        public void DeleteCourt(TennisCourtModel model);

        ResultModel DownlaodBookingListExcel( string filePath);



    }
}
