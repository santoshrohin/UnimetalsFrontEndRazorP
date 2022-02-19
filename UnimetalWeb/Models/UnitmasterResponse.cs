using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    public class UnitmasterResponse : CommonResponse
    {
        public List<Unitmaster> result { get; set; }
    }
    public class UnitmasterGetAll : CommonResponse
    {
        public Unitmaster result { get; set; }
        
    }
    public class Unitmaster 
    {
        public int? Id { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyCode { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdatedDateandtime { get; set; }
        public bool? IsModify { get; set; }
        public bool? IsDelete { get; set; }
        public string UnitName { get; set; }
        public string UnitAbbreviation { get; set; }
    }
}
