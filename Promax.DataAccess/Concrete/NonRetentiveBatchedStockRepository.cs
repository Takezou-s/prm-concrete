using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class NonRetentiveBatchedStockRepository : ComplexNonRetentiveEntityRepository<BatchedStock, BatchedStockDTO>, INonRetentiveBatchedStockRepository
    {
        public NonRetentiveBatchedStockRepository(IEntityRepository<BatchedStockDTO> database, IBeeMapper mapper) : base(database, mapper)
        {
        }
    }
}
