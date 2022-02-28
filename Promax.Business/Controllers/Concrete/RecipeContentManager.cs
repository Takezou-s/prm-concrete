using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class RecipeContentManager : Manager<RecipeContent>, IRecipeContentManager
    {
        public RecipeContentManager(IEntityRepository<RecipeContent> repo) : base(repo)
        {

        }
        public RecipeContentManager(IBeeMapper mapper) : base(new RetentiveRecipeContentRepository(new EntityRepository<RecipeContentDTO, ExpertContext>(), mapper))
        {

        }
    }
}
