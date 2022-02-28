using Extensions;
using Promax.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;

namespace Promax.Entities
{
    public class Stock : INotifyPropertyChanged
    {
        private MyList<Silo> _siloList = new MyList<Silo>(nameof(Silo.SiloId));

        public IEnumerable<Silo> Silos => _siloList;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region FieldsByAutoPropCreator
        private int _stockId = -1;
        private string _stockCode;
        private string _stockName;
        private int _stockCatNum;
        private double _temp;
        private double _balance;
        #endregion
        #region PropertiesByAutoPropCreator
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
        public string StockCode
        {
            get
            {
                return _stockCode;
            }
            set
            {
                bool changed = false;
                if (!_stockCode.IsEqual(value))
                    changed = true;
                _stockCode = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(StockCode));
                }
            }
        }
        public string StockName
        {
            get
            {
                return _stockName;
            }
            set
            {
                bool changed = false;
                if (!_stockName.IsEqual(value))
                    changed = true;
                _stockName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(StockName));
                }
            }
        }
        public int StockCatNum
        {
            get
            {
                return _stockCatNum;
            }
            set
            {
                bool changed = false;
                if (!_stockCatNum.IsEqual(value))
                    changed = true;
                _stockCatNum = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(StockCatNum));
                }
            }
        }
        public double Temp
        {
            get
            {
                return _temp;
            }
            set
            {
                bool changed = false;
                if (!_temp.IsEqual(value))
                    changed = true;
                _temp = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Temp));
                }
            }
        }
        public double Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                bool changed = false;
                if (!_balance.IsEqual(value))
                    changed = true;
                _balance = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Balance));
                }
            }
        }
        #endregion

        public StockType StockType
        {
            get => (StockType)StockCatNum; set
            {
                bool changed = false;
                if (!StockType.IsEqual(value))
                    changed = true;
                StockCatNum = (int)value;
                if (changed)
                {
                    OnPropertyChanged(nameof(StockType));
                }
            }
        }

        #region Add, Update, Remove, Clear
        public void AddSilo(Silo silo)
        {
            if (silo != null && !_siloList.Contains(silo))
            {
                _siloList.Add(silo);
                silo.Stock = this;
                OnPropertyChanged(nameof(Silos));
            }
        }
        public void RemoveSilo(Silo silo)
        {
            if (silo != null && _siloList.Contains(silo))
            {
                _siloList.Remove(silo);
                silo.Do(x => x.Stock = null);
                OnPropertyChanged(nameof(Silos));
            }
        }
        public void ClearSilos()
        {
            _siloList.Clear();
            OnPropertyChanged(nameof(Silos));
        }
        #endregion
    }
}