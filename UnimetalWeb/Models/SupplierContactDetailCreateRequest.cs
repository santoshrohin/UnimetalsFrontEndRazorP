using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    public class SupplierContactDetailCreateRequest
    {




        public int id { get; set; }
        public int? fkid { get; set; }
        public string ContactType { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
    }
}
