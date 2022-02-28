using Extensions;
using Promax.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class Client : INotifyPropertyChanged
    {
        private string _gravity = "false";
        private string _isHidden = "false";
        private string _enableNotification = "false";
        private MyList<Site> _activeSitesList = new MyList<Site>(nameof(Site.SiteId));
        private MyList<Site> _allSitesList = new MyList<Site>(nameof(Site.SiteId));

        #region FieldsByAutoPropCreater
        private int _clientId = -1;
        private string _clientCode = string.Empty;
        private string _clientName = string.Empty;
        private string _address = string.Empty;
        private string _state = string.Empty;
        private string _city = string.Empty;
        private string _addressLine = string.Empty;
        private string _contact = string.Empty;
        private string _phone = string.Empty;
        private string _taxLocation = string.Empty;
        private string _taxNumber = string.Empty;
        private short _clientType;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _clientTitle = string.Empty;
        private string _email = string.Empty;
        private short _mailAttachInfo;
        private double _delivered;
        #endregion
        #region PropertiesByAutoPropCreator
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
        public string ClientCode
        {
            get
            {
                return _clientCode;
            }
            set
            {
                bool changed = false;
                if (!_clientCode.IsEqual(value))
                    changed = true;
                _clientCode = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ClientCode));
                }
            }
        }
        public string ClientName
        {
            get
            {
                return _clientName;
            }
            set
            {
                bool changed = false;
                if (!_clientName.IsEqual(value))
                    changed = true;
                _clientName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ClientName));
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
        public string TaxLocation
        {
            get
            {
                return _taxLocation;
            }
            set
            {
                bool changed = false;
                if (!_taxLocation.IsEqual(value))
                    changed = true;
                _taxLocation = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(TaxLocation));
                }
            }
        }
        public string TaxNumber
        {
            get
            {
                return _taxNumber;
            }
            set
            {
                bool changed = false;
                if (!_taxNumber.IsEqual(value))
                    changed = true;
                _taxNumber = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(TaxNumber));
                }
            }
        }
        public string Gravity
        {
            get => _gravity;
            set
            {
                bool changed = false;
                if (!_gravity.IsEqual(value))
                    changed = true;
                _gravity = value.Boolify();
                if (changed)
                {
                    OnPropertyChanged(nameof(Gravity));
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
        public short ClientType
        {
            get
            {
                return _clientType;
            }
            set
            {
                bool changed = false;
                if (!_clientType.IsEqual(value))
                    changed = true;
                _clientType = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ClientType));
                }
            }
        }
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                bool changed = false;
                if (!_firstName.IsEqual(value))
                    changed = true;
                _firstName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                bool changed = false;
                if (!_lastName.IsEqual(value))
                    changed = true;
                _lastName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }
        public string ClientTitle
        {
            get
            {
                return _clientTitle;
            }
            set
            {
                bool changed = false;
                if (!_clientTitle.IsEqual(value))
                    changed = true;
                _clientTitle = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ClientTitle));
                }
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                bool changed = false;
                if (!_email.IsEqual(value))
                    changed = true;
                _email = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public string EnableNotification
        {
            get => _enableNotification; set
            {
                bool changed = false;
                if (!_enableNotification.IsEqual(value))
                    changed = true;
                _enableNotification = value.Boolify();
                if (changed)
                {
                    OnPropertyChanged(nameof(EnableNotification));
                }
            }
        }
        public short MailAttachInfo
        {
            get
            {
                return _mailAttachInfo;
            }
            set
            {
                bool changed = false;
                if (!_mailAttachInfo.IsEqual(value))
                    changed = true;
                _mailAttachInfo = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(MailAttachInfo));
                }
            }
        }
        public double Delivered
        {
            get
            {
                return _delivered;
            }
            set
            {
                bool changed = false;
                if (!_delivered.IsEqual(value))
                    changed = true;
                _delivered = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Delivered));
                }
            }
        }
        #endregion
        public ClientType ClientTypeEnum
        {
            get => (ClientType)ClientType; set
            {
                bool changed = false;
                if (!ClientTypeEnum.IsEqual(value))
                    changed = true;
                ClientType = (short)value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ClientTypeEnum));
                }
            }
        }
        public IEnumerable<Site> ActiveSites => _activeSitesList;
        public IEnumerable<Site> AllSites => _allSitesList;
        #region Listing
        public void AddSite(Site site)
        {
            site.DoIf(x => !x.IsHidden.GetBool(), x => x.DoReturn(y => AddToList(y, _activeSitesList)).Do(y => OnPropertyChanged(nameof(ActiveSites))));
            site.Do(x => x.DoReturn(y => AddToList(y, _allSitesList)).Do(y => OnPropertyChanged(nameof(AllSites))));
            site.Client = this;
        }
        public void RemoveSite(Site site)
        {
            RemoveFromList(site, _activeSitesList);
            OnPropertyChanged(nameof(ActiveSites));
            RemoveFromList(site, _allSitesList);
            OnPropertyChanged(nameof(AllSites));
        }

        public void ClearSites()
        {
            _activeSitesList.Clear();
            _allSitesList.Clear();
            OnPropertyChanged(nameof(ActiveSites));
            OnPropertyChanged(nameof(AllSites));
        }
        public void UpdateAtList(Site site)
        {
            site.DoIfElse(x => x.IsHidden.GetBool(), x => x.DoReturn(y => RemoveFromList(y, _activeSitesList)).Do(y => OnPropertyChanged(nameof(ActiveSites))), null);
        }
        private void AddToList(Site site, IList<Site> list)
        {
            if (list.Contains(site) || list.FirstOrDefault(x => x.SiteId == site.SiteId) != null)
                return;
            list.Add(site);
        }
        private void RemoveFromList(Site site, IList<Site> list)
        {
            if (list.Contains(site))
                list.Remove(site);
            list.FirstOrDefault(x => x.SiteId == site.SiteId)?.Do(x => list.Remove(x));
        }
        #endregion
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
    }
}
