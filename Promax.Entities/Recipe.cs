using Promax.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class Recipe : INotifyPropertyChanged
    {
        private List<RecipeContent> _recipeContentList = new List<RecipeContent>();
        private string _gravity="false";
        private string _isHidden="false";

        public event PropertyChangedEventHandler PropertyChanged;

        #region FieldsByAutoPropCreator
        private int _recipeId = -1;
        private string _recipeCode;
        private int _recipeCatNum;
        private string _recipeName;
        private int _cweg1OrderTime;
        private int _cweg2OrderTime;
        private int _sweg1OrderTime;
        private int _sweg2OrderTime;
        private int _kweg1OrderTime;
        private int _kweg2OrderTime;
        private int _mixingTime;
        private int _fullopenTime;
        private int _lastopenTime;
        private double _unitPrice;
        private string _cementType;
        private string _mineralType;
        private string _additiveType;
        private string _consistency;
        private string _endurance;
        private string _dmax;
        private string _slump;
        private string _unitVolume;
        private string _environmental;
        private string _chlorideContent;
        #endregion
        #region PropertiesByAutoPropCreator
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
        public string RecipeCode
        {
            get
            {
                return _recipeCode;
            }
            set
            {
                bool changed = false;
                if (!_recipeCode.IsEqual(value))
                    changed = true;
                _recipeCode = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(RecipeCode));
                }
            }
        }
        public int RecipeCatNum
        {
            get
            {
                return _recipeCatNum;
            }
            set
            {
                bool changed = false;
                if (!_recipeCatNum.IsEqual(value))
                    changed = true;
                _recipeCatNum = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(RecipeCatNum));
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
        public int Cweg1OrderTime
        {
            get
            {
                return _cweg1OrderTime;
            }
            set
            {
                bool changed = false;
                if (!_cweg1OrderTime.IsEqual(value))
                    changed = true;
                _cweg1OrderTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Cweg1OrderTime));
                }
            }
        }
        public int Cweg2OrderTime
        {
            get
            {
                return _cweg2OrderTime;
            }
            set
            {
                bool changed = false;
                if (!_cweg2OrderTime.IsEqual(value))
                    changed = true;
                _cweg2OrderTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Cweg2OrderTime));
                }
            }
        }
        public int Sweg1OrderTime
        {
            get
            {
                return _sweg1OrderTime;
            }
            set
            {
                bool changed = false;
                if (!_sweg1OrderTime.IsEqual(value))
                    changed = true;
                _sweg1OrderTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Sweg1OrderTime));
                }
            }
        }
        public int Sweg2OrderTime
        {
            get
            {
                return _sweg2OrderTime;
            }
            set
            {
                bool changed = false;
                if (!_sweg2OrderTime.IsEqual(value))
                    changed = true;
                _sweg2OrderTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Sweg2OrderTime));
                }
            }
        }
        public int Kweg1OrderTime
        {
            get
            {
                return _kweg1OrderTime;
            }
            set
            {
                bool changed = false;
                if (!_kweg1OrderTime.IsEqual(value))
                    changed = true;
                _kweg1OrderTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Kweg1OrderTime));
                }
            }
        }
        public int Kweg2OrderTime
        {
            get
            {
                return _kweg2OrderTime;
            }
            set
            {
                bool changed = false;
                if (!_kweg2OrderTime.IsEqual(value))
                    changed = true;
                _kweg2OrderTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Kweg2OrderTime));
                }
            }
        }
        public int MixingTime
        {
            get
            {
                return _mixingTime;
            }
            set
            {
                bool changed = false;
                if (!_mixingTime.IsEqual(value))
                    changed = true;
                _mixingTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(MixingTime));
                }
            }
        }
        public int FullopenTime
        {
            get
            {
                return _fullopenTime;
            }
            set
            {
                bool changed = false;
                if (!_fullopenTime.IsEqual(value))
                    changed = true;
                _fullopenTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(FullopenTime));
                }
            }
        }
        public int LastopenTime
        {
            get
            {
                return _lastopenTime;
            }
            set
            {
                bool changed = false;
                if (!_lastopenTime.IsEqual(value))
                    changed = true;
                _lastopenTime = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(LastopenTime));
                }
            }
        }
        public double UnitPrice
        {
            get
            {
                return _unitPrice;
            }
            set
            {
                bool changed = false;
                if (!_unitPrice.IsEqual(value))
                    changed = true;
                _unitPrice = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(UnitPrice));
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
        #endregion
        #region FieldsByAutoPropCreator
        private string _enduranceRate = string.Empty;
        private string _fibers = string.Empty;
        private string _enduranceDay = string.Empty;
        #endregion
        #region PropertiesByAutoPropCreator
        public string EnduranceRate
        {
            get
            {
                return _enduranceRate;
            }
            set
            {
                bool changed = false;
                if (!_enduranceRate.IsEqual(value))
                    changed = true;
                _enduranceRate = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(EnduranceRate));
                }
            }
        }
        public string Fibers
        {
            get
            {
                return _fibers;
            }
            set
            {
                bool changed = false;
                if (!_fibers.IsEqual(value))
                    changed = true;
                _fibers = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Fibers));
                }
            }
        }
        public string EnduranceDay
        {
            get
            {
                return _enduranceDay;
            }
            set
            {
                bool changed = false;
                if (!_enduranceDay.IsEqual(value))
                    changed = true;
                _enduranceDay = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(EnduranceDay));
                }
            }
        }
        #endregion

        public string Gravity
        {
            get => _gravity; set
            {
                bool changed = false;
                if (!_gravity.IsEqual(value))
                    changed = true;
                _gravity = value.Boolify();
                OnPropertyChanged(nameof(Gravity));
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
                OnPropertyChanged(nameof(IsHidden));
                if (changed)
                {
                    OnPropertyChanged(nameof(IsHidden));
                }
            }
        }

        public IEnumerable<RecipeContent> RecipeContents => _recipeContentList;
        public void AddRecipeContent(RecipeContent recipeContent)
        {
            if (_recipeContentList.Contains(recipeContent))
                return;
            _recipeContentList.Add(recipeContent);
            recipeContent.Recipe = this;
            OnPropertyChanged(nameof(RecipeContents));
        }
        public void RemoveRecipeContent(RecipeContent recipeContent)
        {
            if (_recipeContentList.Contains(recipeContent))
                _recipeContentList.Remove(recipeContent);
            OnPropertyChanged(nameof(RecipeContents));
        }
        public void ClearRecipeContents()
        {
            _recipeContentList.Clear();
            OnPropertyChanged(nameof(RecipeContents));
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
