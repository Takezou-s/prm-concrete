using Promax.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Business
{
    public class Manager<T> : IManager<T>
        where T : class
    {
        private IEntityRepository<T> _repo;

        public Manager(IEntityRepository<T> repo)
        {
            _repo = repo;
        }

        public virtual void Add(T entity)
        {
            _repo.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _repo.Delete(entity);
        }

        public virtual T Get(Expression<Func<T, bool>> filter)
        {
            return _repo.Get(filter);
        }

        public virtual List<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            return _repo.GetList(filter);
        }

        public virtual void Update(T entity)
        {
            _repo.Update(entity);
        }
    }
}
