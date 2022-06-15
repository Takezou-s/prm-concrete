using Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class Settings : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region FieldsByAutoPropCreator
        private string _ip;
        private int _timeout;
        #endregion
        #region PropertiesByAutoPropCreator
        public string Ip
        {
            get
            {
                return _ip;
            }
            set
            {
                bool changed = false;
                if (!_ip.IsEqual(value))
                    changed = true;
                _ip = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Ip));
                }
            }
        }
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                bool changed = false;
                if (!_timeout.IsEqual(value))
                    changed = true;
                _timeout = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Timeout));
                }
            }
        }

        #endregion

    }
}
