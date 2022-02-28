using Promax.Business.Abstract;
using Promax.Business.Mappers;
using Promax.Core;
using Promax.Entities;
using Promax.UI.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Promax.UI
{
    /// <summary>
    /// ReçetelerSayfası.xaml etkileşim mantığı
    /// </summary>
    public partial class ReçetelerSayfası : Page
    {
        public static readonly DependencyProperty LockedProperty =
       DependencyProperty.Register(nameof(Unlocked), typeof(bool), typeof(ReçetelerSayfası));
        public static readonly DependencyProperty SelectedRecipeNormsProperty =
      DependencyProperty.Register(nameof(SelectedRecipeNorms), typeof(NormViewDTO), typeof(ReçetelerSayfası));
        public static readonly DependencyProperty SelectedRecipeProperty =
    DependencyProperty.Register(nameof(SelectedRecipe), typeof(Recipe), typeof(ReçetelerSayfası));
        private object _selectedRecipe;
        public IComplexRecipeManager ComplexRecipeManager { get => Infrastructure.Main.GetRecipeManager(); }
        public IComplexRecipeContentManager ComplexRecipeContentManager { get => Infrastructure.Main.GetRecipeContentManager(); }
        public IComplexStockManager StockManager { get => Infrastructure.Main.GetStockManager(); }
        public INormViewManager NormViewManager { get => Infrastructure.Main.GetNormViewManager(); }
        public IBeeMapper Mapper { get => Infrastructure.Main.GetMapper(); }
        private List<Stock> _stocks;

        public object selectedRecipe
        {
            get => _selectedRecipe; set
            {
                _selectedRecipe = value;
                SelectedRecipe = _selectedRecipe == null ? null : (Recipe)_selectedRecipe;
                RecipeSelected();
            }
        }
        public object selectedStock { get; set; }
        public object selectedRecipeContent { get; set; }
        public Recipe SelectedRecipe
        {
            get { return (Recipe)GetValue(SelectedRecipeProperty); }
            set
            {
                var oldValue = (Recipe)GetValue(SelectedRecipeProperty);
                var newValue = value;
                SetValue(SelectedRecipeProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(SelectedRecipeProperty, oldValue, newValue));
            }
        }
        private Stock SelectedStock { get { return selectedStock == null ? null : (Stock)selectedStock; } }
        private RecipeContent SelectedRecipeContent { get { return selectedRecipeContent == null ? null : (RecipeContent)selectedRecipeContent; } }
        public NormViewDTO SelectedRecipeNorms
        {
            get => (NormViewDTO)GetValue(SelectedRecipeNormsProperty); set
            {
                var oldValue = (NormViewDTO)GetValue(SelectedRecipeNormsProperty);
                var newValue = value;
                SetValue(SelectedRecipeNormsProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(SelectedRecipeNormsProperty, oldValue, newValue));
            }
        }
        public ReçetelerSayfası()
        {
            InitializeComponent();
        }
        public bool Unlocked
        {
            get => (bool)GetValue(LockedProperty); set
            {
                var oldValue = (bool)GetValue(LockedProperty);
                var newValue = value;
                SetValue(LockedProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(LockedProperty, oldValue, newValue));
            }
        }
        private void AddRecipeContent(object sender, RoutedEventArgs e)
        {
            if (SelectedRecipe == null || SelectedStock == null || !Unlocked)
                return;
            RecipeContent recipeContent = new RecipeContent() { Stock = SelectedStock };
            SelectedRecipe.AddRecipeContent(recipeContent);
            ComplexRecipeContentManager.Add(recipeContent);
            recipeContent.PropertyChanged += RecipeContent_PropertyChanged;
            RecipeSelected();
        }
        private void RemoveRecipeContent(object sender, RoutedEventArgs e)
        {
            if (SelectedRecipe == null || SelectedRecipeContent == null || !Unlocked)
                return;
            SelectedRecipe.RemoveRecipeContent(SelectedRecipeContent);
            ComplexRecipeContentManager.Delete(SelectedRecipeContent);
            SelectedRecipeContent.PropertyChanged += RecipeContent_PropertyChanged;
            RecipeSelected();
        }
        private void RecipeContent_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RecipeContent recipeContent = sender as RecipeContent;
            if (recipeContent != null)
            {
                ComplexRecipeContentManager.Update(recipeContent, recipeContent);
                RecipeSelected();
            }
        }
        private void CreateNewRecipe(object sender, RoutedEventArgs e)
        {
            ReçeteKartı reçeteKartı = ReçeteKartı.CreateNew();
            reçeteKartı.ShowDialog();
            ListRecipes();
        }

        private void EditRecipe(object sender, RoutedEventArgs e)
        {
            if (SelectedRecipe == null)
                return;
            ReçeteKartı reçeteKartı = ReçeteKartı.Edit(SelectedRecipe);
            reçeteKartı.ShowDialog();
            ListRecipes();
        }
        private void DeleteRecipe(object sender, RoutedEventArgs e)
        {
            if (SelectedRecipe == null || MessageBoxResult.Yes != MessageBox.Show("Seçili kayıtlar silinsin mi?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information))
                return;
            DeleteRecipe(SelectedRecipe);
        }
        private void DeleteRecipe(Recipe recipe)
        {
            var recipeDto = new Recipe();
            Mapper.Map<Recipe, Recipe>(recipe, recipeDto);
            recipeDto.IsHidden = "true";
            ComplexRecipeManager.Update(recipeDto, recipe);
            ListRecipes();
        }
        private void RefreshRecipes(object sender, RoutedEventArgs e)
        {
            ListRecipes();
        }
        private void GetStocks()
        {
            _stocks = StockManager.GetList();
        }
        private void ListRecipes()
        {
            var list = ComplexRecipeManager.GetList(x => x.IsHidden == "false");
            ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>(list);
            recipeDataGrid.ItemsSource = recipes;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetStocks();
            ListRecipes();
        }
        private void RecipeSelected()
        {
            ObservableCollection<Stock> stocks = new ObservableCollection<Stock>();
            ObservableCollection<RecipeContent> recipeContents = new ObservableCollection<RecipeContent>();
            SelectedRecipeNorms = null;
            if (SelectedRecipe != null)
            {
                recipeContents = new ObservableCollection<RecipeContent>(SelectedRecipe.RecipeContents);
                foreach (var stock in _stocks)
                {
                    bool inRecipeContents = false;
                    foreach (var recipeContent in SelectedRecipe.RecipeContents)
                    {
                        if (stock.Equals(recipeContent.Stock))
                        {
                            inRecipeContents = true;
                            break;
                        }
                    }
                    if (!inRecipeContents)
                        stocks.Add(stock);
                }
                SelectedRecipeNorms = NormViewManager.Get(x => x.RecipeId == SelectedRecipe.RecipeId);
                foreach (var item in SelectedRecipe.RecipeContents)
                {
                    item.PropertyChanged += RecipeContent_PropertyChanged;
                }
            }
            stockDataGrid.ItemsSource = stocks;
            recipeContentDataGrid.ItemsSource = recipeContents;
        }

        private void RecipeGravityCheckClicked(object sender, RoutedEventArgs e)
        {
            Recipe recipe = recipeDataGrid.CurrentItem as Recipe;
            if (recipe != null)
            {
                ComplexRecipeManager.Update(recipe, recipe);
            }
        }

        private void recipeContentDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //RecipeContent recipeContent = e.Row.Item as RecipeContent;
            //if(recipeContent != null)
            //{
            //    ComplexRecipeContentManager.Update(recipeContent, recipeContent);
            //    RecipeSelected();
            //}
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditRecipe(null, null);
        }

        private void DataGridRow_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            AddRecipeContent(null, null);
        }

        private void DataGridRow_MouseDoubleClick_2(object sender, MouseButtonEventArgs e)
        {
            if (!(sender as DataGridCell).IsReadOnly)
                return;
            RemoveRecipeContent(null, null);
        }
    }
}
