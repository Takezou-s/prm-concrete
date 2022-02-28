using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class RetentiveRecipeRepository : ComplexRetentiveEntityRepository<Recipe, RecipeDTO>, IRetentiveRecipeRepository
    {
        public RetentiveRecipeRepository(IEntityRepository<RecipeDTO> database, IBeeMapper mapper) : base(database, nameof(RecipeDTO.RecipeId), mapper, null)
        {

        }
    }
}
