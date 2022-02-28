using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class BatchedStockDTO
    {
        public int BatchedId { get; set; }
        public int ProductId { get; set; }
        public int BatchNo { get; set; }
        public int StockId { get; set; }
        public int SiloId { get; set; }
        public int UserId { get; set; }
        public DateTime BatchedDate { get; set; }
        public DateTime BatchedTime { get; set; }
        public double AddVal { get; set; }
        public double Design { get; set; }
        public double Batched { get; set; }
    }
}
