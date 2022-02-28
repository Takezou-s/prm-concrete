using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class RetentiveStockRepository : ComplexRetentiveEntityRepository<Stock, StockDTO>, IRetentiveStockRepository
    {
        public RetentiveStockRepository(IEntityRepository<StockDTO> database, IBeeMapper mapper) : base(database, nameof(StockDTO.StockId), mapper, null)
        {

        }
    }
}
