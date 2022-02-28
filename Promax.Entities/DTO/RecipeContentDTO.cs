using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class RecipeContentDTO
    {
        public int ContentId { get; set; } = -1;
        public int RecipeId { get; set; }
        public int StockId { get; set; }
        public double Quantity { get; set; }
    }
}
