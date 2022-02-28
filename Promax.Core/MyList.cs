using System;
using System.Collections;
using System.Collections.Generic;

namespace Promax.Core
{
    /// <summary>
    /// Contains ve remove işlemlerini belirtilen key property'e göre yapar.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MyList<TEntity> : IList<TEntity>
    {
        private IList<TEntity> _innerList = new List<TEntity>();
        string _keyPropertyName;

        public MyList(string keyPropertyName)
        {
            _keyPropertyName = keyPropertyName;
        }

        private void DeleteInnerList(TEntity entity)
        {
            int entityIndex = FindIndex(entity);
            if (entityIndex > -1 && entityIndex < _innerList.Count)
            {
                _innerList.RemoveAt(entityIndex);
            }
        }
        private int FindIndex(TEntity entity)
        {
            object keyPropertyValue = GetKeyPropertyValue(entity);
            int index = -1;
            for (int i = 0; i < _innerList.Count; i++)
            {
                if (_innerList[i] != null && keyPropertyValue.Equals(GetKeyPropertyValue(_innerList[i])))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        private object GetKeyPropertyValue(TEntity entity)
        {
            object result = null;
            var propertyInfo = typeof(TEntity).GetProperty(_keyPropertyName);
            if (propertyInfo != null)
            {
                Type propertyType = propertyInfo.PropertyType;
                result = GetDefaultOfType(propertyType);
                var value = propertyInfo.GetValue(entity);
                if (value != null)
                {
                    result = value;
                }
            }
            return result;
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

        #region IList<T> interface


        public int IndexOf(TEntity item)
        {
            return FindIndex(item);
            //return _innerList.IndexOf(item);
        }
        public bool Contains(TEntity item)
        {
            return FindIndex(item) >= 0;
            //return _innerList.Contains(item);
        }
        public bool Remove(TEntity item)
        {
            _innerList.Remove(item);
            DeleteInnerList(item);
            return true;
        }
        #region Through the controller
        public TEntity this[int index] { get => _innerList[index]; set => _innerList[index] = value; }

        public int Count => _innerList.Count;

        public bool IsReadOnly => _innerList.IsReadOnly;

        public void Add(TEntity item)
        {
            _innerList.Add(item);
        }

        public void Clear()
        {
            //throw new Exception("Kim çağırıyo lan seni");
            _innerList.Clear();
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            _innerList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        public void Insert(int index, TEntity item)
        {
            _innerList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _innerList.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_innerList).GetEnumerator();
        }
        #endregion 
        #endregion
    }
}
