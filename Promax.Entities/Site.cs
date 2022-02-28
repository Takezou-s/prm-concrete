using Extensions;
using Promax.Core;
using System.ComponentModel;

namespace Promax.Entities
{
    public class Site : INotifyPropertyChanged
    {
        private Client _client;
        private string _isHidden="false";
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        #region FieldsByAutoPropCreator
        private int _siteId = -1;
        private int _clientId;
        private string _siteCode;
        private string _siteName;
        private string _address;
        private string _state;
        private string _city;
        private string _addressLine;
        private string _contact;
        private string _phone;
        private int _distance;
        #endregion
        #region PropertiesByAutoPropCreator
        public int SiteId
        {
            get
            {
                return _siteId;
            }
            set
            {
                bool changed = false;
                if (!_siteId.IsEqual(value))
                    changed = true;
                _siteId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SiteId));
                }
            }
        }
        public int ClientId
        {
            get
            {
                return _clientId;
            }
            set
            {
                bool changed = false;
                if (!_clientId.IsEqual(value))
                    changed = true;
                _clientId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ClientId));
                }
            }
        }
        public string SiteCode
        {
            get
            {
                return _siteCode;
            }
            set
            {
                bool changed = false;
                if (!_siteCode.IsEqual(value))
                    changed = true;
                _siteCode = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SiteCode));
                }
            }
        }
        public string SiteName
        {
            get
            {
                return _siteName;
            }
            set
            {
                bool changed = false;
                if (!_siteName.IsEqual(value))
                    changed = true;
                _siteName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SiteName));
                }
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                bool changed = false;
                if (!_address.IsEqual(value))
                    changed = true;
                _address = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Address));
                }
            }
        }
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                bool changed = false;
                if (!_state.IsEqual(value))
                    changed = true;
                _state = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(State));
                }
            }
        }
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                bool changed = false;
                if (!_city.IsEqual(value))
                    changed = true;
                _city = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(City));
                }
            }
        }
        public string AddressLine
        {
            get
            {
                return _addressLine;
            }
            set
            {
                bool changed = false;
                if (!_addressLine.IsEqual(value))
                    changed = true;
                _addressLine = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(AddressLine));
                }
            }
        }
        public string Contact
        {
            get
            {
                return _contact;
            }
            set
            {
                bool changed = false;
                if (!_contact.IsEqual(value))
                    changed = true;
                _contact = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Contact));
                }
            }
        }
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                bool changed = false;
                if (!_phone.IsEqual(value))
                    changed = true;
                _phone = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Phone));
                }
            }
        }
        public int Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                bool changed = false;
                if (!_distance.IsEqual(value))
                    changed = true;
                _distance = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Distance));
                }
            }
        }
        public string IsHidden
        {
            get => _isHidden; set
            {
                bool changed = false;
                if (!_isHidden.IsEqual(value))
                    changed = true;
                _isHidden = value.Boolify();
                if (changed)
                {
                    OnPropertyChanged(nameof(IsHidden));
                }
            }
        }
        #endregion
        public Client Client
        {
            get => _client; set
            {
                bool changed = false;
                if (!_client.IsEqual(value))
                    changed = true;
                _client = value;
                value.Do(o => ClientId = o.ClientId, () => ClientId = -1);
                if (changed)
                {
                    OnPropertyChanged(nameof(Client));
                }
            }
        }
    }
}