using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtBookingApp.Common.BusinessEntities;
using TennisCourtBookingApp.Common.CommonEntities;
using TennisCourtBookingApp.Common.Utility;
using TennisCourtBookingApp.Provider.IProvider;
using TennisCourtBookingApp.Repository.Models;
using TennisCourtBookingApp.Repository.Repository;
using System.Data;
using TennisCourtBookingApp.Provider.Provider;
using TennisCourtBookingApp.Repository.ADO;

namespace TennisCourtBookingApp.Provider.Provider
{

  
    public class UserProvider : IUserProvider
    {
         protected UnitOfWork unitofwork = new UnitOfWork();
        private DBConnectivity dBConnectivity = new DBConnectivity();
        private readonly IMapper _mapper;
        private readonly ICommonProvider _commonProvider;

        public UserProvider(IMapper mapper,ICommonProvider commonProvider)
        {
            _mapper = mapper;
            _commonProvider = commonProvider;
        }
        public TennisCourtBookingUserModel GetUserById(int? userId)
        {
            List<StoredProcModel> parms = new List<StoredProcModel>();
            parms.Add(new StoredProcModel() { Key = "UserId", Value = userId });
            DataTable dt = dBConnectivity.GetDataFromSP(parms, "spGetUserById");
            TennisCourtBookingUserModel response = _commonProvider.ConvertDataTableToList<TennisCourtBookingUserModel>(dt).FirstOrDefault(); 
            response.InvoiceAttachmentBase  = "ExtraFiles/MaintenenceDocuments/" + response.InvoiceAttachment;
            return response;
        }


        public int UpdateUser(TennisCourtBookingUserModel model)
        {
           
            List<StoredProcModel> parms = new List<StoredProcModel>();
            parms.Add(new StoredProcModel() { Key = "UserId", Value =model.UserId });
            parms.Add(new StoredProcModel() { Key = "UserName", Value = model.UserName });
            parms.Add(new StoredProcModel() { Key = "PhoneNo", Value = model.PhoneNo });
            parms.Add(new StoredProcModel() { Key = "Email", Value = model.Email });
            parms.Add(new StoredProcModel() { Key = "Password", Value = model.Password });
            parms.Add(new StoredProcModel() { Key = "Address", Value = model.Address });
            parms.Add(new StoredProcModel() { Key = "Image", Value = null });
            parms.Add(new StoredProcModel() { Key = "RoleId", Value = 2 });
            parms.Add(new StoredProcModel() { Key = "UpdatedOn", Value = DateTime.Now});
            DataTable dt = dBConnectivity.GetDataFromSP(parms, "spSaveOrEdit_User");
            TennisCourtBookingUserModel response = _commonProvider.ConvertDataTableToList<TennisCourtBookingUserModel>(dt).FirstOrDefault();
            return model.UserId;
        }

        public void UpdateUserImage(TennisCourtBookingUserModel model, int? userId)
        {
            List<StoredProcModel> parms = new List<StoredProcModel>();
            parms.Add(new StoredProcModel() { Key = "UserId", Value = userId });
            parms.Add(new StoredProcModel() { Key = "Image", Value = model.Image });
            DataTable dt = dBConnectivity.GetDataFromSP(parms, "spUpdate_UserImage");
            TennisCourtBookingUserModel response = _commonProvider.ConvertDataTableToList<TennisCourtBookingUserModel>(dt).FirstOrDefault();
        }


        DatatablePageResponseModel<UserBookingDetailsModel> IUserProvider.GetUserBooking(DatatablePageRequestModel datatablePageRequest, int? userId, int status, string searchText)
        {
            DatatablePageResponseModel<UserBookingDetailsModel> userBookingModelsList = new DatatablePageResponseModel<UserBookingDetailsModel>()
            {
                draw = datatablePageRequest.Draw,
                data = new List<UserBookingDetailsModel>()
            };

            try
            {
               
                List<StoredProcModel> parms = new List<StoredProcModel>();
                parms.Add(new StoredProcModel() { Key = "PAGE_INDEX", Value = datatablePageRequest.StartIndex });
                parms.Add(new StoredProcModel() { Key = "SORT_DIR", Value = datatablePageRequest.SortDirection });
                parms.Add(new StoredProcModel() { Key = "SORT_COLUMN", Value = datatablePageRequest.SortColumnName });
                parms.Add(new StoredProcModel() { Key = "PAGE_SIZE", Value = datatablePageRequest.PageSize > 0 ? datatablePageRequest.PageSize : int.MaxValue });
                parms.Add(new StoredProcModel() { Key = "SEARCH_TEXT", Value = searchText});
              
                parms.Add(new StoredProcModel() { Key = "Status", Value = status });
                parms.Add(new StoredProcModel() { Key = "UserIdd", Value = userId });

                int totalRecords = 0;
                DataTable dataTable = dBConnectivity.GetDataFromSP(parms, "GetBookings_SP", ref totalRecords);

                userBookingModelsList.recordsTotal = totalRecords;
                userBookingModelsList.recordsFiltered = totalRecords;
                userBookingModelsList.data = _commonProvider.ConvertDataTableToList<UserBookingDetailsModel>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }

            return userBookingModelsList;
        }
        //DatatablePageResponseModel<UserBookingDetailsModel> IUserProvider.GetUserBooking(DatatablePageRequestModel datatablePageRequest, int? userId, int status)
        //{
        //    DatatablePageResponseModel<UserBookingDetailsModel> model = new DatatablePageResponseModel<UserBookingDetailsModel>()
        //    {
        //        draw = datatablePageRequest.Draw,
        //        data = new List<UserBookingDetailsModel>()
        //    };
        //    try
        //    {
        //        var userList = (from u in unitofwork.TennisCourtBooking.GetAll()
        //                        join user in unitofwork.TennisCourtBookingUser.GetAll() on u.UserId equals user.UserId
        //                        join court in unitofwork.TennisCourt.GetAll() on u.TennisCourtId equals court.TennisCourtId
        //                        select new UserBookingDetailsModel()
        //                        {
        //                            BookingId = u.BookingId,
        //                            BookingDate = u.BookingDate,
        //                            Status = u.Status??"",
        //                            BookingTime = u.BookingTime,
        //                            TennisCourtId = u.TennisCourtId,
        //                            UserId = u.UserId,
        //                            UserName = user.UserName,

        //                            TennisCourtName = court.TennisCourtName,
        //                            TennisCourtAddress = court.TennisCourtAddress,

        //                            // Add other properties as needed
        //                        });
        //        userList = userList.Where(x => x.UserId == userId);
        //        switch ((BookingStatus)status)
        //        {
        //            case BookingStatus.Pending:
        //                userList = userList.Where(x => x.Status == BookingStatus.Pending.ToString());
        //                break;
        //            case BookingStatus.Confirm:
        //                userList = userList.Where(x => x.Status == BookingStatus.Confirm.ToString());
        //                break;
        //            case BookingStatus.Rejected:
        //                userList = userList.Where(x => x.Status == BookingStatus.Rejected.ToString());
        //                break;
        //            case BookingStatus.All:
        //                // Handle the "All" case if needed
        //                break;
        //            default:
        //                // Handle other cases
        //                break;
        //        }
        //        //if (status == "Reject")
        //        //    userList = userList.Where(x => x.Status == "Reject");
        //        //if (status == "Pending")
        //        //    userList = userList.Where(x => x.Status == "Pending");
        //        //if (status == "Confirm")
        //        //    userList = userList.Where(x => x.Status == "Confirm");
        //        model.recordsTotal = userList.Count();
        //        if (!string.IsNullOrEmpty(datatablePageRequest.SearchText))
        //        {
        //            string searchTextWithoutSpaces = datatablePageRequest.SearchText.Replace(" ", "").ToLower();
        //            userList = userList.Where(x =>
        //                x.TennisCourtName.Replace(" ", "").ToLower().Contains(searchTextWithoutSpaces)
        //                || x.TennisCourtAddress.Replace(" ", "").ToLower().Contains(searchTextWithoutSpaces)
        //                 || x.Status.Replace(" ", "").ToLower().Contains(searchTextWithoutSpaces)
        //                 //|| (x.BookingDate.HasValue && x.BookingDate.Value.ToString("MM/dd/yyyy").Contains(datatablePageRequest.SearchText))
        //                 // || (x.BookingTime.HasValue && x.BookingTime.Value.ToString().Contains(datatablePageRequest.SearchText))
        //            // Add other conditions as needed
        //            );
        //        }

        //        model.recordsFiltered = userList.Count();

        //        model.data = userList.Skip(datatablePageRequest.StartIndex).Take(datatablePageRequest.PageSize).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return model;
        //}
        //public List<T> ConvertDataTableToList<T>(DataTable dt)
        //{
        //    var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
        //    var properties = typeof(T).GetProperties();
        //   var model= dt.AsEnumerable().Select(row =>
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
        //    return model;
        //}
        public ResponseModel ChangePassword(TennisCourtBookingUserModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            var userDetails=unitofwork.TennisCourtBookingUser.GetAll(e=>e.Email==model.Email).FirstOrDefault();
            if (userDetails!=null)
            {
              string pass=PasswordHash.CreateHash(model.Password);
                userDetails.Password=pass;



                unitofwork.Save();
                responseModel.IsSuccess=true;
                return responseModel;
               
            }
            responseModel.IsSuccess = false;
            return responseModel;
        }
    }

}
