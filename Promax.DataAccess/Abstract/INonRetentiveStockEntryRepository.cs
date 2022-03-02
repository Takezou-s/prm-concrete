using Promax.Entities;

namespace Promax.DataAccess
{
    public interface INonRetentiveStockEntryRepository : IComplexNonRetentiveEntityRepository<StockEntry, StockEntryDTO>
    {

    }
    public interface INonRetentiveNormViewRepository : IComplexNonRetentiveEntityRepository<NormViewDTO, NormViewDTO>
    {

    }
}