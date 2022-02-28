using Extensions;
using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class ProductDTO
    {
        private string _isBill="false";
        private string _desk="false";
        private string _dep="false";

        public int ProductId { get; set; } = -1;
        public DateTime ProductDate { get; set; }
        public DateTime ProductTime { get; set; }
        public int RtmNumber { get; set; }
        public int BillNumber { get; set; }
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public int SiteId { get; set; }
        public int RecipeId { get; set; }
        public int ServiceCatNum { get; set; }
        public int MixerServiceId { get; set; }
        public int PumpServiceId { get; set; }
        public int SelfServiceId { get; set; }
        public int MixerDriverId { get; set; }
        public int PumpDriverId { get; set; }
        public int SelfDriverId { get; set; }
        public int UserId { get; set; }
        public double Target { get; set; }
        public double Addon { get; set; }
        public double RealTarget { get; set; }
        public double Produced { get; set; }
        public double Shipped { get; set; }
        public double Returned { get; set; }
        public double Transfer { get; set; }
        public double Recycled { get; set; }
        public double Delivered { get; set; }
        public double Capacity { get; set; }
        public double Ubm { get; set; }
        public int AimBatch { get; set; }
        public string AimFactor { get; set; }
        public int RtmBatch { get; set; }
        public string RtmFactor { get; set; }
        public int GateNum { get; set; }
        public double AddWater { get; set; }
        public string IsBill { get => _isBill; set => _isBill = value.Boolify(); }
        public string Desk { get => _desk; set => _desk = value.Boolify(); }
        public string Dep { get => _dep; set => _dep = value.Boolify(); }
        public DateTime DepTime { get; set; }
        public int Pos { get; set; }
        public string DespatchNumber { get; set; }
        public string DespatchGuid { get; set; }
        public string EbisNumber { get; set; }
        public string DespatchTag { get; set; }
        public int DespatchStatus { get; set; }
        public double OrderQuantity { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
