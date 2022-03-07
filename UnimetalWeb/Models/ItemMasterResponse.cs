using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    
    public class ItemMasterResponse : CommonResponse
    {
        public List<ItemMaster> result { get; set; }
    }
    public class ItemMasterGetAll : CommonResponse
    {
        public ItemMaster result { get; set; }

    }
    public class ItemMaster
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyCode { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdatedDateandtime { get; set; }
        public bool? IsModify { get; set; }
        public bool? IsDelete { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public float? Width { get; set; }
        public float? Length { get; set; }
        public float? Thickness { get; set; }
        public string OD { get; set; }
        public string StoreLocation { get; set; }
        public float? MinLevel { get; set; }
        public float? MaxLevel { get; set; }
        public float? ReOrderLevel { get; set; }
        public bool? ShelfLife { get; set; }
        public bool? IsActive { get; set; }
        public string Type { get; set; }
        public float? NoOfTrays { get; set; }
        public string TopType { get; set; }
        public bool? IsLock { get; set; }
        public bool? LedArrangement { get; set; }
        public float? Height { get; set; }
        public string Location { get; set; }
        public string AutoCadNo { get; set; }
        public string Attachment { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? ItemSubCategoryId { get; set; }
        public int? GSTId { get; set; }
        public int? UOM { get; set; }
        public int? UOMAlternateID { get; set; }
        public float? ConvertionRatio { get; set; }
        public int? TrayTypeId { get; set; }
        public int? MaterialTypeId { get; set; }
        public int? FormTypeId { get; set; }
        public int? ProductLineId { get; set; }
        public int? FixtureTypeId { get; set; }
        public int? PartitionTypeId { get; set; }

        public string CategoryName { get; set; }
        public string FormTypeFormTypeTitle { get; set; }
    }
}
