using System;

namespace Promax.Entities
{
    public class ProductWayBillViewDTO
    {
        public int ProductId { get; set; }
        public DateTime ProductDate { get; set; }
        public DateTime ProductTime { get; set; }
        public DateTime DepTime { get; set; }
        public int RtmNumber { get; set; }
        public int BillNumber { get; set; }
        public double Shipped { get; set; }
        public double Delivered { get; set; }
        public double TotalMaterial { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPhone { get; set; }
        public string ClientTaxLocation { get; set; }
        public string ClientTaxNumber { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public string SitePhone { get; set; }
        public string SiteContact { get; set; }
        public string RecipeName { get; set; }
        public string Consistency { get; set; }
        public string Endurance { get; set; }
        public string Dmax { get; set; }
        public string CementType { get; set; }
        public string MineralType { get; set; }
        public string AdditiveType { get; set; }
        public string Slump { get; set; }
        public string UnitVolume { get; set; }
        public string Environmental { get; set; }
        public string ChlorideContent { get; set; }
        public double CemRate { get; set; }
        public int MixerServiceId { get; set; }
        public string MixerLicencePlate { get; set; }
        public string MixerDriver { get; set; }
        public int PumpServiceId { get; set; }
        public string PumpLicencePlate { get; set; }
        public string PumpDriver { get; set; }
        public string ServiceCategoryName { get; set; }
        public string UserName { get; set; }
        public string AdditiveName { get; set; }
    }
}
