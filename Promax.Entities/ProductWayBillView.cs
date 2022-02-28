using Promax.Core;
using System;
using System.ComponentModel;

namespace Promax.Entities
{
    public class ProductWayBillView:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region FieldsByAutoPropCreator
        private int _productId;
        private DateTime _productDate;
        private DateTime _productTime;
        private DateTime _depTime;
        private int _rtmNumber;
        private int _billNumber;
        private double _shipped;
        private double _delivered;
        private double _totalMaterial;
        private string _clientName;
        private string _clientAddress;
        private string _clientPhone;
        private string _clientTaxLocation;
        private string _clientTaxNumber;
        private string _siteName;
        private string _siteAddress;
        private string _sitePhone;
        private string _siteContact;
        private string _recipeName;
        private string _consistency;
        private string _endurance;
        private string _dmax;
        private string _cementType;
        private string _mineralType;
        private string _additiveType;
        private string _slump;
        private string _unitVolume;
        private string _environmental;
        private string _chlorideContent;
        private double _cemRate;
        private int _mixerServiceId;
        private string _mixerLicencePlate;
        private string _mixerDriver;
        private int _pumpServiceId;
        private string _pumpLicencePlate;
        private string _pumpDriver;
        private string _serviceCategoryName;
        private string _userName;
        private string _additiveName;
        #endregion
        #region PropertiesByAutoPropCreator
        public int ProductId
        {
            get
            {
                return _productId;
            }
            set
            {
                bool changed = false;
                if (!_productId.IsEqual(value))
                    changed = true;
                _productId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ProductId));
                }
            }
        }
        public DateTime ProductDate
        {
            get
            {
                return _productDate;
            }
            set
            {
                bool changed = false;
                if (!_productDate.IsEqual(value))
                    changed = true;
                _productDate = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ProductDate));
                }
            }
        }
        public DateTime ProductTime
        {
            get
            {
                return _productTime;
            }
            set
            {
                bool changed = false;
                if (!_productTime.IsEqual(value))
                    changed = true;
                _productTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ProductTime));
                }
            }
        }
        public DateTime DepTime
        {
            get
            {
                return _depTime;
            }
            set
            {
                bool changed = false;
                if (!_depTime.IsEqual(value))
                    changed = true;
                _depTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(DepTime));
                }
            }
        }
        public int RtmNumber
        {
            get
            {
                return _rtmNumber;
            }
            set
            {
                bool changed = false;
                if (!_rtmNumber.IsEqual(value))
                    changed = true;
                _rtmNumber = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(RtmNumber));
                }
            }
        }
        public int BillNumber
        {
            get
            {
                return _billNumber;
            }
            set
            {
                bool changed = false;
                if (!_billNumber.IsEqual(value))
                    changed = true;
                _billNumber = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(BillNumber));
                }
            }
        }
        public double Shipped
        {
            get
            {
                return _shipped;
            }
            set
            {
                bool changed = false;
                if (!_shipped.IsEqual(value))
                    changed = true;
                _shipped = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Shipped));
                    OnPropertyChanged(nameof(Returned));
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
                    OnPropertyChanged(nameof(Returned));
                }
            }
        }
        public double TotalMaterial
        {
            get
            {
                return _totalMaterial;
            }
            set
            {
                bool changed = false;
                if (!_totalMaterial.IsEqual(value))
                    changed = true;
                _totalMaterial = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(TotalMaterial));
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
        public string ClientAddress
        {
            get
            {
                return _clientAddress;
            }
            set
            {
                bool changed = false;
                if (!_clientAddress.IsEqual(value))
                    changed = true;
                _clientAddress = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ClientAddress));
                }
            }
        }
        public string ClientPhone
        {
            get
            {
                return _clientPhone;
            }
            set
            {
                bool changed = false;
                if (!_clientPhone.IsEqual(value))
                    changed = true;
                _clientPhone = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ClientPhone));
                }
            }
        }
        public string ClientTaxLocation
        {
            get
            {
                return _clientTaxLocation;
            }
            set
            {
                bool changed = false;
                if (!_clientTaxLocation.IsEqual(value))
                    changed = true;
                _clientTaxLocation = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ClientTaxLocation));
                }
            }
        }
        public string ClientTaxNumber
        {
            get
            {
                return _clientTaxNumber;
            }
            set
            {
                bool changed = false;
                if (!_clientTaxNumber.IsEqual(value))
                    changed = true;
                _clientTaxNumber = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ClientTaxNumber));
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
        public string SiteAddress
        {
            get
            {
                return _siteAddress;
            }
            set
            {
                bool changed = false;
                if (!_siteAddress.IsEqual(value))
                    changed = true;
                _siteAddress = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SiteAddress));
                }
            }
        }
        public string SitePhone
        {
            get
            {
                return _sitePhone;
            }
            set
            {
                bool changed = false;
                if (!_sitePhone.IsEqual(value))
                    changed = true;
                _sitePhone = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SitePhone));
                }
            }
        }
        public string SiteContact
        {
            get
            {
                return _siteContact;
            }
            set
            {
                bool changed = false;
                if (!_siteContact.IsEqual(value))
                    changed = true;
                _siteContact = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SiteContact));
                }
            }
        }
        public string RecipeName
        {
            get
            {
                return _recipeName;
            }
            set
            {
                bool changed = false;
                if (!_recipeName.IsEqual(value))
                    changed = true;
                _recipeName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(RecipeName));
                }
            }
        }
        public string Consistency
        {
            get
            {
                return _consistency;
            }
            set
            {
                bool changed = false;
                if (!_consistency.IsEqual(value))
                    changed = true;
                _consistency = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Consistency));
                }
            }
        }
        public string Endurance
        {
            get
            {
                return _endurance;
            }
            set
            {
                bool changed = false;
                if (!_endurance.IsEqual(value))
                    changed = true;
                _endurance = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Endurance));
                }
            }
        }
        public string Dmax
        {
            get
            {
                return _dmax;
            }
            set
            {
                bool changed = false;
                if (!_dmax.IsEqual(value))
                    changed = true;
                _dmax = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Dmax));
                }
            }
        }
        public string CementType
        {
            get
            {
                return _cementType;
            }
            set
            {
                bool changed = false;
                if (!_cementType.IsEqual(value))
                    changed = true;
                _cementType = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(CementType));
                }
            }
        }
        public string MineralType
        {
            get
            {
                return _mineralType;
            }
            set
            {
                bool changed = false;
                if (!_mineralType.IsEqual(value))
                    changed = true;
                _mineralType = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(MineralType));
                }
            }
        }
        public string AdditiveType
        {
            get
            {
                return _additiveType;
            }
            set
            {
                bool changed = false;
                if (!_additiveType.IsEqual(value))
                    changed = true;
                _additiveType = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(AdditiveType));
                }
            }
        }
        public string Slump
        {
            get
            {
                return _slump;
            }
            set
            {
                bool changed = false;
                if (!_slump.IsEqual(value))
                    changed = true;
                _slump = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Slump));
                }
            }
        }
        public string UnitVolume
        {
            get
            {
                return _unitVolume;
            }
            set
            {
                bool changed = false;
                if (!_unitVolume.IsEqual(value))
                    changed = true;
                _unitVolume = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(UnitVolume));
                }
            }
        }
        public string Environmental
        {
            get
            {
                return _environmental;
            }
            set
            {
                bool changed = false;
                if (!_environmental.IsEqual(value))
                    changed = true;
                _environmental = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Environmental));
                }
            }
        }
        public string ChlorideContent
        {
            get
            {
                return _chlorideContent;
            }
            set
            {
                bool changed = false;
                if (!_chlorideContent.IsEqual(value))
                    changed = true;
                _chlorideContent = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ChlorideContent));
                }
            }
        }
        public double CemRate
        {
            get
            {
                return _cemRate;
            }
            set
            {
                bool changed = false;
                if (!_cemRate.IsEqual(value))
                    changed = true;
                _cemRate = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(CemRate));
                }
            }
        }
        public int MixerServiceId
        {
            get
            {
                return _mixerServiceId;
            }
            set
            {
                bool changed = false;
                if (!_mixerServiceId.IsEqual(value))
                    changed = true;
                _mixerServiceId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(MixerServiceId));
                }
            }
        }
        public string MixerLicencePlate
        {
            get
            {
                return _mixerLicencePlate;
            }
            set
            {
                bool changed = false;
                if (!_mixerLicencePlate.IsEqual(value))
                    changed = true;
                _mixerLicencePlate = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(MixerLicencePlate));
                }
            }
        }
        public string MixerDriver
        {
            get
            {
                return _mixerDriver;
            }
            set
            {
                bool changed = false;
                if (!_mixerDriver.IsEqual(value))
                    changed = true;
                _mixerDriver = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(MixerDriver));
                }
            }
        }
        public int PumpServiceId
        {
            get
            {
                return _pumpServiceId;
            }
            set
            {
                bool changed = false;
                if (!_pumpServiceId.IsEqual(value))
                    changed = true;
                _pumpServiceId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(PumpServiceId));
                }
            }
        }
        public string PumpLicencePlate
        {
            get
            {
                return _pumpLicencePlate;
            }
            set
            {
                bool changed = false;
                if (!_pumpLicencePlate.IsEqual(value))
                    changed = true;
                _pumpLicencePlate = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(PumpLicencePlate));
                }
            }
        }
        public string PumpDriver
        {
            get
            {
                return _pumpDriver;
            }
            set
            {
                bool changed = false;
                if (!_pumpDriver.IsEqual(value))
                    changed = true;
                _pumpDriver = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(PumpDriver));
                }
            }
        }
        public string ServiceCategoryName
        {
            get
            {
                return _serviceCategoryName;
            }
            set
            {
                bool changed = false;
                if (!_serviceCategoryName.IsEqual(value))
                    changed = true;
                _serviceCategoryName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ServiceCategoryName));
                }
            }
        }
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                bool changed = false;
                if (!_userName.IsEqual(value))
                    changed = true;
                _userName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }
        public string AdditiveName
        {
            get
            {
                return _additiveName;
            }
            set
            {
                bool changed = false;
                if (!_additiveName.IsEqual(value))
                    changed = true;
                _additiveName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(AdditiveName));
                }
            }
        }
        #endregion



        public double Returned { get => Shipped - Delivered; }
    }
    public class ClientRecipeView
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string SiteName { get; set; }
        public string RecipeName { get; set; }
        public double MixerServiceAmount { get; set; }
        public double PumpServiceAmount { get; set; }
        public double CentralServiceAmount { get; set; }
        public double Shipped { get; set; }
        public double Delivered { get; set; }

        public double Returned { get => Shipped - Delivered; }
    }
    public class MixerServiceReportView
    {
        public string MixerName { get; set; }
        public string DriverName { get; set; }
        public string Distance { get; set; }
        public double Shipped { get; set; }
        public double Delivered { get; set; }


        public double Returned { get => Shipped - Delivered; }
    }
}
