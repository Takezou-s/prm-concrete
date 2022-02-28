using Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Core
{
    public delegate void ObjectRegisteredEventHandler<T>(object container, T registeredObject);
    public delegate void ObjectRegisteredEventHandler<T1, T2>(object container, T1 registeredT1, T2 registeredT2);
    public class Container<T> : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private List<T> _objects = new List<T>();
        public IEnumerable<T> Objects => _objects;
        public event ObjectRegisteredEventHandler<T> ObjectRegistered;
        public event ObjectRegisteredEventHandler<T> ObjectUnregistered;
        public void Register(T obj)
        {
            if (_objects.Contains(obj))
                return;
            _objects.Add(obj);
            OnPropertyChanged(nameof(Objects));
            ObjectRegistered?.Invoke(this, obj);
        }
        public void Unregister(T obj)
        {
            if (!_objects.Contains(obj))
                return;
            _objects.Remove(obj);
            OnPropertyChanged(nameof(Objects));
            ObjectUnregistered?.Invoke(this, obj);
        }
        public void ClearRegistrations()
        {
            _objects.Clear();
            OnPropertyChanged(nameof(Objects));
        }
    }
    public class Container<TKey, TValue> : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private Dictionary<TKey, TValue> _objects = new Dictionary<TKey, TValue>();
        public IReadOnlyDictionary<TKey, TValue> Objects => _objects;
        public event ObjectRegisteredEventHandler<TKey, TValue> ObjectRegistered;
        public event ObjectRegisteredEventHandler<TKey, TValue> ObjectUnregistered;
        public void Register(TKey key, TValue value)
        {
            if (_objects.ContainsKey(key))
                return;
            _objects.DoIfElse(x => !x.ContainsKey(key), x => x.Add(key, value), x => x[key] = value);
            OnPropertyChanged(nameof(Objects));
            ObjectRegistered?.Invoke(this, key, value);
        }
        public void ClearRegistrations()
        {
            _objects.Clear();
            OnPropertyChanged(nameof(Objects));
        }
    }
}
