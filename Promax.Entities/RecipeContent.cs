using Promax.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class RecipeContent : INotifyPropertyChanged
    {
        private Recipe _recipe;
        private Stock _stock;
        #region FieldsByAutoPropCreator
        private int _contentId = -1;
        private int _recipeId;
        private int _stockId;
        private double _quantity;
        #endregion
        #region PropertiesByAutoPropCreator
        public int ContentId
        {
            get
            {
                return _contentId;
            }
            set
            {
                bool changed = false;
                if (!_contentId.IsEqual(value))
                    changed = true;
                _contentId = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ContentId));
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
        #endregion

        public Recipe Recipe
        {
            get => _recipe;
            set
            {
                bool changed = false;
                if (!_recipe.IsEqual(value))
                    changed = true;
                _recipe = value;
                value.Do(o => RecipeId = o.RecipeId,() => RecipeId = -1);
                if (changed)
                {
                    OnPropertyChanged(nameof(Recipe));
                }
            }
        }
        public Stock Stock
        {
            get => _stock;
            set
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
