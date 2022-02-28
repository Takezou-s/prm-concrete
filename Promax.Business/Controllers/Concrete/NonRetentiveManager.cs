using Promax.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Promax.Business
{
    public class NonRetentiveManager<TEntity, TDto> : IComplexNonRetentiveManager<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        private IComplexNonRetentiveEntityRepository<TEntity, TDto> _repo;

        public NonRetentiveManager(IComplexNonRetentiveEntityRepository<TEntity, TDto> repo)
        {
            _repo = repo;
        }

        public virtual void Add(TEntity entity)
        {
            _repo.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _repo.Delete(entity);
        }

        public virtual TEntity Get(Expression<Func<TDto, bool>> filter)
        {
            return _repo.Get(filter);
        }

        public virtual List<TEntity> GetList(Expression<Func<TDto, bool>> filter = null)
        {
            return _repo.GetList(filter);
        }

        public virtual void Update(TEntity newObj, TEntity oldObj)
        {
            _repo.Update(newObj, oldObj);
        }
    }
}
