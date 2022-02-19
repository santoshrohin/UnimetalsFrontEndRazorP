using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    public class CommonResponse
    {
        public bool? IsError { get; set; }
        public int? Status { get; set; }
        public string Message { get; set; }

    }
}
