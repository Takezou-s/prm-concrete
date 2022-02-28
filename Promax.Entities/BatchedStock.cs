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
    public class BatchedStock : ICloneable, INotifyPropertyChanged
    {
        private Stock _stock;
        private Silo _silo;
        private Product _product;
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region FieldsByAutoPropCreator
        private int _batchedId;
        private int _productId;
        private int _batchNo;
        private int _stockId;
        private int _siloId;
        private int _userId;
        private DateTime _batchedDate;
        private DateTime _batchedTime;
        private double _addVal;
        private double _design;
        private double _batched;
        private UserDTO _user;
        #endregion
        #region PropertiesByAutoPropCreator
        public int BatchedId
        {
            get
            {
                return _batchedId;
            }
            set
            {
                bool changed = false;
                if (!_batchedId.IsEqual(value))
                    changed = true;
                _batchedId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(BatchedId));
                }
            }
        }
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
        public int BatchNo
        {
            get
            {
                return _batchNo;
            }
            set
            {
                bool changed = false;
                if (!_batchNo.IsEqual(value))
                    changed = true;
                _batchNo = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(BatchNo));
                }
            }
        }
        public int StockId
        {
            get
            {
                return _stockId;
            }
            set
            {
                bool changed = false;
                if (!_stockId.IsEqual(value))
                    changed = true;
                _stockId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(StockId));
                }
            }
        }
        public int SiloId
        {
            get
            {
                return _siloId;
            }
            set
            {
                bool changed = false;
                if (!_siloId.IsEqual(value))
                    changed = true;
                _siloId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SiloId));
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
        public DateTime BatchedDate
        {
            get
            {
                return _batchedDate;
            }
            set
            {
                bool changed = false;
                if (!_batchedDate.IsEqual(value))
                    changed = true;
                _batchedDate = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(BatchedDate));
                }
            }
        }
        public DateTime BatchedTime
        {
            get
            {
                return _batchedTime;
            }
            set
            {
                bool changed = false;
                if (!_batchedTime.IsEqual(value))
                    changed = true;
                _batchedTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(BatchedTime));
                }
            }
        }
        public double AddVal
        {
            get
            {
                return _addVal;
            }
            set
            {
                bool changed = false;
                if (!_addVal.IsEqual(value))
                    changed = true;
                _addVal = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(AddVal));
                }
            }
        }
        public double Design
        {
            get
            {
                return _design;
            }
            set
            {
                bool changed = false;
                if (!_design.IsEqual(value))
                    changed = true;
                _design = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Design));
                }
            }
        }
        public double Batched
        {
            get
            {
                return _batched;
            }
            set
            {
                bool changed = false;
                if (!_batched.IsEqual(value))
                    changed = true;
                _batched = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Batched));
                }
            }
        }
        #endregion


        public Stock Stock
        {
            get => _stock; set
            {
                bool changed = false;
                _stock.DoIf(x => !x.IsEqual(value), x => changed = true);
                _stock = value;
                value.Do(o => StockId = o.StockId, () => StockId = -1);
                changed.DoIf(x => x, x => OnPropertyChanged(nameof(Stock)));
            }
        }
        public Silo Silo
        {
            get => _silo; set
            {
                bool changed = false;
                _silo.DoIf(x => !x.IsEqual(value), x => changed = true);
                _silo = value;
                value.Do(o => SiloId = o.SiloId, () => SiloId = -1);
                changed.DoIf(x => x, x => OnPropertyChanged(nameof(Silo)));
            }
        }
        public Product Product
        {
            get => _product;
            set
            {
                bool changed = false;
                _product.DoIf(x => !x.IsEqual(value), x => changed = true);
                _product = value;
                value.Do(o => ProductId = o.ProductId, () => ProductId = -1);
                changed.DoIf(x => x, x => OnPropertyChanged(nameof(Product)));
            }
        }
        public UserDTO User
        {
            get => _user;
            set
            {
                bool changed = false;
                _user.DoIf(x => !x.IsEqual(value), x => changed = true);
                _user = value;
                value.Do(o => UserId = o.UserId, () => UserId = -1);
                this.DoIf(x => !x._product.IsEqual(value), x => x.OnPropertyChanged(nameof(User)));
                changed.DoIf(x => x, x => OnPropertyChanged(nameof(User)));
            }
        }

        public object Clone()
        {
            return new BatchedStock()
            {
                BatchedId = this.BatchedId,
                ProductId = this.ProductId,
                BatchNo = this.BatchNo,
                StockId = this.StockId,
                SiloId = this.SiloId,
                UserId = this.UserId,
                BatchedDate = this.BatchedDate,
                BatchedTime = this.BatchedTime,
                AddVal = this.AddVal,
                Design = this.Design,
                Batched = this.Batched,
                Stock = this.Stock,
                Silo = this.Silo,
                Product = this.Product
            };
        }
    }
}
