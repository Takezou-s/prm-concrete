using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class ConsumedStockManager : NonRetentiveManager<ConsumedStock, ConsumedStockDTO>, IConsumedStockManager
    {
        public ConsumedStockManager(IComplexNonRetentiveEntityRepository<ConsumedStock, ConsumedStockDTO> repo) : base(repo)
        {

        }
        public ConsumedStockManager(IBeeMapper mapper) : base(new NonRetentiveConsumedStockRepository(new EntityRepository<ConsumedStockDTO, ExpertContext>(), mapper))
        {

        }
    }
}
