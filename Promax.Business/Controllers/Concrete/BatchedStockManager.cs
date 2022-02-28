using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class BatchedStockManager : NonRetentiveManager<BatchedStock, BatchedStockDTO>, IBatchedStockManager
    {
        public BatchedStockManager(IComplexNonRetentiveEntityRepository<BatchedStock, BatchedStockDTO> repo) : base(repo)
        {

        }
        public BatchedStockManager(IBeeMapper mapper) : base(new NonRetentiveBatchedStockRepository(new EntityRepository<BatchedStockDTO, ExpertContext>(), mapper))
        {

        }
    }
}
