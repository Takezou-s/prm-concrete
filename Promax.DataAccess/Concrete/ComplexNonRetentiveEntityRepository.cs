using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Utility.Triggers;

namespace Promax.DataAccess
{
    public class ComplexNonRetentiveEntityRepository<TEntity, TDto> : IComplexNonRetentiveEntityRepository<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        protected IEntityRepository<TDto> _database;
        protected IBeeMapper _mapper;
        public TriggerPerformer<TEntity> TriggerPeformer { get; } = new TriggerPerformer<TEntity>();
        protected ComplexNonRetentiveEntityRepository(IEntityRepository<TDto> database, IBeeMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }
        public virtual void Add(TEntity entity)
        {
            var a = _mapper.Map<TDto>(entity);
            TEntity old = default(TEntity);
            TriggerPeformer.Perform(TriggerInfo.Get().Before().Insert(), entity, old);
            _database.Add(a);
            _mapper.Map<TDto, TEntity>(a, entity);
            TriggerPeformer.Perform(TriggerInfo.Get().After().Insert(), entity, old);
        }

        public virtual void Delete(TEntity entity)
        {
            TEntity old = default(TEntity);
            TriggerPeformer.Perform(TriggerInfo.Get().Before().Delete(), entity, old);
            _database.Delete(_mapper.Map<TDto>(entity));
            TriggerPeformer.Perform(TriggerInfo.Get().After().Delete(), entity, old);
        }

        public virtual TEntity Get(Expression<Func<TDto, bool>> filter)
        {
            return _mapper.Map<TEntity>(_database.Get(filter));
        }

        public virtual List<TEntity> GetList(Expression<Func<TDto, bool>> filter = null)
        {
            return _mapper.Map<List<TEntity>>(_database.GetList(filter));
        }

        public virtual void Update(TEntity newObj, TEntity oldObj)
        {
            TriggerPeformer.Perform(TriggerInfo.Get().Before().Update(), newObj, oldObj);
            _database.Update(_mapper.Map<TDto>(newObj));
            TriggerPeformer.Perform(TriggerInfo.Get().After().Update(), newObj, oldObj);
        }
    }
}
