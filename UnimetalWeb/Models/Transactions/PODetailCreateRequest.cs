using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models.Transactions
{
    public class PODetailCreateRequest
    {

        public int id { get; set; }
        public int? fkid { get; set; }
        public int? SrNo { get; set; }
        public int? Icode { get; set; }
        public string Description { get; set; }
        public float? OrderQty { get; set; }
        public float? InwardQty { get; set; }
        public float? Rate { get; set; }
        public float? Amt { get; set; }
        public float? DiscPer { get; set; }
        public float? DiscAmt { get; set; }
        public int? ItemUnit { get; set; }
        public int? ItemGST { get; set; }
        public float? ConvRate { get; set; }
        public int? RateUnit { get; set; }
        public string ModNo { get; set; }
        public DateTime? ModDate { get; set; }
        public float? CGSTRate { get; set; }
        public float? SGSTRate { get; set; }
        public float? IGSTRate { get; set; }
        public float? CGSTAmt { get; set; }
        public float? SGSTAmt { get; set; }
        public float? IGSTAmt { get; set; }
        public bool? Status { get; set; }
        public int? Process { get; set; }
    }
}
