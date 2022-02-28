namespace Promax.DataAccess
{
    public interface IComplexRetentiveEntityRepository<TEntity, TDto> : IEntityRepository<TEntity>
        where TEntity : class
        where TDto : class
    {

    }
}
