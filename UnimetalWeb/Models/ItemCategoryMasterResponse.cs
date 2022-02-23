using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    
    public class ItemCategoryMasterResponse : CommonResponse
    {
        public List<ItemCategoryMaster> result { get; set; }
    }
    public class ItemCategoryMasterGetAll : CommonResponse
    {
        public ItemCategoryMaster result { get; set; }

    }
    public class ItemCategoryMaster
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CategoryName { get; set; }
        public bool IsShortClose { get; set; }
        public string Abbreviation { get; set; }
        public string IsModify { get; set; }
    }
}
