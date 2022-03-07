using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    
    public class HSNMasterResponse : CommonResponse
    {
        public List<HSNMaster> result { get; set; }
    }
    public class HSNMasterGetAll : CommonResponse
    {
        public HSNMaster result { get; set; }

    }
    public class HSNMaster
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyCode { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdatedDateandtime { get; set; }
        public bool? IsModify { get; set; }
        public bool? IsDelete { get; set; }
        public string HsnNumber { get; set; }
        public string HsnName { get; set; }
        public int? HsnType { get; set; }
        public float? HsnIgst { get; set; }
        public float? HsnCgst { get; set; }
        public float? HsnSgst { get; set; }
    }
}
