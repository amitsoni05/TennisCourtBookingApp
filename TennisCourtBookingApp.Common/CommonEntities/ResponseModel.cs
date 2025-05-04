using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourtBookingApp.Common.CommonEntities
{
    public class ResponseModel
    {

        public bool IsSuccess { get; set; }
        public string HeaderMessage { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public int Confirmation { get; set; }
    }
}
