using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class RecipeManager : Manager<Recipe>, IRecipeManager
    {
        public RecipeManager(IEntityRepository<Recipe> repo) : base(repo)
        {

        }
        public RecipeManager(IBeeMapper mapper) : base(new RetentiveRecipeRepository(new EntityRepository<RecipeDTO, ExpertContext>(), mapper))
        {

        }
    }
}
