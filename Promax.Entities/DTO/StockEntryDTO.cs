using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class StockEntryDTO
    {
        public int EntryId { get; set; } = -1;
        public int StockId { get; set; } = -1;
        public int SiloId { get; set; } = -1;
        public int UserId { get; set; } = -1;
        public DateTime EntryDate { get; set; }
        public DateTime EntryTime { get; set; }
        public string DocumentNo { get; set; }
        public string Description { get; set; }
        public double Entry { get; set; }
        public double Minus { get; set; }
    }
}
