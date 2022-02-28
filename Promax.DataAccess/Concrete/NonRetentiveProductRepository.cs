using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class NonRetentiveProductRepository : ComplexNonRetentiveEntityRepository<Product, ProductDTO>, INonRetentiveProductRepository
    {
        public NonRetentiveProductRepository(IEntityRepository<ProductDTO> database, IBeeMapper mapper) : base(database, mapper)
        {
        }
    }
}
