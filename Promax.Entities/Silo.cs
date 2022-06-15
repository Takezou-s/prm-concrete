using Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Promax.Entities
{
    public class Silo : INotifyPropertyChanged
    {
        private string _isStock = "false";
        private string _isActive = "false";
        private string _enabled = "false";
        private Stock stock;
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region FieldsByAutoPropCreator
        private int _siloId;
        private int _wegId;
        private string _siloName;
        private int _siloNo;
        private int _stockId;
        private double _capacity;
        private int _scale;
        private double _fastVal;
        private int _vibOn;
        private int _vibOff;
        private double _vibFl;
        private int _swingOn;
        private int _swingOff;
        private double _swingVal;
        private double _tolVal;
        private double _shotVal;
        private double _manNem;
        private int _nemId;
        private double _minDebi;
        private double _temp;
        private double _balance;
        #endregion
        #region PropertiesByAutoPropCreator
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
        public int WegId
        {
            get
            {
                return _wegId;
            }
            set
            {
                bool changed = false;
                if (!_wegId.IsEqual(value))
                    changed = true;
                _wegId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(WegId));
                    OnPropertyChanged(nameof(UniqueName));
                }
            }
        }
        public string SiloName
        {
            get
            {
                return _siloName;
            }
            set
            {
                bool changed = false;
                if (!_siloName.IsEqual(value))
                    changed = true;
                _siloName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SiloName));
                    OnPropertyChanged(nameof(UniqueName));
                    OnPropertyChanged(nameof(StockCatNum));
                }
            }
        }
        public int SiloNo
        {
            get
            {
                return _siloNo;
            }
            set
            {
                bool changed = false;
                if (!_siloNo.IsEqual(value))
                    changed = true;
                _siloNo = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SiloNo));
                    OnPropertyChanged(nameof(UniqueName));
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
        public int Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                bool changed = false;
                if (!_scale.IsEqual(value))
                    changed = true;
                _scale = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Scale));
                }
            }
        }
        public double FastVal
        {
            get
            {
                return _fastVal;
            }
            set
            {
                bool changed = false;
                if (!_fastVal.IsEqual(value))
                    changed = true;
                _fastVal = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(FastVal));
                }
            }
        }
        public int VibOn
        {
            get
            {
                return _vibOn;
            }
            set
            {
                bool changed = false;
                if (!_vibOn.IsEqual(value))
                    changed = true;
                _vibOn = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(VibOn));
                }
            }
        }
        public int VibOff
        {
            get
            {
                return _vibOff;
            }
            set
            {
                bool changed = false;
                if (!_vibOff.IsEqual(value))
                    changed = true;
                _vibOff = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(VibOff));
                }
            }
        }
        public double VibFl
        {
            get
            {
                return _vibFl;
            }
            set
            {
                bool changed = false;
                if (!_vibFl.IsEqual(value))
                    changed = true;
                _vibFl = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(VibFl));
                }
            }
        }
        public int SwingOn
        {
            get
            {
                return _swingOn;
            }
            set
            {
                bool changed = false;
                if (!_swingOn.IsEqual(value))
                    changed = true;
                _swingOn = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SwingOn));
                }
            }
        }
        public int SwingOff
        {
            get
            {
                return _swingOff;
            }
            set
            {
                bool changed = false;
                if (!_swingOff.IsEqual(value))
                    changed = true;
                _swingOff = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SwingOff));
                }
            }
        }
        public double SwingVal
        {
            get
            {
                return _swingVal;
            }
            set
            {
                bool changed = false;
                if (!_swingVal.IsEqual(value))
                    changed = true;
                _swingVal = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SwingVal));
                }
            }
        }
        public double TolVal
        {
            get
            {
                return _tolVal;
            }
            set
            {
                bool changed = false;
                if (!_tolVal.IsEqual(value))
                    changed = true;
                _tolVal = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(TolVal));
                }
            }
        }
        public double ShotVal
        {
            get
            {
                return _shotVal;
            }
            set
            {
                bool changed = false;
                if (!_shotVal.IsEqual(value))
                    changed = true;
                _shotVal = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ShotVal));
                }
            }
        }
        public double ManNem
        {
            get
            {
                return _manNem;
            }
            set
            {
                bool changed = false;
                if (!_manNem.IsEqual(value))
                    changed = true;
                _manNem = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ManNem));
                }
            }
        }
        public int NemId
        {
            get
            {
                return _nemId;
            }
            set
            {
                bool changed = false;
                if (!_nemId.IsEqual(value))
                    changed = true;
                _nemId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(NemId));
                }
            }
        }
        public double MinDebi
        {
            get
            {
                return _minDebi;
            }
            set
            {
                bool changed = false;
                if (!_minDebi.IsEqual(value))
                    changed = true;
                _minDebi = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(MinDebi));
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

        public ObservableCollection<Stock> AvailableStocks { get; } = new ObservableCollection<Stock>();
        public string UniqueName { get => SiloName +WegId + SiloNo; }
        public int StockCatNum
        {
            get
            {
                int result = 0;
                if (SiloName.Equals("AGG"))
                {
                    result = 1;
                }
                else if (SiloName.Equals("CEM"))
                {
                    result = 2;
                }
                else if (SiloName.Equals("WTR"))
                {
                    result = 3;
                }
                else if (SiloName.Equals("ADV"))
                {
                    result = 4;
                }
                return result;
            }
        }
        public string IsStock
        {
            get => _isStock; set
            {
                bool changed = false;
                if (!_isStock.IsEqual(value.Boolify()))
                    changed = true;
                _isStock = value.Boolify();
                if (changed)
                {
                    OnPropertyChanged(nameof(IsStock));
                }
            }
        }
        public string IsActive
        {
            get => _isActive; set
            {
                bool changed = false;
                if (!_isActive.IsEqual(value.Boolify()))
                    changed = true;
                _isActive = value.Boolify();
                if (changed)
                {
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }
        public string Enabled
        {
            get => _enabled; set
            {
                bool changed = false;
                if (!_enabled.IsEqual(value.Boolify()))
                    changed = true;
                _enabled = value.Boolify();
                if (changed)
                {
                    OnPropertyChanged(nameof(Enabled));
                }
            }
        }

        public Stock Stock
        {
            get => stock; set
            {
                bool changed = false;
                if (!stock.IsEqual(value))
                    changed = true;
                stock = value;
                value.Do(o => StockId = o.StockId, () => StockId = -1);
                if (changed)
                {
                    OnPropertyChanged(nameof(Stock));
                }
            }
        }
        public double Batched { get; set; }
    }
}