using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class NonRetentiveConsumedStockRepository : ComplexNonRetentiveEntityRepository<ConsumedStock, ConsumedStockDTO>, INonRetentiveConsumedStockRepository
    {
        public NonRetentiveConsumedStockRepository(IEntityRepository<ConsumedStockDTO> database, IBeeMapper mapper) : base(database, mapper)
        {
        }
    }
}
