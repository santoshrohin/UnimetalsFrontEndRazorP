using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    
    public class ItemsubcategorymasterResponse : CommonResponse
    {
        public List<Itemsubcategorymaster> result { get; set; }
    }
    public class ItemsubcategorymasterGetAll : CommonResponse
    {
        public Itemsubcategorymaster result { get; set; }

    }
    public class Itemsubcategorymaster
    {
        public int id { get; set; }
        public int? company_id { get; set; }
        public int? company_code { get; set; }
        public string username { get; set; }
        public DateTime? last_updated_dateandtime { get; set; }
        public bool? is_modify { get; set; }
        public bool? is_delete { get; set; }
        public int? fkid { get; set; }
        public string sub_category_title { get; set; }
    }
}
