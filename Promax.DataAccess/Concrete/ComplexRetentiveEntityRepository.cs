using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Utility.Triggers;

namespace Promax.DataAccess
{
    public class ComplexRetentiveEntityRepository<TEntity, TDto> : IComplexRetentiveEntityRepository<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        protected IEntityRepository<TDto> _database;
        protected InMemoryController<TEntity> _inMemoryController;
        protected IBeeMapper _mapper;
        protected Expression<Func<TDto, bool>> _innerListFilter;
        private bool _initialized;
        public TriggerPerformer<TEntity> TriggerPeformer { get; } = new TriggerPerformer<TEntity>();
        protected ComplexRetentiveEntityRepository(IEntityRepository<TDto> database, string keyPropertyName, IBeeMapper mapper)
        {
            _database = database;
            _inMemoryController = new InMemoryController<TEntity>(keyPropertyName, mapper);
            _mapper = mapper;
        }
        protected ComplexRetentiveEntityRepository(IEntityRepository<TDto> database, string keyPropertyName, IBeeMapper mapper, Expression<Func<TDto, bool>> innerListFilter)
        {
            _database = database;
            _inMemoryController = new InMemoryController<TEntity>(keyPropertyName, mapper);
            _mapper = mapper;
            _innerListFilter = innerListFilter;
        }
        public virtual void Add(TEntity entity)
        {
            var a = _mapper.Map<TDto>(entity);
            _database.Add(a);
            _mapper.Map<TDto, TEntity>(a, entity);
            TEntity old = default(TEntity);
            TriggerPeformer.Perform(TriggerInfo.Get().Before().Insert(), entity, old);
            _inMemoryController.Add(entity);
            TriggerPeformer.Perform(TriggerInfo.Get().After().Insert(), entity, old);
        }

        public virtual void Delete(TEntity entity)
        {
            _database.Delete(_mapper.Map<TDto>(entity));
            TEntity old = default(TEntity);
            TriggerPeformer.Perform(TriggerInfo.Get().Before().Delete(), entity, old);
            _inMemoryController.Delete(entity);
            TriggerPeformer.Perform(TriggerInfo.Get().After().Delete(), entity, old);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            TEntity result = default(TEntity);
            InitInMemoryController();
            result = _inMemoryController.Get(filter);
            return result;
        }

        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            List<TEntity> result = new List<TEntity>();
            InitInMemoryController();
            result = _inMemoryController.GetList(filter);
            return result;
        }

        public virtual void Update(TEntity entity)
        {
            _database.Update(_mapper.Map<TDto>(entity));
            TEntity old = _inMemoryController.GetEntity(entity);
            TriggerPeformer.Perform(TriggerInfo.Get().Before().Update(), entity, old);
            _inMemoryController.Update(entity);
            TriggerPeformer.Perform(TriggerInfo.Get().After().Update(), entity, old);
            _inMemoryController.UpdateOld(entity);
        }
        public virtual void ReInit()
        {
            _initialized = false;
            InitInMemoryController();
        }
        private void InitInMemoryController()
        {
            if (!_initialized)
            {
                _inMemoryController.Clear();
                _database.GetList(_innerListFilter).ForEach(x => _inMemoryController.Add(_mapper.Map<TEntity>(x)));
                _initialized = true;
            }
        }
    }
}
