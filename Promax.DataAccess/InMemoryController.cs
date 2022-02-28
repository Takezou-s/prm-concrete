using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Promax.DataAccess
{
    public class InMemoryController<TEntity> : IEntityRepository<TEntity>
        where TEntity : class
    {
        private string _keyPropertyName;
        private IBeeMapper _mapper;
        private MyList<TEntity> _innerList, _oldList;

        public InMemoryController(string keyPropertyName, IBeeMapper mapper)
        {
            _keyPropertyName = keyPropertyName;
            _innerList = new MyList<TEntity>(keyPropertyName);
            _oldList = new MyList<TEntity>(keyPropertyName);
            _mapper = mapper;
        }

        public void Add(TEntity entity)
        {
            _innerList.Add(entity);
            _oldList.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _innerList.Remove(entity);
            _oldList.Remove(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            TEntity result = default(TEntity);
            if (_innerList.Count > 0)
                result = filter == null ?
                    _innerList.FirstOrDefault() :
                    _innerList.Where(filter.Compile()).FirstOrDefault();
            return result;
        }
        public TEntity GetEntity(TEntity entity)
        {
            TEntity result = default(TEntity);
            int entityIndex = _oldList.IndexOf(entity);
            if (entityIndex > -1 && entityIndex < _oldList.Count)
            {
                var newresult = _mapper.Map<TEntity>(_oldList[entityIndex]);
                result = newresult;
            }
            return result;
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            List<TEntity> result = new List<TEntity>();
            if (_innerList.Count > 0)
                result = filter == null ?
                    _innerList.ToList() :
                    _innerList.Where(filter.Compile()).ToList();
            return result;
        }

        public void Update(TEntity entity)
        {
            UpdateInnerList(entity, _innerList);
        }
        public void UpdateOld(TEntity entity)
        {
            UpdateInnerList(entity, _oldList);
        }
        public void Clear()
        {
            _innerList.Clear();
        }

        private void UpdateInnerList(TEntity entity, MyList<TEntity> list)
        {
            int entityIndex = list.IndexOf(entity);
            if (entityIndex > -1 && entityIndex < list.Count)
            {
                var listedEntity = list[entityIndex];
                _mapper.Map(entity, listedEntity, typeof(TEntity), typeof(TEntity));
            }
        }

        private object GetDefaultOfType(Type propertyType)
        {
            object result = null;
            var methodInfo = this.GetType().GetMethod("GetDefault");
            if (methodInfo != null)
            {
                methodInfo.MakeGenericMethod(propertyType);
                result = methodInfo.Invoke(this, null);
            }
            return result;
        }
        private T GetDefault<T>()
        {
            return default(T);
        }
    }
}
