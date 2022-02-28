using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class ClientManager : Manager<Client>, IClientManager
    {
        public ClientManager(IEntityRepository<Client> repo) : base(repo)
        {

        }
        public ClientManager(IBeeMapper mapper) : base(new RetentiveClientRepository(new EntityRepository<ClientDTO, ExpertContext>(), mapper))
        {

        }
    }
}
