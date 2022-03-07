using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models.Transactions
{
    
    public class POMasterResponse : CommonResponse
    {
        public List<POMaster> result { get; set; }
    }
    public class POMasterGetAll : CommonResponse
    {
        public POMaster result { get; set; }

    }
    public class POMaster
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyCode { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdatedDateandtime { get; set; }
        public bool? IsModify { get; set; }
        public bool? IsDelete { get; set; }
        public int? SupplierId { get; set; }
        public int? POType { get; set; }
        public int? Type { get; set; }
        public int? PONO { get; set; }
        public DateTime? PODate { get; set; }
        public string PONumber { get; set; }
        public string POAbbrevation { get; set; }
        public string SupplierRef { get; set; }
        public DateTime? SupplierRefDate { get; set; }
        public DateTime? POValidityDate { get; set; }
        public int? AmendmentNo { get; set; }
        public string Notes { get; set; }
        public string Freight { get; set; }
        public string DelivaryInst { get; set; }
        public string PaymentTerms { get; set; }
        public string DelivaryMode { get; set; }
        public bool? Post { get; set; }
        public DateTime? PostDate { get; set; }
        public bool? Authorized { get; set; }
        public DateTime? AuthorizedDate { get; set; }
        public bool? Cancel { get; set; }
        public DateTime? CancelDate { get; set; }
        public float? BasicAmt { get; set; }
        public float? TransportAmt { get; set; }
        public float? PackgingAmt { get; set; }
        public float? DiscPer { get; set; }
        public float? DiscAmt { get; set; }
        public float? TaxbleAmt { get; set; }
        public float? CGSTPer { get; set; }
        public float? CGSTAMT { get; set; }
        public float? SGSTPer { get; set; }
        public float? SGSTAMT { get; set; }
        public float? IGSTPer { get; set; }
        public float? IGSTAMT { get; set; }
        public float? RoundOffAmt { get; set; }
        public float? TotalAmt { get; set; }
        public List<PODetailCreateRequest> requestPODetail { get; set; }
    }
}
