using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourtBookingApp.Common.CommonEntities
{
    public class DatatablePageRequestModel
    {
        public int StartIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string SearchText { get; set; } = "";
        public string SortColumnName { get; set; } = "";
        public string SortDirection { get; set; } = "";
        public object Draw { get; set; } = "";
        public string ExtraSearch { get; set; } = "";
    }
}
