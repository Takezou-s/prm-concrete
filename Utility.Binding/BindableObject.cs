using Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Binding
{
    public class BindableObject : INotifyPropertyChanged
    {
        private Dictionary<string, INotifyPropertyChanged> _subbed = new Dictionary<string, INotifyPropertyChanged>();
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        protected void SetValue(string propertyName, object value, string fieldName)
        {
            var propertyValue = ReflectionController.GetPropertyValue(this, propertyName);
            bool changed = !propertyValue.IsEqual(value);
            ReflectionController.SetPropertyValue(this, fieldName, value);
            if (changed)
                OnPropertyChanged(propertyName);

            Unsubscribe(propertyName);

            Subscribe(propertyName, value);

            var propertyInfo = ReflectionController.GetPropertyInfo(this, propertyName);
            if(propertyInfo != null && (typeof(INotifyPropertyChanged)).IsAssignableFrom(propertyInfo.PropertyType))
            {
                var propertyInfos = propertyInfo.PropertyType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                foreach (var info in propertyInfos)
                {
                    OnPropertyChanged(propertyName + "." + info.Name);
                }
            }
        }

        private void PropChanged(object sender, PropertyChangedEventArgs e)
        {
            string propertyName = string.Empty;
            foreach (var item in _subbed)
            {
                if(item.Value.Equals(sender))
                {
                    propertyName = item.Key;
                    break;
                }
            }
            if(!string.IsNullOrEmpty(propertyName))
            {
                OnPropertyChanged(propertyName + "." + e.PropertyName);
            }
        }
        protected BindableObject()
        {
            InitProps();
        }
        protected void InitProps()
        {
            var propertyInfos = this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in propertyInfos)
            {
                object value = ReflectionController.GetPropertyValue(this, propertyInfo.Name);
                Subscribe(propertyInfo.Name, value);
            }
        }
        private void Subscribe(string propertyName, object value)
        {
            if (value != null && value is INotifyPropertyChanged)
            {
                if (_subbed.ContainsKey(propertyName))
                {
                    _subbed[propertyName].Do(x=>x.PropertyChanged -= PropChanged);
                    _subbed.Remove(propertyName);
                }
                INotifyPropertyChanged notifyPropertyChanged = (value as INotifyPropertyChanged);
                notifyPropertyChanged.PropertyChanged += PropChanged;
                _subbed.Add(propertyName, notifyPropertyChanged);
            }
        }
        private void Unsubscribe(string propertyName)
        {
            if (_subbed.ContainsKey(propertyName))
            {
                _subbed[propertyName].Do(x => x.PropertyChanged -= PropChanged);
                _subbed.Remove(propertyName);
            }
        }
    }
}
