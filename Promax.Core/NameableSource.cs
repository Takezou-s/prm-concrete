using Extensions;
using RemoteVariableHandler.Core;
using System.ComponentModel;

namespace Promax.Core
{
    public class NameableSource : INameableSource, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region FieldsByAutoPropCreator
        private string _nameableId;
        private string _displayName;
        #endregion
        #region PropertiesByAutoPropCreator
        public string NameableId
        {
            get
            {
                return _nameableId;
            }
            set
            {
                bool changed = false;
                if (!_nameableId.IsEqual(value))
                    changed = true;
                _nameableId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(NameableId));
                }
            }
        }
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                bool changed = false;
                if (!_displayName.IsEqual(value))
                    changed = true;
                _displayName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(DisplayName));
                }
            }
        }
        #endregion
    }
}
