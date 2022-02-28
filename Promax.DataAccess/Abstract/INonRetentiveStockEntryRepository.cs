using Promax.Entities;

namespace Promax.DataAccess
{
    public interface INonRetentiveStockEntryRepository : IComplexNonRetentiveEntityRepository<StockEntry, StockEntryDTO>
    {

    }
}