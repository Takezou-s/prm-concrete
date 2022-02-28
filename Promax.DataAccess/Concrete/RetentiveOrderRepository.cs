using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class RetentiveOrderRepository : ComplexRetentiveEntityRepository<Order, OrderDTO>, IRetentiveOrderRepository
    {
        public RetentiveOrderRepository(IEntityRepository<OrderDTO> database, IBeeMapper mapper) : base(database, nameof(OrderDTO.OrderId), mapper, null)
        {

        }
    }
}
