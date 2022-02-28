using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class StockEntryManager : NonRetentiveManager<StockEntry, StockEntryDTO>, IStockEntryManager
    {
        public StockEntryManager(IComplexNonRetentiveEntityRepository<StockEntry, StockEntryDTO> repo) : base(repo)
        {

        }
        public StockEntryManager(IBeeMapper mapper) : base(new NonRetentiveStockEntryRepository(new EntityRepository<StockEntryDTO, ExpertContext>(), mapper))
        {

        }
    }
}
