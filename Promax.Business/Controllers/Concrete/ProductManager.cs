using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class ProductManager : NonRetentiveManager<Product, ProductDTO>, IProductManager
    {
        public ProductManager(IComplexNonRetentiveEntityRepository<Product, ProductDTO> repo) : base(repo)
        {

        }
        public ProductManager(IBeeMapper mapper) : base(new NonRetentiveProductRepository(new EntityRepository<ProductDTO, ExpertContext>(), mapper))
        {

        }
    }
}