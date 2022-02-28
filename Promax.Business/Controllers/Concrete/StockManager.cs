using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class StockManager : Manager<Stock>, IStockManager
    {
        public StockManager(IEntityRepository<Stock> repo) : base(repo)
        {

        }
        public StockManager(IBeeMapper mapper) : base(new RetentiveStockRepository(new EntityRepository<StockDTO, ExpertContext>(), mapper))
        {

        }
    }
}
