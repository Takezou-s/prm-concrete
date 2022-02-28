using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class StockMovementDTO : ICloneable
    {
        public DateTime InvDate { get; set; }
        public int StockId { get; set; }
        public int SiloId { get; set; }
        public int StockCatNum { get; set; }
        public string StockName { get; set; }
        public double Quantity { get; set; }

        public object Clone()
        {
            return new StockMovementDTO()
            {
                InvDate = InvDate,
                StockId = StockId,
                SiloId = SiloId,
                StockCatNum = StockCatNum,
                StockName = StockName,
                Quantity = Quantity
            };
        }
    }
}
