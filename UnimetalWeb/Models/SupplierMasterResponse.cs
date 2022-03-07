using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnimetalWeb.Models
{
    
    public class SupplierMasterResponse : CommonResponse
    {
        public List<SupplierMaster> result { get; set; }
    }
    public class SupplierMasterGetAll : CommonResponse
    {
        public SupplierMaster result { get; set; }

    }
    public class SupplierMaster
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyCode { get; set; }
        public string UserName { get; set; }
        public DateTime? LastUpdatedDateandtime { get; set; }
        public bool? IsModify { get; set; }
        public bool? IsDelete { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string YearofCommencement { get; set; }
        public string PhoneNo { get; set; }
        public string GSTNo { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public string CIN { get; set; }
        public string UdyamRegistrationCertificateNo { get; set; }
        public string Category { get; set; }
        public string Constitution { get; set; }
        public string WeeklyOff { get; set; }
        public string Workinghours { get; set; }
        public string DetailsofProductsorServices { get; set; }
        public string LANDArea { get; set; }
        public string BUILDINGArea { get; set; }
        public string Bankers { get; set; }
        public string Certification { get; set; }
        public string TransportationDacility { get; set; }
        public string TurnoverofLastTwoYears { get; set; }
        public string OverallProductionCapacity { get; set; }
        public string CurrentCapacityUtilize { get; set; }
        public string SpareCapacity { get; set; }
        public string NoofSamples { get; set; }
        public bool? SafetyOrEnvironmentalCompliance { get; set; }
        public bool? ProductsOrProcessEnvironmentFriendly { get; set; }
        public bool? HazardousWasteManagementProcess { get; set; }
        public bool? ReachAndRoHSCompliance { get; set; }
        public bool? MsmeApplicable { get; set; }
        public string MsmeCertificate { get; set; }
        public string TallyName { get; set; }
        public List<SupplierContactDetailCreateRequest> requestSupplierContactDetail { get; set; }
        public List<SupplierMachineryDetailCreateRequest> requestSupplierMachineryDetail { get; set; }

        public List<SupplierQualityDetailCreateRequest> requestSupplierQualityDetail { get; set; }
    }
}
