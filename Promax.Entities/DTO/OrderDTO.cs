using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class OrderDTO
    {
        public int OrderId { get; set; } = -1;
        public DateTime OrderDate { get; set; }
        public DateTime OrderTime { get; set; }
        public int OrderStatus { get; set; }
        public int ServiceCatNum { get; set; }
        public int ClientId { get; set; }
        public int SiteId { get; set; }
        public int RecipeId { get; set; }
        public double AddWater { get; set; }
        public int PumpServiceId { get; set; }
        public double Quantity { get; set; }
        public double Produced { get; set; }
        public double Remaining { get; set; }
        public string OrderNumber { get; set; }

    }
}
