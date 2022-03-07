using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    public class SupplierQualityDetailCreateRequest
    {




        public int id { get; set; }
        public int? fkid { get; set; }
        public string InstrumentName { get; set; }
        public string Make { get; set; }
        public bool? CalibrationFacility { get; set; }
        public string Qty { get; set; }
    }
}
