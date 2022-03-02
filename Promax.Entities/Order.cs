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
    public class Order : INotifyPropertyChanged
    {
        private Client _client;
        private Site _site;
        private Recipe _recipe;
        private ServiceCategory _serviceCategory;
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        #region FieldsByAutoPropCreator
        private int _orderId = -1;
        private DateTime _orderDate;
        private DateTime _orderTime;
        private int _orderStatus;
        private int _serviceCatNum;
        private int _clientId;
        private int _siteId;
        private int _recipeId;
        private double _addWater;
        private int _pumpServiceId;
        private double _quantity;
        private double _produced;
        private double _remaining;
        private string _orderNumber;
        #endregion
        #region PropertiesByAutoPropCreator
        public int OrderId
        {
            get
            {
                return _orderId;
            }
            set
            {
                bool changed = false;
                if (!_orderId.IsEqual(value))
                    changed = true;
                _orderId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(OrderId));
                }
            }
        }
        public DateTime OrderDate
        {
            get
            {
                return _orderDate;
            }
            set
            {
                bool changed = false;
                if (!_orderDate.IsEqual(value))
                    changed = true;
                _orderDate = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(OrderDate));
                }
            }
        }
        public DateTime OrderTime
        {
            get
            {
                return _orderTime;
            }
            set
            {
                bool changed = false;
                if (!_orderTime.IsEqual(value))
                    changed = true;
                _orderTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(OrderTime));
                }
            }
        }
        public int OrderStatus
        {
            get
            {
                return _orderStatus;
            }
            set
            {
                bool changed = false;
                if (!_orderStatus.IsEqual(value))
                    changed = true;
                _orderStatus = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(OrderStatus));
                    OnPropertyChanged(nameof(OrderStatusEnum));
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
        public int RecipeId
        {
            get
            {
                return _recipeId;
            }
            set
            {
                bool changed = false;
                if (!_recipeId.IsEqual(value))
                    changed = true;
                _recipeId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(RecipeId));
                }
            }
        }
        public double AddWater
        {
            get
            {
                return _addWater;
            }
            set
            {
                bool changed = false;
                if (!_addWater.IsEqual(value))
                    changed = true;
                _addWater = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(AddWater));
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
        public double Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                bool changed = false;
                if (!_quantity.IsEqual(value))
                    changed = true;
                _quantity = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }
        public double Produced
        {
            get
            {
                return _produced;
            }
            set
            {
                bool changed = false;
                if (!_produced.IsEqual(value))
                    changed = true;
                _produced = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Produced));
                }
            }
        }
        public double Remaining
        {
            get
            {
                return _remaining;
            }
            set
            {
                bool changed = false;
                if (!_remaining.IsEqual(value))
                    changed = true;
                _remaining = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Remaining));
                }
            }
        }
        public string OrderNumber
        {
            get
            {
                return _orderNumber;
            }
            set
            {
                bool changed = false;
                if (!_orderNumber.IsEqual(value))
                    changed = true;
                _orderNumber = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(OrderNumber));
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
        public Site Site
        {
            get => _site; set
            {
                bool changed = false;
                if (!_site.IsEqual(value))
                    changed = true;
                _site = value;
                value.Do(o => SiteId = o.SiteId, () => SiteId = -1);
                if (changed)
                {
                    OnPropertyChanged(nameof(Site));
                }
            }
        }
        public Recipe Recipe
        {
            get => _recipe; set
            {
                bool changed = false;
                if (!_recipe.IsEqual(value))
                    changed = true;
                _recipe = value;
                value.Do(o => RecipeId = o.RecipeId, () =>RecipeId= -1);
                if (changed)
                {
                    OnPropertyChanged(nameof(Recipe));
                }
            }
        }
        public ServiceCategory ServiceCategory
        {
            get => _serviceCategory; set
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
        public OrderStatus OrderStatusEnum { get => (OrderStatus)OrderStatus; set => OrderStatus = (int)value; }
    }
}
