using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class RetentiveUserRepository : ComplexRetentiveEntityRepository<User, UserDTO>, IRetentiveUserRepository
    {
        public RetentiveUserRepository(IEntityRepository<UserDTO> database, IBeeMapper mapper) : base(database, nameof(UserDTO.UserId), mapper, null)
        {

        }
    }
}
