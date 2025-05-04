using Microsoft.AspNetCore.Mvc;

namespace TennisCourtBookingApp.Web.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
