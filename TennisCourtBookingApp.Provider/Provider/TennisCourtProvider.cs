using AutoMapper;
using TennisCourtBookingApp.Common.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtBookingApp.Common.BusinessEntities;
using TennisCourtBookingApp.Provider.IProvider;
using TennisCourtBookingApp.Repository.Models;
using TennisCourtBookingApp.Repository.Repository;
using TennisCourtBookingApp.Common.Utility;
using TennisCourtBookingApp.Repository.ADO;
using System.Data;
using OfficeOpenXml;

namespace TennisCourtBookingApp.Provider.Provider
{
    public class TennisCourtProvider : ITennisCourtProvider
    {

        protected UnitOfWork unitofwork = new UnitOfWork();
        private readonly IMapper _mapper;
        private readonly ICommonProvider _commonProvider;
        private DBConnectivity dBConnectivity = new DBConnectivity();
        public TennisCourtProvider(IMapper mapper,ICommonProvider commonProvider)
        {
            _mapper = mapper;
            _commonProvider = commonProvider;
        }
        public void SaveTennisCourt(TennisCourtModel courtDetails, int userId)
        {

            List<StoredProcModel> parms = new List<StoredProcModel>();

            parms.Add(new StoredProcModel() { Key = "TennisCourtId", Value = DBNull.Value });
            parms.Add(new StoredProcModel() { Key = "TennisCourtName", Value = courtDetails.TennisCourtName });
            parms.Add(new StoredProcModel() { Key = "TennisCourtAddress", Value = courtDetails.TennisCourtAddress });
            parms.Add(new StoredProcModel() { Key = "TennisCourtCapacity", Value = courtDetails.TennisCourtCapacity });
            parms.Add(new StoredProcModel() { Key = "CreatedOn", Value = DateTime.Now });
            parms.Add(new StoredProcModel() { Key = "CreatedBy", Value = userId });
            DataTable dt = dBConnectivity.GetDataFromSP(parms, "spSaveOrEditAndGetTennisCourt");
            var response = _commonProvider.ConvertDataTableToList<TennisCourtModel>(dt);
        }

        public DatatablePageResponseModel<TennisCourtModel> GetList(DatatablePageRequestModel datatablePageRequest, string searchText,int length)
        {
            DatatablePageResponseModel<TennisCourtModel> tennisCourtModelsList = new DatatablePageResponseModel<TennisCourtModel>()
            {
                draw = datatablePageRequest.Draw,
                data = new List<TennisCourtModel>()
            };

            try
            {
                List<StoredProcModel> parms = new List<StoredProcModel>();
                parms.Add(new StoredProcModel() { Key = "PAGE_INDEX", Value = datatablePageRequest.StartIndex });
                parms.Add(new StoredProcModel() { Key = "SORT_DIR", Value = datatablePageRequest.SortDirection });
                parms.Add(new StoredProcModel() { Key = "SORT_COLUMN", Value = datatablePageRequest.SortColumnName });
                parms.Add(new StoredProcModel() { Key = "PAGE_SIZE", Value = datatablePageRequest.PageSize > 0 ? datatablePageRequest.PageSize : int.MaxValue });
                parms.Add(new StoredProcModel() { Key = "SEARCH_TEXT", Value = searchText});

                int totalRecords = 0;
                DataTable dataTable = dBConnectivity.GetDataFromSP(parms, "GetUserBooking_SP_ShowData", ref totalRecords);

                tennisCourtModelsList.recordsTotal = totalRecords;
                tennisCourtModelsList.recordsFiltered = totalRecords;
                tennisCourtModelsList.data = _commonProvider.ConvertDataTableToList<TennisCourtModel>(dataTable);
                //tennisCourtModelsList.data = tennisCourtModelsList.data.Where(b=>b.Booki);
            }
            catch (Exception)
            {
                throw;
            }

            return tennisCourtModelsList;
        }

        public TennisCourtModel GetCourtById(int? courtID)
        {


            List<StoredProcModel> parms = new List<StoredProcModel>();
            parms.Add(new StoredProcModel() { Key = "TennisCourtId", Value = courtID });
            DataTable dt = dBConnectivity.GetDataFromSP(parms, "spGetTennisCourtById");
            var response = _commonProvider.ConvertDataTableToList<TennisCourtModel>(dt);
            return response.FirstOrDefault();
        }

        public void UpdateCourt(TennisCourtModel model, int userId)
        {
            if (model != null)
            {



                List<StoredProcModel> parms = new List<StoredProcModel>();
                parms.Add(new StoredProcModel() { Key = "TennisCourtId", Value = model.TennisCourtId });
                parms.Add(new StoredProcModel() { Key = "TennisCourtName", Value = model.TennisCourtName });
                parms.Add(new StoredProcModel() { Key = "TennisCourtAddress", Value = model.TennisCourtAddress });
                parms.Add(new StoredProcModel() { Key = "TennisCourtCapacity", Value = model.TennisCourtCapacity });
                parms.Add(new StoredProcModel() { Key = "UpdatedOn", Value = DateTime.Now });
                parms.Add(new StoredProcModel() { Key = "UpdatedBy", Value = userId });
                DataTable dt = dBConnectivity.GetDataFromSP(parms, "spSaveOrEditAndGetTennisCourt");
                var response = _commonProvider.ConvertDataTableToList<TennisCourtModel>(dt);

            }
        }

        public void DeleteCourt(TennisCourtModel model)
        {
            List<StoredProcModel> parms = new List<StoredProcModel>();
            parms.Add(new StoredProcModel() { Key = "TennisCourtId", Value = model.TennisCourtId });
            DataTable dt = dBConnectivity.GetDataFromSP(parms, "spDeleteTennisCourt");
            var response = _commonProvider.ConvertDataTableToList<TennisCourtModel>(dt);

        }
        DatatablePageResponseModel<UserBookingDetailsModel> ITennisCourtProvider.GetBookingList(DatatablePageRequestModel datatablePageRequest, int status,string searchText)
        {
            DatatablePageResponseModel<UserBookingDetailsModel> userBookingModelsList = new DatatablePageResponseModel<UserBookingDetailsModel>()
            {
                draw = datatablePageRequest.Draw,
                data = new List<UserBookingDetailsModel>()
            };

            try
            {
                //var StatusCheck = (BookingStatus)status;
                List<StoredProcModel> parms = new List<StoredProcModel>();
                parms.Add(new StoredProcModel() { Key = "PAGE_INDEX", Value = datatablePageRequest.StartIndex });
                parms.Add(new StoredProcModel() { Key = "SORT_DIR", Value = datatablePageRequest.SortDirection });
                parms.Add(new StoredProcModel() { Key = "SORT_COLUMN", Value = datatablePageRequest.SortColumnName });
                parms.Add(new StoredProcModel() { Key = "PAGE_SIZE", Value = datatablePageRequest.PageSize > 0 ? datatablePageRequest.PageSize : int.MaxValue });
                parms.Add(new StoredProcModel() { Key = "SEARCH_TEXT", Value = searchText });
                parms.Add(new StoredProcModel() { Key = "Status", Value = status });

                int totalRecords = 0;
                DataTable dataTable = dBConnectivity.GetDataFromSP(parms, "GetUserBookings_SP", ref totalRecords);

                userBookingModelsList.recordsTotal = totalRecords;
                userBookingModelsList.recordsFiltered = totalRecords;
                userBookingModelsList.data = _commonProvider.ConvertDataTableToList<UserBookingDetailsModel>(dataTable);
                //userBookingModelsList.data = userBookingModelsList.data.Where(b=>b.BookingDate>=DateTime.Now).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return userBookingModelsList;
        }
        //DatatablePageResponseModel<UserBookingDetailsModel> ITennisCourtProvider.GetBookingList(DatatablePageRequestModel datatablePageRequest, int status)
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
        //                            Status = u.Status ?? "",
        //                            BookingTime = u.BookingTime,
        //                            TennisCourtId = u.TennisCourtId,
        //                            UserId = u.UserId,
        //                            UserName = user.UserName,

        //                            TennisCourtName = court.TennisCourtName,
        //                            TennisCourtAddress = court.TennisCourtAddress,

        //                            // Add other properties as needed
        //                        });

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
        //        //if (status==3)
        //        //    userList = userList.Where(x => x.Status == "Reject");
        //        //if (status == 2)
        //        //    userList = userList.Where(x => x.Status == "Pending");
        //        //if (status == 4)
        //        //    userList = userList.Where(x => x.Status == "Confirm");
        //        model.recordsTotal = userList.Count();
        //        if (!string.IsNullOrEmpty(datatablePageRequest.SearchText))
        //        {
        //            string searchTextWithoutSpaces = datatablePageRequest.SearchText.Replace(" ", "").ToLower();
        //            userList = userList.Where(x =>

        //                x.TennisCourtName.Replace(" ", "").ToLower().Contains(searchTextWithoutSpaces)
        //                || x.TennisCourtAddress.Replace(" ", "").ToLower().Contains(searchTextWithoutSpaces)
        //                 || x.UserName.Replace(" ", "").ToLower().Contains(searchTextWithoutSpaces)
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

        public List<UserBookingDetailsModel> ShowPreviousBookings()
        {
            List<StoredProcModel> parms = new List<StoredProcModel>();

            DataTable dt = dBConnectivity.GetDataFromSP(parms, "GetUserPreviousBookings_SP");
            var response = _commonProvider.ConvertDataTableToList<UserBookingDetailsModel>(dt);
            List<UserBookingDetailsModel> data = new List<UserBookingDetailsModel>();
            foreach (var row in response)
            {
                UserBookingDetailsModel model = new UserBookingDetailsModel();
                model = row;
                model.StatusString = row.Status == (int)Enumaration.BookingStatus.All ? "All" : row.Status == (int)Enumaration.BookingStatus.Pending ? "Pending" : row.Status == (int)Enumaration.BookingStatus.Confirm ? "Confirm" : "Reject";
                data.Add(model);
            }
           
            return data;

        }



        public ResultModel DownlaodBookingListExcel( string filePath)
        {
            ResultModel model = new ResultModel();
            try
            {
                var bookingData = (from d in unitofwork.TennisCourtBooking.GetAll()
                                    join p in unitofwork.TennisCourt.GetAll()
                                    on d.TennisCourtId equals p.TennisCourtId
                                    join cmp in unitofwork.TennisCourtBookingUser.GetAll()
                                    on d.UserId equals cmp.UserId
                                    select new { cmp.UserName, d.BookingDate, p.TennisCourtAddress, p.TennisCourtName, d.BookingTime, d.Status }
                                                 ).OrderBy(x => x.BookingDate).ToList();

                string fileName = "BookingList_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".xlsx";
                string fullPath = Path.Combine(filePath, fileName);

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(new FileInfo(fullPath)))
                {
                    int index = 1;
                    int indUserName = index; index++;
                    int indTennisCourtName = index; index++;
                    int indTennisCourtAddress = index; index++;
                    int indBookingDate = index; index++;
                    int indBookingtime = index; index++;
                    int indStatus = index; index++;                   

                    ExcelWorksheet excel = package.Workbook.Worksheets.Add("TennisCourt Booking");
                    excel.Cells[1, indUserName].Value = "User Name";
                    excel.Cells[1, indTennisCourtName].Value = "Tennis Court Name";
                    excel.Cells[1, indTennisCourtAddress].Value = "Tennis Court Address";
                    excel.Cells[1, indBookingDate].Value = "Booking Date";
                    excel.Cells[1, indBookingtime].Value = "Booking time";
                    excel.Cells[1, indStatus].Value = "Status";
                   


                    //excel.Cells[1, 1, 1, indReturnReceivedDate].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //excel.Cells[1, 1, 1, indReturnReceivedDate].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDEBF7"));
                    //excel.Cells[1, 1, 1, indReturnReceivedDate].Style.Font.Bold = true;

                    int k = 2;
                    foreach (var item in bookingData.GroupBy(x => x.BookingDate).Select(x => x.FirstOrDefault()))
                    {



                        excel.Cells[k, indUserName].Value = item.UserName;
                        excel.Cells[k, indTennisCourtName].Value = item.TennisCourtName;
                        excel.Cells[k, indTennisCourtAddress].Value = item.TennisCourtAddress;
                        excel.Cells[k, indBookingDate].Value = item.BookingDate;
                        excel.Cells[k, indBookingtime].Value = item.BookingTime;
                        excel.Cells[k, indStatus].Value = item.Status == (int)Enumaration.BookingStatus.Pending ? "Pending" : item.Status == (int)Enumaration.BookingStatus.Confirm ? "Confirm" : "Reject"; ;
                      

                        k++;
                    }
                   
                    package.Save();
                }

                model.Result = true;
                model.Message = fileName;

            }
            catch (Exception ex)
            {
                model.Result = false;
                model.Message = AppCommon.ErrorMessage;
               
            }
            return model;
        }

    }
}
