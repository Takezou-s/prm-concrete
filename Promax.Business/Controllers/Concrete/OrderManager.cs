using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class OrderManager : Manager<Order>, IOrderManager
    {
        public OrderManager(IEntityRepository<Order> repo) : base(repo)
        {

        }
        public OrderManager(IBeeMapper mapper) : base(new RetentiveOrderRepository(new EntityRepository<OrderDTO, ExpertContext>(), mapper))
        {

        }
    }
}
