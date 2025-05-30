﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourtBookingApp.Common.CommonEntities
{
    public class DatatablePageResponseModel<T> where T : class
    {
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T> data { get; set; }
        public object draw { get; set; }
    }
    public class DatatableSpDataResponseModel<T> where T : class
    {
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public T data { get; set; }
        public T footer { get; set; }
        public object draw { get; set; }
    }
}
