using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class DriverManager : Manager<Driver>, IDriverManager
    {
        public DriverManager(IEntityRepository<Driver> repo) : base(repo)
        {

        }
        public DriverManager(IBeeMapper mapper) : base(new RetentiveDriverRepository(new EntityRepository<DriverDTO, ExpertContext>(), mapper))
        {

        }
    }
}
