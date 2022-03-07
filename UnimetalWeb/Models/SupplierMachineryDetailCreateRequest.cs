using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    public class SupplierMachineryDetailCreateRequest
    {




        public int id { get; set; }
        public int? fkid { get; set; }
        public string MachineName { get; set; }
        public string Make { get; set; }
        public string Capacity { get; set; }
        public string Qty { get; set; }
    }
}
