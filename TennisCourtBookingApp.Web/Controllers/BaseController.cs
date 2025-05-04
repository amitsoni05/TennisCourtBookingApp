using Microsoft.AspNetCore.Mvc;
using TennisCourtBookingApp.Common.CommonEntities;
using TennisCourtBookingApp.Common.Utility;
using TennisCourtBookingApp.Provider.IProvider;

namespace TennisCourtBookingApp.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICommonProvider _commonProvider;
        //private readonly ISessionManager _sessionManager;

        public BaseController(ICommonProvider commonProvider)
        {
            _commonProvider = commonProvider;
            //_sessionManager = sessionManager;
        }

        //[NonAction]
        //protected SessionProviderModel GetSessionProviderParameters()
        //{
        //    SessionProviderModel sessionProviderModel = new SessionProviderModel
        //    {
        //        UserId = _sessionManager.UserId,
        //        Username = _sessionManager.Username,
        //        Ip = _sessionManager.GetIP(),
        //        FirstName = _sessionManager.FirstName,
        //        LastName = _sessionManager.LastName,
        //        RoleId = _sessionManager.RoleId,
        //        RoleName = _sessionManager.RoleName,
        //    };
        //    return sessionProviderModel;
        //}

        [NonAction]
        public DatatablePageRequestModel GetPagingRequestModel()
        {
            DatatablePageRequestModel model = new DatatablePageRequestModel
            {
            };
            model.StartIndex = Convert.ToInt32(HttpContext.Request.Form["start"]);
            model.PageSize = Convert.ToInt32(HttpContext.Request.Form["length"]);
            model.SearchText = HttpContext.Request.Form["search[value]"];
            model.SortColumnName = HttpContext.Request.Form["columns[" + HttpContext.Request.Form["order[0][column]"] + "][name]"];
            model.SortDirection = HttpContext.Request.Form["order[0][dir]"];
            model.Draw = HttpContext.Request.Form["draw"];

            return model;
        }

    }
}
