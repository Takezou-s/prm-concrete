using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class ServiceCategoryDTO
    {
        public int CatId { get; set; } = -1;
        public int CatClass { get; set; }
        public int CatNum { get; set; }
        public string CatName { get; set; }
    }
}
