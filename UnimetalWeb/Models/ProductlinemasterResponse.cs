using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    
    public class ProductlinemasterResponse : CommonResponse
    {
        public List<Productlinemaster> result { get; set; }
    }
    public class ProductlinemasterGetAll : CommonResponse
    {
        public Productlinemaster result { get; set; }
    }
    public class Productlinemaster
    {

        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyCode { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdatedDateandtime { get; set; }
        public bool? IsModify { get; set; }
        public bool? IsDelete { get; set; }
        public string ProductLineTitle { get; set; }
        public string ProductLineAbbreviation { get; set; }
    }

}
