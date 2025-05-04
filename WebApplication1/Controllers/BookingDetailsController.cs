using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;
using TennisCourtBookingApp.Provider.IProvider;
using TennisCourtBookingApp.Web.Controllers;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class BookingDetailsController : Controller
    {
      

        public BookingDetailsController()
        {
           
        }
        [HttpPost("PreviousBookings")]
        public IActionResult PreviousBookings([FromBody] int UserId)
        {
            var val = UserId == 1 ? "this is for admin use" : "this is for user use";
            return Ok(val);
        }
    }
}
