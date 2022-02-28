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
    public class ConsumedStock : ICloneable, INotifyPropertyChanged
    {
        private Stock _stock;
        private Silo _silo;
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        #region FieldsByAutoPropCreator
        private int _consumedId;
        private int _stockId;
        private int _siloId;
        private int _userId;
        private DateTime _consumedDate;
        private DateTime _consumedTime;
        private double _consumed;
        #endregion
        #region PropertiesByAutoPropCreator
        public int ConsumedId
        {
            get
            {
                return _consumedId;
            }
            set
            {
                bool changed = false;
                if (!_consumedId.IsEqual(value))
                    changed = true;
                _consumedId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ConsumedId));
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
        public DateTime ConsumedDate
        {
            get
            {
                return _consumedDate;
            }
            set
            {
                bool changed = false;
                if (!_consumedDate.IsEqual(value))
                    changed = true;
                _consumedDate = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ConsumedDate));
                }
            }
        }
        public DateTime ConsumedTime
        {
            get
            {
                return _consumedTime;
            }
            set
            {
                bool changed = false;
                if (!_consumedTime.IsEqual(value))
                    changed = true;
                _consumedTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ConsumedTime));
                }
            }
        }
        public double Consumed
        {
            get
            {
                return _consumed;
            }
            set
            {
                bool changed = false;
                if (!_consumed.IsEqual(value))
                    changed = true;
                _consumed = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Consumed));
                }
            }
        }
        #endregion

        public Stock Stock
        {
            get => _stock; set
            {
                bool changed = false;
                if (!_stock.IsEqual(value))
                    changed = true;
                _stock = value;
                value.Do(o => StockId = o.StockId, () => StockId = -1);
                if (changed)
                {
                    OnPropertyChanged(nameof(Stock));
                }
            }
        }
        public Silo Silo
        {
            get => _silo; set
            {
                bool changed = false;
                if (!_silo.IsEqual(value))
                    changed = true;
                _silo = value;
                value.Do(o => SiloId = o.SiloId, () => SiloId = -1);
                if (changed)
                {
                    OnPropertyChanged(nameof(Silo));
                }
            }
        }

        public object Clone()
        {
            return new ConsumedStock()
            {
                ConsumedId = this.ConsumedId,
                StockId = this.StockId,
                SiloId = this.SiloId,
                UserId = this.UserId,
                ConsumedDate = this.ConsumedDate,
                ConsumedTime = this.ConsumedTime,
                Consumed = this.Consumed,
                Stock = this.Stock,
                Silo = this.Silo
            };
        }
    }
}
