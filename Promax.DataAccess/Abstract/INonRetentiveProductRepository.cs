using Promax.Entities;

namespace Promax.DataAccess
{
    public interface INonRetentiveProductRepository : IComplexNonRetentiveEntityRepository<Product, ProductDTO>
    {

    }
}