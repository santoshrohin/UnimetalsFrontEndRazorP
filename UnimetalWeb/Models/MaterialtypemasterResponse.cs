using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    public class MaterialtypemasterResponse : CommonResponse
    {
        public List<Materialtypemaster> result { get; set; }
    }
    public class MaterialtypemasterGetAll : CommonResponse
    {
        public Materialtypemaster result { get; set; }

    }
    public class Materialtypemaster
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyCode { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdatedDateandtime { get; set; }
        public bool? IsModify { get; set; }
        public bool? IsDelete { get; set; }
        public string MaterialTypeTitle { get; set; }
        public int? MaterialTypeCategoryID { get; set; }
        public int? MaterialTypeProductLineID { get; set; }
        public string CategoryName { get; set; }
        public string ProductLineTitle { get; set; }
        public string Abbreviation { get; set; }

    }
}
