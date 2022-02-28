using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class NonRetentiveStockEntryRepository : ComplexNonRetentiveEntityRepository<StockEntry, StockEntryDTO>, INonRetentiveStockEntryRepository
    {
        public NonRetentiveStockEntryRepository(IEntityRepository<StockEntryDTO> database, IBeeMapper mapper) : base(database, mapper)
        {
        }
    }
}
