using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class SiloManager : Manager<Silo>, ISiloManager
    {
        public SiloManager(IEntityRepository<Silo> repo) : base(repo)
        {

        }
        public SiloManager(IBeeMapper mapper) : base(new RetentiveSiloRepository(new EntityRepository<SiloDTO, ExpertContext>(), mapper))
        {

        }
    }
}
