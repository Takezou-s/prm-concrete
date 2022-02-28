using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Promax.DataAccess
{
    public interface IComplexNonRetentiveEntityRepository<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        List<TEntity> GetList(Expression<Func<TDto, bool>> filter = null);
        TEntity Get(Expression<Func<TDto, bool>> filter);
        void Add(TEntity entity);
        void Update(TEntity newObj, TEntity oldObj);
        void Delete(TEntity entity);
    }
}
