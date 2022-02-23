using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    
    public class POTypeMasterResponse : CommonResponse
    {
        public List<POTypeMaster> result { get; set; }
    }
    public class POTypeMasterResponseGetAll : CommonResponse
    {
        public POTypeMaster result { get; set; }
    }
    public class POTypeMaster
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyCode { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdatedDateandtime { get; set; }
        public bool? IsModify { get; set; }
        public bool? IsDelete { get; set; }
        public string POType { get; set; }
        public string POTypeDescription { get; set; }
        public string Abbrevation { get; set; }

    }

}
