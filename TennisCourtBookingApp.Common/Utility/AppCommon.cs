using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourtBookingApp.Common.Utility
{
    public class AppCommon
    {


        public static string ErrorMessage = "Something Went Wrong. Please Contact Administrator!";
        public static string ApplicationLongTitle = "Tennis Court Booking App";
        public static string ApplicationTitle = "Tennis Court Booking App";
        public static string Protection = "Tennis Court Booking App";
        public static string ConnectionString = "";
        public static string SessionName = "";
        public static string Trim = "";
        public static string AppUrl = "https://localhost:7235/Home/ChangePassword/";
      
        public static DateTime CurrentDate
        {
            get
            {
                return DateTime.Now;

            }
        }
    }
}
