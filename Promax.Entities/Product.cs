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
    public class Product : INotifyPropertyChanged
    {
        private List<BatchedStock> _batchedList = new List<BatchedStock>();
        private Site _site;
        private Order _order;
        private Recipe _recipe;
        private ServiceCategory _serviceCategory;
        private Service _service;
        private Driver _driver;
        private User _user;
        private Client _client;
        private string _isBill="false";
        private string _desk="false";
        private string _dep="false";
        #region DTO
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            ArrangePropertyValues();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        #region FieldsByAutoPropCreator
        private int _productId=-1;
        private DateTime _productDate;
        private DateTime _productTime;
        private int _rtmNumber;
        private int _billNumber;
        private int _orderId;
        private int _clientId;
        private int _siteId;
        private int _recipeId;
        private int _serviceCatNum;
        private int _mixerServiceId;
        private int _pumpServiceId;
        private int _selfServiceId;
        private int _mixerDriverId;
        private int _pumpDriverId;
        private int _selfDriverId;
        private int _userId;
        private double _target;
        private double _addon;
        private double _realTarget;
        private double _produced;
        private double _shipped;
        private double _returned;
        private double _transfer;
        private double _recycled;
        private double _delivered;
        private double _capacity;
        private double _ubm;
        private int _aimBatch;
        private int _rtmBatch;
        private int _gateNum;
        private double _addWater;
        private DateTime _depTime;
        private int _pos;
        private int _despatchStatus;
        private DateTime _orderDate;
        private double _orderQuantity;

        private string _despatchNumber=string.Empty;
        private string _despatchGuid=string.Empty;
        private string _ebisNumber=string.Empty;
        private string _despatchTag=string.Empty;
        private string _orderNumber=string.Empty;
        private string _rtmFactor=string.Empty;
        private string _aimFactor=string.Empty;
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
        public int SelfServiceId
        {
            get
            {
                return _selfServiceId;
            }
            set
            {
                bool changed = false;
                if (!_selfServiceId.IsEqual(value))
                    changed = true;
                _selfServiceId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SelfServiceId));
                }
            }
        }
        public int MixerDriverId
        {
            get
            {
                return _mixerDriverId;
            }
            set
            {
                bool changed = false;
                if (!_mixerDriverId.IsEqual(value))
                    changed = true;
                _mixerDriverId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(MixerDriverId));
                }
            }
        }
        public int PumpDriverId
        {
            get
            {
                return _pumpDriverId;
            }
            set
            {
                bool changed = false;
                if (!_pumpDriverId.IsEqual(value))
                    changed = true;
                _pumpDriverId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(PumpDriverId));
                }
            }
        }
        public int SelfDriverId
        {
            get
            {
                return _selfDriverId;
            }
            set
            {
                bool changed = false;
                if (!_selfDriverId.IsEqual(value))
                    changed = true;
                _selfDriverId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SelfDriverId));
                }
            }
        }
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                bool changed = false;
                if (!_userId.IsEqual(value))
                    changed = true;
                _userId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }
        /// <summary>
        /// Toplam miktar.
        /// </summary>
        public double Target
        {
            get
            {
                return _target;
            }
            set
            {
                bool changed = false;
                if (!_target.IsEqual(value))
                    changed = true;
                _target = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Target));
                }
            }
        }
        /// <summary>
        /// Hazır miktar.
        /// </summary>
        public double Addon
        {
            get
            {
                return _addon;
            }
            set
            {
                bool changed = false;
                if (!_addon.IsEqual(value))
                    changed = true;
                _addon = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Addon));
                }
            }
        }
        /// <summary>
        /// Üretilecek miktar.
        /// </summary>
        public double RealTarget
        {
            get
            {
                return _realTarget;
            }
            set
            {
                bool changed = false;
                if (!_realTarget.IsEqual(value))
                    changed = true;
                _realTarget = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(RealTarget));
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
                }
            }
        }
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
                }
            }
        }
        public double Recycled
        {
            get
            {
                return _recycled;
            }
            set
            {
                bool changed = false;
                if (!_recycled.IsEqual(value))
                    changed = true;
                _recycled = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Recycled));
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
        public double Ubm
        {
            get
            {
                return _ubm;
            }
            set
            {
                bool changed = false;
                if (!_ubm.IsEqual(value))
                    changed = true;
                _ubm = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Ubm));
                }
            }
        }
        public int AimBatch
        {
            get
            {
                return _aimBatch;
            }
            set
            {
                bool changed = false;
                if (!_aimBatch.IsEqual(value))
                    changed = true;
                _aimBatch = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(AimBatch));
                }
            }
        }
        public string AimFactor
        {
            get
            {
                return _aimFactor;
            }
            set
            {
                bool changed = false;
                if (!_aimFactor.IsEqual(value))
                    changed = true;
                _aimFactor = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(AimFactor));
                }
            }
        }
        public int RtmBatch
        {
            get
            {
                return _rtmBatch;
            }
            set
            {
                bool changed = false;
                if (!_rtmBatch.IsEqual(value))
                    changed = true;
                _rtmBatch = value;
                Produced = RtmBatch != AimBatch ? RtmBatch * Ubm : RealTarget;
                if (changed)
                {
                    OnPropertyChanged(nameof(RtmBatch));
                }
            }
        }
        public string RtmFactor
        {
            get
            {
                return _rtmFactor;
            }
            set
            {
                bool changed = false;
                if (!_rtmFactor.IsEqual(value))
                    changed = true;
                _rtmFactor = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(RtmFactor));
                }
            }
        }
        public int GateNum
        {
            get
            {
                return _gateNum;
            }
            set
            {
                bool changed = false;
                if (!_gateNum.IsEqual(value))
                    changed = true;
                _gateNum = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(GateNum));
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
        public int Pos
        {
            get
            {
                return _pos;
            }
            set
            {
                bool changed = false;
                if (!_pos.IsEqual(value))
                    changed = true;
                _pos = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Pos));
                    OnPropertyChanged(nameof(PosStatus));
                }
            }
        }
        public string DespatchNumber
        {
            get
            {
                return _despatchNumber;
            }
            set
            {
                bool changed = false;
                if (!_despatchNumber.IsEqual(value))
                    changed = true;
                _despatchNumber = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(DespatchNumber));
                }
            }
        }
        public string DespatchGuid
        {
            get
            {
                return _despatchGuid;
            }
            set
            {
                bool changed = false;
                if (!_despatchGuid.IsEqual(value))
                    changed = true;
                _despatchGuid = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(DespatchGuid));
                }
            }
        }
        public string EbisNumber
        {
            get
            {
                return _ebisNumber;
            }
            set
            {
                bool changed = false;
                if (!_ebisNumber.IsEqual(value))
                    changed = true;
                _ebisNumber = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(EbisNumber));
                }
            }
        }
        public string DespatchTag
        {
            get
            {
                return _despatchTag;
            }
            set
            {
                bool changed = false;
                if (!_despatchTag.IsEqual(value))
                    changed = true;
                _despatchTag = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(DespatchTag));
                }
            }
        }
        public int DespatchStatus
        {
            get
            {
                return _despatchStatus;
            }
            set
            {
                bool changed = false;
                if (!_despatchStatus.IsEqual(value))
                    changed = true;
                _despatchStatus = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(DespatchStatus));
                }
            }
        }
        public double OrderQuantity
        {
            get
            {
                return _orderQuantity;
            }
            set
            {
                bool changed = false;
                if (!_orderQuantity.IsEqual(value))
                    changed = true;
                _orderQuantity = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(OrderQuantity));
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
        #endregion
        /// <summary>
        /// Üretim durumu
        /// </summary>
        public ProductionStatus PosStatus { get => (ProductionStatus)Pos; set => Pos = (int)value; }
        /// <summary>
        /// Belge no üret.
        /// </summary>
        public string IsBill
        {
            get => _isBill; set
            {
                bool changed = false;
                if (!_isBill.IsEqual(value))
                    changed = true;
                _isBill = value.Boolify();
                if (changed)
                {
                    OnPropertyChanged(nameof(IsBill));
                }
            }
        }
        public string Desk
        {
            get => _desk; set
            {
                bool changed = false;
                if (!_desk.IsEqual(value))
                    changed = true;
                _desk = value.Boolify();
                if (changed)
                {
                    OnPropertyChanged(nameof(Desk));
                }
            }
        }
        public string Dep
        {
            get => _dep; set
            {
                bool changed = false;
                if (!_dep.IsEqual(value))
                    changed = true;
                _dep = value.Boolify();
                if (changed)
                {
                    OnPropertyChanged(nameof(Dep));
                }
            }
        }
        #endregion
        public IEnumerable<BatchedStock> BatchedStocks => _batchedList;
        public Client Client
        {
            get => _client; set
            {
                bool changed = false;
                if (!_client.IsEqual(value))
                    changed = true; _client = value;
                value.Do(o => ClientId = o.ClientId, () => ClientId = 0);
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
                value.Do(o => SiteId = o.SiteId, () => SiteId = 0);
                if (changed)
                {
                    OnPropertyChanged(nameof(Site));
                }
            }
        }
        public Order Order
        {
            get => _order; set
            {
                bool changed = false;
                if (!_order.IsEqual(value))
                    changed = true;
                _order = value;
                value.Do(o => OrderId = o.OrderId, () => OrderId = 0);
                if (changed)
                {
                    OnPropertyChanged(nameof(Order));
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
                value.Do(o => RecipeId = o.RecipeId, () => RecipeId = 0);
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
                value.Do(o => ServiceCatNum = o.CatNum, () => ServiceCatNum = 0);
                if (changed)
                {
                    OnPropertyChanged(nameof(ServiceCategory));
                }
            }
        }
        public Service Service
        {
            get => _service; set
            {
                bool changed = false;
                if (!_service.IsEqual(value))
                    changed = true;
                _service = value;
                _mixerServiceId = 0;
                _pumpServiceId = 0;
                _selfServiceId = 0;
                MixerService = null;
                PumpService = null;
                SelfService = null;
                if (ServiceCatNum == 1)
                    value.DoReturn(o => { MixerServiceId = o.ServiceId; Addon = o.Recycled; }, () => { MixerServiceId = 0; Addon = 0; }).Do(x => MixerService = x);
                else if (ServiceCatNum == 2)
                    value.DoReturn(o => PumpServiceId = o.ServiceId, () => PumpServiceId = 0).Do(x => PumpService = x);
                else if (ServiceCatNum == 3)
                    value.DoReturn(o => SelfServiceId = o.ServiceId, () => SelfServiceId = 0).Do(x => SelfService = x);
                if (changed)
                {
                    OnPropertyChanged(nameof(Service));
                    OnPropertyChanged(nameof(SelfService));
                    OnPropertyChanged(nameof(MixerService));
                    OnPropertyChanged(nameof(PumpService));
                }
            }
        }
        public Service SelfService { get; private set; }
        public Service MixerService { get; private set; }
        public Service PumpService { get; private set; }
        public int ServiceId
        {
            get
            {
                int result = -1;
                ServiceCatNum.DoIfElse(x => x == 1, x => result = MixerServiceId, x => x.DoIfElse(y => y == 2, y => result = PumpServiceId, y => y.DoIf(z => z == 3, z => result = SelfServiceId)));
                return result;
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
                _mixerDriverId = 0;
                _pumpDriverId = 0;
                _selfDriverId = 0;
                if (ServiceCatNum == 1)
                    value.Do(o => MixerDriverId = o.DriverId, () => MixerDriverId = 0);
                else if (ServiceCatNum == 2)
                    value.Do(o => PumpDriverId = o.DriverId, () => PumpDriverId = 0);
                else if (ServiceCatNum == 3)
                    value.Do(o => SelfDriverId = o.DriverId, () => SelfDriverId = 0);
                if (changed)
                {
                    OnPropertyChanged(nameof(Driver));
                }
            }
        }
        public int DriverId
        {
            get
            {
                int result = -1;
                ServiceCatNum.DoIfElse(x => x == 1, x => result = MixerDriverId, x => x.DoIfElse(y => y == 2, y => result = PumpDriverId, y => y.DoIf(z => z == 3, z => result = SelfDriverId)));
                return result;
            }
        }
        public User User
        {
            get => _user; set
            {
                bool changed = false;
                if (!_user.IsEqual(value))
                    changed = true;
                _user = value;
                value.Do(o => UserId = o.UserId, () => UserId = 0);
                if (changed)
                {
                    OnPropertyChanged(nameof(User));
                }
            }
        }
        public void AddBatchedStock(BatchedStock batchedStock)
        {
            if (_batchedList.Contains(batchedStock))
                return;
            _batchedList.Add(batchedStock);
            batchedStock.Product = this;
        }
        public void RemoveBatchedStock(BatchedStock batchedStock)
        {
            if (!_batchedList.Contains(batchedStock))
                return;
            _batchedList.Remove(batchedStock);
        }
        public void ClearBatchedStocks()
        {
            _batchedList.Clear();
        }
        private void ArrangePropertyValues()
        {
            this.Do(x =>
            {
                x.RealTarget = x.Target - x.Addon;
                x.Shipped = x.Produced + x.Addon;
                x.Recycled = x.Returned + x.Transfer;
                x.Delivered = x.Shipped - x.Returned;

                x.DoIfElse(z => z.Capacity > 0, z => z.AimBatch = (int)Math.Ceiling(z.RealTarget / z.Capacity), z => z.AimBatch = 0);
                x.DoIfElse(z => z.AimBatch > 0, z => z.Ubm = z.RealTarget / ((double)z.AimBatch), z => z.Ubm = 0);
                AimFactor = AimBatch.ToString() + "*" + Ubm.ToString();
            });
        }
    }
}
