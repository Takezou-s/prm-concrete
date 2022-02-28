using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class RetentiveRecipeContentRepository : ComplexRetentiveEntityRepository<RecipeContent, RecipeContentDTO>, IRetentiveRecipeContentRepository
    {
        public RetentiveRecipeContentRepository(IEntityRepository<RecipeContentDTO> database, IBeeMapper mapper) : base(database, nameof(RecipeContentDTO.ContentId), mapper, null)
        {

        }
    }
}
