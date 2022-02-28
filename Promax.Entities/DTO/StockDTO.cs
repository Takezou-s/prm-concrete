using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class StockDTO
    {
        public int StockId { get; set; } = -1;
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public int StockCatNum { get; set; }
        public double Temp { get; set; }
        public double Balance { get; set; }
    }
}
