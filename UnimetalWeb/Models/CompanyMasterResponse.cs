using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    public class CompanyMasterResponse
    {

        public bool? IsError { get; set; }
        public int? Status { get; set; }
        public string Message { get; set; }

        public List<companyMaster> result { get; set; }

    }


    public class companyMaster
        {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }
        public string Owner { get; set; }
        public string Phoneno1 { get; set; }
        public string Phoneno2 { get; set; }
        public string Phoneno3 { get; set; }
        public string Faxno { get; set; }
        public string EmailId { get; set; }
        public string Website { get; set; }
        public string SurchargeNo { get; set; }
        public string VatTinNo { get; set; }
        public string CstNo { get; set; }
        public string ServiceTaxNo { get; set; }
        public string PanNo { get; set; }
        public string CommodityNo { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsModify { get; set; }
        public string RegdNo { get; set; }
        public string EccNo { get; set; }
        public DateTime? VatWef { get; set; }
        public DateTime? CstWef { get; set; }
        public string IsoNumber { get; set; }
        public string ExpLicenNo { get; set; }
        public string ExpPermissoinNo { get; set; }
        public string ExciseRange { get; set; }
        public string ExciseDivision { get; set; }
        public string CommisonRate { get; set; }
        public string ExcSupreDetails { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankAccNo { get; set; }
        public string AccType { get; set; }
        public string BSwiftCode { get; set; }
        public string IfscCode { get; set; }
        public string CinNo { get; set; }
        public string CommCustom { get; set; }
        public string AutSpecSign { get; set; }
        public string GstNo { get; set; }
       
    }

  

}
