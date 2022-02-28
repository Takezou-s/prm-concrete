using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class RetentiveClientRepository : ComplexRetentiveEntityRepository<Client, ClientDTO>, IRetentiveClientRepository
    {
        public RetentiveClientRepository(IEntityRepository<ClientDTO> database, IBeeMapper mapper) : base(database, nameof(ClientDTO.ClientId), mapper, null)
        {

        }
    }
}
