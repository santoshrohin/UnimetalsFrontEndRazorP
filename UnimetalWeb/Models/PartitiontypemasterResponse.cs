using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    
    public class PartitiontypemasterResponse : CommonResponse
    {
        public List<Partitiontypemaster> result { get; set; }
    }
    public class PartitiontypemasterGetAll : CommonResponse
    {
        public Partitiontypemaster result { get; set; }

    }
    public class Partitiontypemaster
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyCode { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdatedDateandtime { get; set; }
        public bool? IsModify { get; set; }
        public bool? IsDelete { get; set; }
        public string PartitionTypeTitle { get; set; }
        public string PartitionTypeAbbreviation { get; set; }
    }
}
