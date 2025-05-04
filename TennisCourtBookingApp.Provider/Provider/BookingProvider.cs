using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using TennisCourtBookingApp.Common.BusinessEntities;
using TennisCourtBookingApp.Common.CommonEntities;
using TennisCourtBookingApp.Common.Utility;
using TennisCourtBookingApp.Provider.IProvider;
using TennisCourtBookingApp.Repository.ADO;
using TennisCourtBookingApp.Repository.Models;
using TennisCourtBookingApp.Repository.Repository;

namespace TennisCourtBookingApp.Provider.Provider
{
    public class BookingProvider : IBookingProvider
    {
        protected UnitOfWork unitofwork = new UnitOfWork();
        private DBConnectivity dBConnectivity = new DBConnectivity();

        private readonly IMapper _mapper;
        private readonly ICommonProvider _commonProvider;

        public BookingProvider(IMapper mapper, ICommonProvider commonProvider)
        {
            _mapper = mapper;
            _commonProvider = commonProvider;
        }
        public string SaveBooking(TennisCourtBookingModel model, int? userId, int? courtId)
        {
            if (model != null)
            {

               
                List<StoredProcModel> parms = new List<StoredProcModel>();
                
                parms.Add(new StoredProcModel() { Key = "BookingId", Value = model.BookingId });
                DataTable dt = dBConnectivity.GetDataFromSP(parms, "spGetBookingById");
                var CourtIdIsExist = _commonProvider.ConvertDataTableToList<UserBookingDetailsModel>(dt);
                if (CourtIdIsExist.Count == 0)
                {
                    List<StoredProcModel> para = new List<StoredProcModel>();
                    para.Add(new StoredProcModel() { Key = "UserId", Value = userId });
                    para.Add(new StoredProcModel() { Key = "TennisCourtId", Value = courtId });
                    para.Add(new StoredProcModel() { Key = "BookingDate", Value = model.BookingDate });
                    para.Add(new StoredProcModel() { Key = "BookingTime", Value = model.BookingTime });
                    para.Add(new StoredProcModel() { Key = "Status", Value =2 });

                    DataTable dtable = dBConnectivity.GetDataFromSP(para, "spCourtCRUD");
                    var respone = _commonProvider.ConvertDataTableToList<UserBookingDetailsModel>(dtable);                  
                    return null;
                }
                else
                {
                    int tennisCourtId = CourtIdIsExist.FirstOrDefault()?.TennisCourtId ?? 0;
                    var modell = CourtIdIsExist.FirstOrDefault();
                    List<StoredProcModel> paramater = new List<StoredProcModel>();
                    paramater.Add(new StoredProcModel() { Key = "TennisCourtId", Value = tennisCourtId });

                    DataTable dtbl = dBConnectivity.GetDataFromSP(paramater, "spCheckSlot");
                    var response = _commonProvider.ConvertDataTableToList<UserBookingDetailsModel>(dtbl);
                    var count = 0;
                    int tennisCourtCapacity = response.FirstOrDefault()?.TennisCourtCapacity ?? 0;

                    foreach (var booking in response)
                    {
                        if (booking.BookingDate == model.BookingDate && booking.BookingTime == model.BookingTime && (booking.Status == 4 || booking.Status == 2))
                        {
                            count++;
                        }
                    }
                    if (count < tennisCourtCapacity)
                    {
                        List<StoredProcModel> parm = new List<StoredProcModel>();
                        parm.Add(new StoredProcModel() { Key = "BookingId", Value = modell.BookingId });
                        parm.Add(new StoredProcModel() { Key = "BookingDate", Value = model.BookingDate });
                        parm.Add(new StoredProcModel() { Key = "BookingTime", Value = model.BookingTime });
                        DataTable bookingtbl = dBConnectivity.GetDataFromSP(parm, "spUpdateBooking");
                        var resp = _commonProvider.ConvertDataTableToList<UserBookingDetailsModel>(bookingtbl);                 
                        return "Success";
                    }
                    else
                    {
                        return "Slot Not Available";
                    }
                }

            }
            return null;
        }
        public TennisCourtBookingModel GetBookingById(int bookingId)
        {
            if (bookingId != null)
            {
                List<StoredProcModel> parms = new List<StoredProcModel>();
                //parms.Add(new StoredProcModel() { Key = "RoleId", Value = signupDetails.RoleId > 0 ? 2 : 1 });
                parms.Add(new StoredProcModel() { Key = "BookingId", Value = bookingId });
                DataTable dt = dBConnectivity.GetDataFromSP(parms, "spGetBookingById");
                var model = _commonProvider.ConvertDataTableToList<TennisCourtBookingModel>(dt);
                return model.FirstOrDefault();

            }
            return null;
        }
        public DataTable GetBookingDetailsById(int bookingId)
        {
            if (bookingId != null)
            {
                List<StoredProcModel> parms = new List<StoredProcModel>();
               
                parms.Add(new StoredProcModel() { Key = "BookingId", Value = bookingId });
                DataTable dt = dBConnectivity.GetDataFromSP(parms, "spGetBookingDetailsById");
               
                return dt;

            }
            return null;
        }
        public void DeleteBooking(int bookingId)
        {
            List<StoredProcModel> parms = new List<StoredProcModel>();
            
            parms.Add(new StoredProcModel() { Key = "BookingId", Value = bookingId });
            DataTable dt = dBConnectivity.GetDataFromSP(parms, "spDeleteTennisCourtBooking");
            var model = _commonProvider.ConvertDataTableToList<TennisCourtBookingModel>(dt);           
        }
        public void DeleteBookingByCourtId(TennisCourtModel model)
        {
            if (model != null)
            {
                List<StoredProcModel> parms = new List<StoredProcModel>();                
                parms.Add(new StoredProcModel() { Key = "TennisCourtId", Value = model.TennisCourtId });
                DataTable dt = dBConnectivity.GetDataFromSP(parms, "spDeleteBookingsByCourtId");
                var response = _commonProvider.ConvertDataTableToList<TennisCourtBookingModel>(dt);

            }
        }

        public (int Count, int? Capacity) CheckSlotAvailability(TennisCourtBookingModel model, int courtId)
        {
            
            List<StoredProcModel> parms = new List<StoredProcModel>();
            parms.Add(new StoredProcModel() { Key = "TennisCourtId", Value = courtId });

            DataTable dtbl = dBConnectivity.GetDataFromSP(parms, "spCheckSlot");
            var bookingDetails = _commonProvider.ConvertDataTableToList<UserBookingDetailsModel>(dtbl);
            var count = 0;
            foreach (var booking in bookingDetails)
            {
                if (booking.BookingDate == model.BookingDate && booking.BookingTime == model.BookingTime && (booking.Status ==4 || booking.Status == 2))
                {
                    count++;
                }
            }
            int tennisCourtCapacity = bookingDetails.FirstOrDefault()?.TennisCourtCapacity ?? 0;
            return (Count: count, Capacity: tennisCourtCapacity);
        }

        public ResponseModel EditBookStatus(int Status, int bookingId)
        {
            var bookingDetail = unitofwork.TennisCourtBooking.GetById(bookingId);
            ResponseModel response = new ResponseModel();
            if (bookingDetail != null)
            {
                bookingDetail.Status = Status;
                unitofwork.Save(); // Assuming you have a Save method in your unitofwork to persist changes
              
                response.Confirmation = Status;
                return response;
            }
            return response;
        }
        //public List<T> ConvertDataTableToList<T>(DataTable dt)
        //{
        //    var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
        //    var properties = typeof(T).GetProperties();
        //    return dt.AsEnumerable().Select(row =>
        //    {
        //        var objT = Activator.CreateInstance<T>();
        //        foreach (var pro in properties)
        //        {
        //            if (columnNames.Contains(pro.Name.ToLower()))
        //            {
        //                try
        //                {
        //                    if (row[pro.Name] != DBNull.Value)
        //                        pro.SetValue(objT, row[pro.Name]);
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw ex;
        //                }
        //            }
        //        }
        //        return objT;
        //    }).ToList();
        //}
    }


}
