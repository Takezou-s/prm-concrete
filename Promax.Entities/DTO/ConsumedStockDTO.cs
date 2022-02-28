using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class ConsumedStockDTO
    {
        public int ConsumedId { get; set; }
        public int StockId { get; set; }
        public int SiloId { get; set; }
        public int UserId { get; set; }
        public DateTime ConsumedDate { get; set; }
        public DateTime ConsumedTime { get; set; }
        public double Consumed { get; set; }
    }
}
