using Extensions;
using System.ComponentModel;

namespace Promax.Entities
{
    public class SistemAyarları : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region FieldsByAutoPropCreator
        private bool _tartıBandı1_Var;
        private bool _tartıBandı2_Var;
        private bool _çimentoBunkeri1_Var;
        private bool _çimentoBunkeri2_Var;
        private bool _suBunkeri1_Var;
        private bool _suBunkeri2_Var;
        private bool _katkıBunkeri1_Var;
        private bool _katkıBunkeri2_Var;
        private bool _mikser1_Var;
        private bool _mikser2_Var;
        private int _tartıBandı1_SiloSayısı;
        private int _tartıBandı2_SiloSayısı;
        private int _çimentoBunkeri1_SiloSayısı;
        private int _çimentoBunkeri2_SiloSayısı;
        private int _suBunkeri1_SiloSayısı;
        private int _suBunkeri2_SiloSayısı;
        private int _katkıBunkeri1_SiloSayısı;
        private int _katkıBunkeri2_SiloSayısı;
        private int _mikser1_KapakSayısı;
        private int _mikser2_KapakSayısı;
        #endregion
        #region PropertiesByAutoPropCreator
        public bool TartıBandı1_Var
        {
            get
            {
                return _tartıBandı1_Var;
            }
            set
            {
                bool changed = false;
                if (!_tartıBandı1_Var.IsEqual(value))
                    changed = true;
                _tartıBandı1_Var = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(TartıBandı1_Var));
                }
            }
        }
        public bool TartıBandı2_Var
        {
            get
            {
                return _tartıBandı2_Var;
            }
            set
            {
                bool changed = false;
                if (!_tartıBandı2_Var.IsEqual(value))
                    changed = true;
                _tartıBandı2_Var = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(TartıBandı2_Var));
                }
            }
        }
        public bool ÇimentoBunkeri1_Var
        {
            get
            {
                return _çimentoBunkeri1_Var;
            }
            set
            {
                bool changed = false;
                if (!_çimentoBunkeri1_Var.IsEqual(value))
                    changed = true;
                _çimentoBunkeri1_Var = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ÇimentoBunkeri1_Var));
                }
            }
        }
        public bool ÇimentoBunkeri2_Var
        {
            get
            {
                return _çimentoBunkeri2_Var;
            }
            set
            {
                bool changed = false;
                if (!_çimentoBunkeri2_Var.IsEqual(value))
                    changed = true;
                _çimentoBunkeri2_Var = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ÇimentoBunkeri2_Var));
                }
            }
        }
        public bool SuBunkeri1_Var
        {
            get
            {
                return _suBunkeri1_Var;
            }
            set
            {
                bool changed = false;
                if (!_suBunkeri1_Var.IsEqual(value))
                    changed = true;
                _suBunkeri1_Var = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SuBunkeri1_Var));
                }
            }
        }
        public bool SuBunkeri2_Var
        {
            get
            {
                return _suBunkeri2_Var;
            }
            set
            {
                bool changed = false;
                if (!_suBunkeri2_Var.IsEqual(value))
                    changed = true;
                _suBunkeri2_Var = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SuBunkeri2_Var));
                }
            }
        }
        public bool KatkıBunkeri1_Var
        {
            get
            {
                return _katkıBunkeri1_Var;
            }
            set
            {
                bool changed = false;
                if (!_katkıBunkeri1_Var.IsEqual(value))
                    changed = true;
                _katkıBunkeri1_Var = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(KatkıBunkeri1_Var));
                }
            }
        }
        public bool KatkıBunkeri2_Var
        {
            get
            {
                return _katkıBunkeri2_Var;
            }
            set
            {
                bool changed = false;
                if (!_katkıBunkeri2_Var.IsEqual(value))
                    changed = true;
                _katkıBunkeri2_Var = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(KatkıBunkeri2_Var));
                }
            }
        }
        public bool Mikser1_Var
        {
            get
            {
                return _mikser1_Var;
            }
            set
            {
                bool changed = false;
                if (!_mikser1_Var.IsEqual(value))
                    changed = true;
                _mikser1_Var = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Mikser1_Var));
                }
            }
        }
        public bool Mikser2_Var
        {
            get
            {
                return _mikser2_Var;
            }
            set
            {
                bool changed = false;
                if (!_mikser2_Var.IsEqual(value))
                    changed = true;
                _mikser2_Var = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Mikser2_Var));
                }
            }
        }
        public int TartıBandı1_SiloSayısı
        {
            get
            {
                return _tartıBandı1_SiloSayısı;
            }
            set
            {
                bool changed = false;
                if (!_tartıBandı1_SiloSayısı.IsEqual(value))
                    changed = true;
                _tartıBandı1_SiloSayısı = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(TartıBandı1_SiloSayısı));
                }
            }
        }
        public int TartıBandı2_SiloSayısı
        {
            get
            {
                return _tartıBandı2_SiloSayısı;
            }
            set
            {
                bool changed = false;
                if (!_tartıBandı2_SiloSayısı.IsEqual(value))
                    changed = true;
                _tartıBandı2_SiloSayısı = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(TartıBandı2_SiloSayısı));
                }
            }
        }
        public int ÇimentoBunkeri1_SiloSayısı
        {
            get
            {
                return _çimentoBunkeri1_SiloSayısı;
            }
            set
            {
                bool changed = false;
                if (!_çimentoBunkeri1_SiloSayısı.IsEqual(value))
                    changed = true;
                _çimentoBunkeri1_SiloSayısı = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ÇimentoBunkeri1_SiloSayısı));
                }
            }
        }
        public int ÇimentoBunkeri2_SiloSayısı
        {
            get
            {
                return _çimentoBunkeri2_SiloSayısı;
            }
            set
            {
                bool changed = false;
                if (!_çimentoBunkeri2_SiloSayısı.IsEqual(value))
                    changed = true;
                _çimentoBunkeri2_SiloSayısı = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ÇimentoBunkeri2_SiloSayısı));
                }
            }
        }
        public int SuBunkeri1_SiloSayısı
        {
            get
            {
                return _suBunkeri1_SiloSayısı;
            }
            set
            {
                bool changed = false;
                if (!_suBunkeri1_SiloSayısı.IsEqual(value))
                    changed = true;
                _suBunkeri1_SiloSayısı = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SuBunkeri1_SiloSayısı));
                }
            }
        }
        public int SuBunkeri2_SiloSayısı
        {
            get
            {
                return _suBunkeri2_SiloSayısı;
            }
            set
            {
                bool changed = false;
                if (!_suBunkeri2_SiloSayısı.IsEqual(value))
                    changed = true;
                _suBunkeri2_SiloSayısı = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(SuBunkeri2_SiloSayısı));
                }
            }
        }
        public int KatkıBunkeri1_SiloSayısı
        {
            get
            {
                return _katkıBunkeri1_SiloSayısı;
            }
            set
            {
                bool changed = false;
                if (!_katkıBunkeri1_SiloSayısı.IsEqual(value))
                    changed = true;
                _katkıBunkeri1_SiloSayısı = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(KatkıBunkeri1_SiloSayısı));
                }
            }
        }
        public int KatkıBunkeri2_SiloSayısı
        {
            get
            {
                return _katkıBunkeri2_SiloSayısı;
            }
            set
            {
                bool changed = false;
                if (!_katkıBunkeri2_SiloSayısı.IsEqual(value))
                    changed = true;
                _katkıBunkeri2_SiloSayısı = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(KatkıBunkeri2_SiloSayısı));
                }
            }
        }
        public int Mikser1_KapakSayısı
        {
            get
            {
                return _mikser1_KapakSayısı;
            }
            set
            {
                bool changed = false;
                if (!_mikser1_KapakSayısı.IsEqual(value))
                    changed = true;
                _mikser1_KapakSayısı = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Mikser1_KapakSayısı));
                }
            }
        }
        public int Mikser2_KapakSayısı
        {
            get
            {
                return _mikser2_KapakSayısı;
            }
            set
            {
                bool changed = false;
                if (!_mikser2_KapakSayısı.IsEqual(value))
                    changed = true;
                _mikser2_KapakSayısı = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Mikser2_KapakSayısı));
                }
            }
        }
        #endregion
    }
}
