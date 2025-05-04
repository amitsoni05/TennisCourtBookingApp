using TennisCourtBookingApp.Common.CommonEntities;

namespace TennisCourtBookingApp.Web.Models
{
    public class DownloadReportModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public List<string> ListOffilepath { get; set; }
    }

}
