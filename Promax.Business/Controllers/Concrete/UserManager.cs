using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class UserManager : Manager<User>, IUserManager
    {
        public UserManager(IEntityRepository<User> repo) : base(repo)
        {

        }
        public UserManager(IBeeMapper mapper) : base(new RetentiveUserRepository(new EntityRepository<UserDTO, ExpertContext>(), mapper))
        {

        }
    }
}
