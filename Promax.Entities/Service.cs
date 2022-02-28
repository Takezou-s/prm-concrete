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
    public class Service : INotifyPropertyChanged
    {
        private ServiceCategoryDTO _serviceCategory;
        private Driver _driver;
        
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        #region FieldsByAutoPropCreator
        private int _serviceId = -1;
        private string _serviceCode;
        private string _serviceName;
        private int _serviceCatNum;
        private string _licencePlate;
        private int _driverId;
        private double _capacity;
        private string _gravity = "false";
        private string _isHidden = "false";
        private double _returned;
        private double _transfer;
        #endregion
        #region PropertiesByAutoPropCreator
        public int ServiceId
        {
            get
            {
                return _serviceId;
            }
            set
            {
                bool changed = false;
                if (!_serviceId.IsEqual(value))
                    changed = true;
                _serviceId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ServiceId));
                }
            }
        }
        public string ServiceCode
        {
            get
            {
                return _serviceCode;
            }
            set
            {
                bool changed = false;
                if (!_serviceCode.IsEqual(value))
                    changed = true;
                _serviceCode = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ServiceCode));
                }
            }
        }
        public string ServiceName
        {
            get
            {
                return _serviceName;
            }
            set
            {
                bool changed = false;
                if (!_serviceName.IsEqual(value))
                    changed = true;
                _serviceName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ServiceName));
                }
            }
        }
        public int ServiceCatNum
        {
            get
            {
                return _serviceCatNum;
            }
            set
            {
                bool changed = false;
                if (!_serviceCatNum.IsEqual(value))
                    changed = true;
                _serviceCatNum = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ServiceCatNum));
                }
            }
        }
        public string LicencePlate
        {
            get
            {
                return _licencePlate;
            }
            set
            {
                bool changed = false;
                if (!_licencePlate.IsEqual(value))
                    changed = true;
                _licencePlate = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(LicencePlate));
                }
            }
        }
        public int DriverId
        {
            get
            {
                return _driverId;
            }
            set
            {
                bool changed = false;
                if (!_driverId.IsEqual(value))
                    changed = true;
                _driverId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(DriverId));
                }
            }
        }
        public double Capacity
        {
            get
            {
                return _capacity;
            }
            set
            {
                bool changed = false;
                if (!_capacity.IsEqual(value))
                    changed = true;
                _capacity = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Capacity));
                }
            }
        }
        public string Gravity
        {
            get => _gravity; set
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
        #endregion
        
        public double Returned
        {
            get
            {
                return _returned;
            }
            set
            {
                bool changed = false;
                if (!_returned.IsEqual(value))
                    changed = true;
                _returned = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Returned));
                    OnPropertyChanged(nameof(Recycled));
                }
            }
        }
        public double Transfer
        {
            get
            {
                return _transfer;
            }
            set
            {
                bool changed = false;
                if (!_transfer.IsEqual(value))
                    changed = true;
                _transfer = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Transfer));
                    OnPropertyChanged(nameof(Recycled));
                }
            }
        }
        public double Recycled { get => Returned + Transfer; }
        public ServiceCategoryDTO ServiceCategory
        {
            get => _serviceCategory;
            set
            {
                bool changed = false;
                if (!_serviceCategory.IsEqual(value))
                    changed = true;
                _serviceCategory = value;
                value.Do(o => ServiceCatNum = o.CatNum, () => ServiceCatNum = -1);
                if (changed)
                {
                    OnPropertyChanged(nameof(ServiceCategory));
                }
            }
        }
        public Driver Driver
        {
            get => _driver; set
            {
                bool changed = false;
                if (!_driver.IsEqual(value))
                    changed = true;
                _driver = value;
                value.Do(o => DriverId = o.DriverId, () => DriverId = -1);
                if (changed)
                {
                    OnPropertyChanged(nameof(Driver));
                }
            }
        }

        public override string ToString()
        {
            return ServiceName;
        }
    }
}
