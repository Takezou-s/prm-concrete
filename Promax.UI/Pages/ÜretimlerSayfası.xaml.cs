using Promax.Business.Abstract;
using Promax.Core;
using Promax.Entities;
using Promax.UI.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
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
    /// ÜretimlerSayfası.xaml etkileşim mantığı
    /// </summary>
    public partial class ÜretimlerSayfası : Page
    {
        public static readonly DependencyProperty FirstDateProperty =
     DependencyProperty.Register(nameof(FirstDate), typeof(DateTime), typeof(ÜretimlerSayfası));

        public static readonly DependencyProperty LastDateProperty =
    DependencyProperty.Register(nameof(LastDate), typeof(DateTime), typeof(ÜretimlerSayfası));

        public static readonly DependencyProperty BugünProperty =
DependencyProperty.Register(nameof(Bugün), typeof(bool), typeof(ÜretimlerSayfası));

        public static readonly DependencyProperty BuHaftaProperty =
DependencyProperty.Register(nameof(BuHafta), typeof(bool), typeof(ÜretimlerSayfası));

        public static readonly DependencyProperty BuAyProperty =
DependencyProperty.Register(nameof(BuAy), typeof(bool), typeof(ÜretimlerSayfası));

        public static readonly DependencyProperty BuYılProperty =
DependencyProperty.Register(nameof(BuYıl), typeof(bool), typeof(ÜretimlerSayfası));

        public static readonly DependencyProperty TümüProperty =
DependencyProperty.Register(nameof(Tümü), typeof(bool), typeof(ÜretimlerSayfası));
        public DateTime FirstDate
        {
            get { return (DateTime)GetValue(FirstDateProperty); }
            set
            {
                var oldValue = FirstDate;
                var newValue = value;
                SetValue(FirstDateProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(FirstDateProperty, oldValue, newValue));
            }
        }
        public DateTime LastDate
        {
            get { return (DateTime)GetValue(LastDateProperty); }
            set
            {
                var oldValue = LastDate;
                var newValue = value;
                SetValue(LastDateProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(LastDateProperty, oldValue, newValue));
            }
        }
        public bool Bugün
        {
            get => (bool)GetValue(BugünProperty); set
            {
                var oldValue = Bugün;
                var newValue = value;
                SetValue(BugünProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(BugünProperty, oldValue, newValue));
            }
        }

        public bool BuHafta
        {
            get => (bool)GetValue(BuHaftaProperty); set
            {
                var oldValue = BuHafta;
                var newValue = value;
                SetValue(BuHaftaProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(BuHaftaProperty, oldValue, newValue));
            }
        }

        public bool BuAy
        {
            get => (bool)GetValue(BuAyProperty); set
            {
                var oldValue = BuAy;
                var newValue = value;
                SetValue(BuAyProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(BuAyProperty, oldValue, newValue));
            }
        }

        public bool BuYıl
        {
            get => (bool)GetValue(BuYılProperty); set
            {
                var oldValue = BuYıl;
                var newValue = value;
                SetValue(BuYılProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(BuYılProperty, oldValue, newValue));
            }
        }

        public bool Tümü
        {
            get => (bool)GetValue(TümüProperty); set
            {
                var oldValue = Tümü;
                var newValue = value;
                SetValue(TümüProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(TümüProperty, oldValue, newValue));
            }
        }
        public object selectedProduct
        {
            get => _selectedProduct; set
            {
                bool changed = !_selectedProduct.IsEqual(value);
                _selectedProduct = value;
                changed.DoIf(x => x, x => ProductSelected());
            }
        }
        public Product SelectedProduct { get => selectedProduct as Product; }
        public DateTime FirstRecordDate { get; set; }
        public DateTime LastRecordDate { get; set; }
        private DateTime _firstDateBuffer;
        private DateTime _lastDateBuffer;
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Equals(FirstDateProperty) || e.Property.Equals(LastDateProperty))
            {
                HandleMenuItems();
            }
        }
        public ÜretimlerSayfası()
        {
            InitializeComponent();
            FirstDate = DateTime.Now;
            LastDate = DateTime.Now;
        }

        bool sekans = false;
        Dictionary<RowDefinition, GridLength> rowLengths = new Dictionary<RowDefinition, GridLength>();
        private object _selectedProduct;

        private void IzgaraButon_Click(object sender, RoutedEventArgs e)
        {
            if (sekans == false)
            {
                rowLengths.Clear();
                foreach (var item in anagrid.RowDefinitions)
                {
                    if (!rowLengths.ContainsKey(item))
                    {
                        rowLengths.Add(item, item.Height);
                    }
                    item.Height = GridLength.Auto;
                }
                anagrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                Grid2.Visibility = Visibility.Collapsed;
                spliter.Visibility = Visibility.Collapsed;
                grid.Width = anagrid.Width;
                //var a = App.Current.Resources["Button.Static.Background"];
                sekans = true;
            }
            else
            {
                foreach (var item in rowLengths)
                {
                    item.Key.Height = item.Value;
                }
                //grid.Width = Double.NaN;
                Grid2.Visibility = Visibility.Visible;
                spliter.Visibility = Visibility.Visible;
                sekans = false;
            }
        }

        private void CreateNewProduction(object sender, RoutedEventArgs e)
        {
            ÜretimKartı.CreateNew().ShowDialog();
            RefreshProducts();
        }
        private void EditProduction(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct == null)
                return;
            ÜretimKartı.Edit(SelectedProduct).ShowDialog();
            RefreshProducts();
        }
        private void DeleteProduction(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct == null || MessageBoxResult.Yes != MessageBox.Show("Seçili kayıtlar silinsin mi?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information))
                return;
            DeleteProduct(SelectedProduct);
            RefreshProducts();
        }
        private void DeleteProduct(Product product)
        {
            ProductManager.Delete(product);
        }
        private void RefreshProducts(object sender, RoutedEventArgs e)
        {
            RefreshProducts();
        }
        private void BugünClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate();
            LastDate = DateTime.Now.MakeFirstDate();
            ListProducts();
        }
        private void BuHaftaClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate().StartOfWeek(DayOfWeek.Monday);
            LastDate = DateTime.Now.MakeFirstDate().EndOfWeek(DayOfWeek.Monday);
            ListProducts();
        }
        private void BuAyClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate().StartOfMonth();
            LastDate = DateTime.Now.MakeFirstDate().EndOfMonth();
            ListProducts();
        }
        private void BuYılClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate().StartOfYear();
            LastDate = DateTime.Now.MakeFirstDate().EndOfYear();
            ListProducts();
        }
        private void TümüClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = FirstRecordDate.MakeFirstDate();
            LastDate = DateTime.Now.MakeFirstDate().EndOfWeek(DayOfWeek.Monday);
            ListProducts();
        }
        private void HandleMenuItems()
        {
            Bugün = FirstDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate()) && LastDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate());
            BuHafta = FirstDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().StartOfWeek(DayOfWeek.Monday)) && LastDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().EndOfWeek(DayOfWeek.Monday));
            BuAy = FirstDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().StartOfMonth()) && LastDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().EndOfMonth());
            BuYıl = FirstDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().StartOfYear()) && LastDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().EndOfYear());
            Tümü = (FirstDate.MakeFirstDate().IsEarlierThan(FirstRecordDate.MakeFirstDate()) || FirstDate.MakeFirstDate().IsSame(FirstRecordDate.MakeFirstDate())) && (LastDate.MakeFirstDate().IsLaterThan(LastRecordDate.MakeFirstDate()) || LastDate.MakeFirstDate().IsSame(LastRecordDate.MakeFirstDate()));
        }
        private void ListProducts()
        {
            if (!FirstDate.Equals(_firstDateBuffer) || !LastDate.Equals(_lastDateBuffer))
                RefreshProducts();
        }
        private void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            ListProducts();
        }

        private void DatePicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ListProducts();
        }
        IComplexProductDalManager ProductManager { get => Infrastructure.Main.GetProductDalManager(); }
        IComplexClientManager ClientManager { get => Infrastructure.Main.GetClientManager(); }
        IComplexSiteManager SiteManager { get => Infrastructure.Main.GetSiteManager(); }
        IComplexOrderManager OrderManager { get => Infrastructure.Main.GetOrderManager(); }
        IComplexRecipeManager RecipeManager { get => Infrastructure.Main.GetRecipeManager(); }
        IComplexServiceCategoryManager ServiceCategoryManager { get => Infrastructure.Main.GetServiceCategoryManager(); }
        IComplexServiceManager ServiceManager { get => Infrastructure.Main.GetServiceManager(); }
        IComplexDriverManager DriverManager { get => Infrastructure.Main.GetDriverManager(); }
        IComplexUserManager UserManager { get => Infrastructure.Main.GetUserManager(); }
        IComplexBatchedStockDalManager BatchedStockManager { get => Infrastructure.Main.GetBatchedStockManager(); }
        IComplexStockManager StockManager { get => Infrastructure.Main.GetStockManager(); }
        private void RefreshProducts()
        {
            var list = ProductManager.GetList(x => x.ProductDate >= FirstDate && x.ProductDate <= LastDate);
            list.ForEach(x => InitProduct(x));
            ObservableCollection<Product> products = new ObservableCollection<Product>(list);
            productDataGrid.ItemsSource = products;
            _firstDateBuffer = FirstDate;
            _lastDateBuffer = LastDate;
        }

        private void uretimlerpage_Loaded(object sender, RoutedEventArgs e)
        {
            ProductManager.GetList().DoReturn(x => x.OrderBy(y => y.ProductDate).FirstOrDefault().Do(y => FirstRecordDate = y.ProductDate)).Do
            (x => x.OrderByDescending(y => y.ProductDate).FirstOrDefault().Do(y => LastRecordDate = y.ProductDate));
            ListProducts();
        }
        private void InitProduct(Product product)
        {
            product.Do(x =>
            {
                x.Client = ClientManager.Get(y => y.ClientId == x.ClientId);
                x.Site = SiteManager.Get(y => y.SiteId == x.SiteId);
                x.Order = OrderManager.Get(y => y.OrderId == x.OrderId);
                x.Recipe = RecipeManager.Get(y => y.RecipeId == x.RecipeId);
                x.ServiceCategory = ServiceCategoryManager.Get(y => y.CatNum == x.ServiceCatNum);
                int val = 0;
                val.DoIf(v => v <= 0, v => x.MixerServiceId.DoIf(z => z > 0, z => val = z));
                val.DoIf(v => v <= 0, v => x.PumpServiceId.DoIf(z => z > 0, z => val = z));
                val.DoIf(v => v <= 0, v => x.SelfServiceId.DoIf(z => z > 0, z => val = z));
                x.Service = ServiceManager.Get(y => y.ServiceId == val);
                val = 0;
                val.DoIf(v => v <= 0, v => x.MixerDriverId.DoIf(z => z > 0, z => val = z));
                val.DoIf(v => v <= 0, v => x.PumpDriverId.DoIf(z => z > 0, z => val = z));
                val.DoIf(v => v <= 0, v => x.SelfDriverId.DoIf(z => z > 0, z => val = z));
                x.Driver = DriverManager.Get(y => y.DriverId == val);
                x.User = UserManager.Get(y => y.UserId == x.UserId);
            });
        }
        private void Bind(object source, string propertyPath, DependencyObject target, DependencyProperty dp)
        {
            Binding binding = new Binding();
            binding.Source = source;
            binding.Path = new PropertyPath(propertyPath);
            BindingOperations.SetBinding(target, dp, binding);
        }
        private void ProductSelected()
        {
            datagrid2.Columns.Clear();
            datagrid3.Columns.Clear();
            if (SelectedProduct == null)
                return;
            var list = BatchedStockManager.GetList(x => x.ProductId == SelectedProduct.ProductId);
            BatchedStockGridAdapterCollection collection = new BatchedStockGridAdapterCollection();
            foreach (var item in list)
            {
                collection.AddBatchedStock(item);
            }
            var stockIdList = collection.StockIds.ToList();
            stockIdList.Sort();
            var stockList = StockManager.GetList();
            var stockArray = stockList.ToArray();
            foreach (var stock in stockArray)
            {
                bool inList = false;
                foreach (var id in stockIdList)
                {
                    if (id == stock.StockId)
                    {
                        inList = true;
                        break;
                    }
                }
                if (!inList)
                {
                    stockList.Remove(stock);
                }
            }
            stockIdList.Clear();
            stockList.OrderBy(x => x.StockType).ToList().ForEach(x => stockIdList.Add(x.StockId));

            DataGridTextColumn batchNoColumn = new DataGridTextColumn();
            batchNoColumn.Header = string.Empty;
            batchNoColumn.Binding = new Binding("BatchNo");
            datagrid2.Columns.Add(batchNoColumn);

            DataGridTextColumn batchNoColumn1 = new DataGridTextColumn();
            batchNoColumn1.Header = string.Empty;
            batchNoColumn1.Binding = new Binding("BatchNo");
            datagrid3.Columns.Add(batchNoColumn1);
            Bind(batchNoColumn, nameof(batchNoColumn.ActualWidth), batchNoColumn1, DataGridTextColumn.WidthProperty);
            Bind(batchNoColumn, nameof(batchNoColumn.DisplayIndex), batchNoColumn1, DataGridTextColumn.DisplayIndexProperty);


            DataGridTextColumn batchedTimeColumn = new DataGridTextColumn();
            batchedTimeColumn.Header = "Saat";
            batchedTimeColumn.Binding = new Binding("BatchedTime");
            datagrid2.Columns.Add(batchedTimeColumn);

            DataGridTextColumn batchedTimeColumn1 = new DataGridTextColumn();
            batchedTimeColumn1.Header = "Saat";
            batchedTimeColumn1.Binding = new Binding("BatchedTime");
            datagrid3.Columns.Add(batchedTimeColumn1);
            Bind(batchedTimeColumn, nameof(batchedTimeColumn.ActualWidth), batchedTimeColumn1, DataGridTextColumn.WidthProperty);
            Bind(batchedTimeColumn, nameof(batchedTimeColumn.DisplayIndex), batchedTimeColumn1, DataGridTextColumn.DisplayIndexProperty);

            stockIdList.ForEach(x =>
            {
                DataGridTextColumn stockColumn = new DataGridTextColumn();
                stockColumn.Header = string.Empty;
                StockManager.Get(y => y.StockId == x).Do(y => stockColumn.Header = y.StockName);
                stockColumn.Binding = new Binding("Stock" + x.ToString() + "_Batched");
                datagrid2.Columns.Add(stockColumn);

                DataGridTextColumn stockColumn1 = new DataGridTextColumn();
                stockColumn1.Header = string.Empty;
                StockManager.Get(y => y.StockId == x).Do(y => stockColumn1.Header = y.StockName);
                stockColumn1.Binding = new Binding("Stock" + x.ToString() + "_Batched");
                datagrid3.Columns.Add(stockColumn1);
                Bind(stockColumn, nameof(stockColumn.ActualWidth), stockColumn1, DataGridTextColumn.WidthProperty);
                Bind(stockColumn, nameof(stockColumn.DisplayIndex), stockColumn1, DataGridTextColumn.DisplayIndexProperty);
            });
            DataGridTextColumn toplamColumn = new DataGridTextColumn();
            toplamColumn.Header = "Toplam";
            toplamColumn.Binding = new Binding("Sum");
            datagrid2.Columns.Add(toplamColumn);

            DataGridTextColumn toplamColumn1 = new DataGridTextColumn();
            toplamColumn1.Header = "Toplam";
            toplamColumn1.Binding = new Binding("Sum");
            datagrid3.Columns.Add(toplamColumn1);
            Bind(toplamColumn, nameof(toplamColumn.ActualWidth), toplamColumn1, DataGridTextColumn.WidthProperty);
            Bind(toplamColumn, nameof(toplamColumn.DisplayIndex), toplamColumn1, DataGridTextColumn.DisplayIndexProperty);

            var list1 = collection.BatchedStocks.OrderBy(x => x.BatchNo).ToList();
            foreach (var item in list1)
            {
                dynamic dynamicRow = new ExpandoObject();
                ((IDictionary<String, Object>)dynamicRow)["BatchNo"] = item.BatchNo;
                ((IDictionary<String, Object>)dynamicRow)["BatchedTime"] = item.BatchedTime.ToString("HH:mm:ss");
                List<object> row = new List<object>();
                row.Add(item.BatchNo);
                row.Add(item.BatchedTime.ToString("HH:mm:ss"));
                stockIdList.ForEach(x =>
                {
                    row.Add(item.GetBatchedQuantity(x));
                    ((IDictionary<String, Object>)dynamicRow)["Stock" + x.ToString() + "_Batched"] = item.GetBatchedQuantity(x);
                });
                row.Add(item.Sum);
                ((IDictionary<String, Object>)dynamicRow)["Sum"] = item.Sum;
                datagrid2.Items.Add(dynamicRow);
            }
            dynamic dynamicRow1 = new ExpandoObject();
            double stockSum = 0;
            stockIdList.ForEach(x =>
            {
                ((IDictionary<String, Object>)dynamicRow1)["Stock" + x.ToString() + "_Batched"] = collection.GetStockSum(x);
                stockSum += collection.GetStockSum(x);
            });
            ((IDictionary<String, Object>)dynamicRow1)["Sum"] = stockSum;
            datagrid3.Items.Add(dynamicRow1);
            ;
        }
    }
}
