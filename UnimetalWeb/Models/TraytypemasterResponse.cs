using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    public class TraytypemasterResponse : CommonResponse
    {
        public List<Traytypemaster> result { get; set; }
    }
    public class TraytypemasterGetAll:CommonResponse
    {
        public Traytypemaster result { get; set; }
    }
    public class Traytypemaster
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyCode { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdatedDateandtime { get; set; }
        public bool? IsModify { get; set; }
        public bool? IsDelete { get; set; }
        public string TrayTypeTitle { get; set; }
        public string TrayTypeAbbreviation { get; set; }
    }
}
