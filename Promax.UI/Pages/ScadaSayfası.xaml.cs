using Extensions;
using Promax.Business;
using Promax.Core;
using Promax.Entities;
using Promax.UI.Windows;
using RemoteVariableHandler.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Utility;
using Utility.Binding;

namespace Promax.UI
{
    /// <summary>
    /// ScadaSayfası.xaml etkileşim mantığı
    /// </summary>
    public partial class ScadaSayfası : Page
    {
        private static DoOnce BindVariablesOnce = new DoOnce();
        private static MyBinding _bindings = new MyBinding();
        public static readonly DependencyProperty ConnectedProperty =
      DependencyProperty.Register(nameof(Connected), typeof(bool), typeof(ScadaSayfası));
        public static readonly DependencyProperty OtoDolumProperty =
      DependencyProperty.Register(nameof(OtoDolum), typeof(bool), typeof(ScadaSayfası));
        public static readonly DependencyProperty OtoBoşaltımProperty =
      DependencyProperty.Register(nameof(OtoBoşaltım), typeof(bool), typeof(ScadaSayfası));
        public static readonly DependencyProperty OtoKarışımProperty =
      DependencyProperty.Register(nameof(OtoKarışım), typeof(bool), typeof(ScadaSayfası));
        #region Order Date Selection
        public static readonly DependencyProperty FirstDateProperty =
      DependencyProperty.Register(nameof(FirstDate), typeof(DateTime), typeof(ScadaSayfası));

        public static readonly DependencyProperty LastDateProperty =
    DependencyProperty.Register(nameof(LastDate), typeof(DateTime), typeof(ScadaSayfası));

        public static readonly DependencyProperty BugünProperty =
DependencyProperty.Register(nameof(Bugün), typeof(bool), typeof(ScadaSayfası));

        public static readonly DependencyProperty BuHaftaProperty =
DependencyProperty.Register(nameof(BuHafta), typeof(bool), typeof(ScadaSayfası));

        public static readonly DependencyProperty BuAyProperty =
DependencyProperty.Register(nameof(BuAy), typeof(bool), typeof(ScadaSayfası));

        public static readonly DependencyProperty BuYılProperty =
DependencyProperty.Register(nameof(BuYıl), typeof(bool), typeof(ScadaSayfası));

        public static readonly DependencyProperty TümüProperty =
DependencyProperty.Register(nameof(Tümü), typeof(bool), typeof(ScadaSayfası));
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
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Equals(FirstDateProperty) || e.Property.Equals(LastDateProperty))
            {
                HandleMenuItems();
            }
        }
        public DateTime FirstRecordDate { get; set; }
        public DateTime LastRecordDate { get; set; }
        private void HandleMenuItems()
        {
            Bugün = FirstDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate()) && LastDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate());
            BuHafta = FirstDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().StartOfWeek(DayOfWeek.Monday)) && LastDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().EndOfWeek(DayOfWeek.Monday));
            BuAy = FirstDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().StartOfMonth()) && LastDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().EndOfMonth());
            BuYıl = FirstDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().StartOfYear()) && LastDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().EndOfYear());
            Tümü = (FirstDate.MakeFirstDate().IsEarlierThan(FirstRecordDate.MakeFirstDate()) || FirstDate.MakeFirstDate().IsSame(FirstRecordDate.MakeFirstDate())) && (LastDate.MakeFirstDate().IsLaterThan(LastRecordDate.MakeFirstDate()) || LastDate.MakeFirstDate().IsSame(LastRecordDate.MakeFirstDate()));
        }
        private void BugünClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate();
            LastDate = DateTime.Now.MakeFirstDate();
            ListOrders();
        }
        private void BuHaftaClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate().StartOfWeek(DayOfWeek.Monday);
            LastDate = DateTime.Now.MakeFirstDate().EndOfWeek(DayOfWeek.Monday);
            ListOrders();
        }
        private void BuAyClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate().StartOfMonth();
            LastDate = DateTime.Now.MakeFirstDate().EndOfMonth();
            ListOrders();
        }
        private void BuYılClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate().StartOfYear();
            LastDate = DateTime.Now.MakeFirstDate().EndOfYear();
            ListOrders();
        }
        private void TümüClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = FirstRecordDate.MakeFirstDate();
            LastDate = DateTime.Now.MakeFirstDate().EndOfWeek(DayOfWeek.Monday);
            ListOrders();
        }
        #endregion
        private IOrderManager OrderManager { get => Infrastructure.Main.OrderManager; }
        private IProductManager ProductManager { get => Infrastructure.Main.ProductManager; }
        private IStockManager StockManager { get => Infrastructure.Main.StockManager; }
        private DateTime _firstDateBuffer;
        private DateTime _lastDateBuffer;
        public object selectedOrder { get; set; }
        public Order SelectedOrder { get => selectedOrder as Order; }
        public object selectedProduct { get; set; }
        public Product SelectedProduct { get => selectedProduct as Product; }
        public bool Connected
        {
            get => (bool)GetValue(ConnectedProperty); set
            {
                var oldValue = Connected;
                var newValue = value;
                SetValue(ConnectedProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(ConnectedProperty, oldValue, newValue));
            }
        }
        public bool OtoDolum
        {
            get => (bool)GetValue(OtoDolumProperty); set
            {
                var oldValue = OtoDolum;
                var newValue = value;
                SetValue(OtoDolumProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(OtoDolumProperty, oldValue, newValue));
            }
        }
        public bool OtoBoşaltım
        {
            get => (bool)GetValue(OtoBoşaltımProperty); set
            {
                var oldValue = OtoBoşaltım;
                var newValue = value;
                SetValue(OtoBoşaltımProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(OtoBoşaltımProperty, oldValue, newValue));
            }
        }
        public bool OtoKarışım
        {
            get => (bool)GetValue(OtoKarışımProperty); set
            {
                var oldValue = OtoKarışım;
                var newValue = value;
                SetValue(OtoKarışımProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(OtoKarışımProperty, oldValue, newValue));
            }
        }
        public VariableOwnerContainer VariableOwnerContainer { get => (VariableOwnerContainer)Resources["VariableOwnerContainer1"]; set => Resources["VariableOwnerContainer1"] = value; }
        public ObservableCollection<Product> Products { get; set; }
        public ScadaSayfası()
        {
            InitializeComponent();
            FirstDate = DateTime.Now;
            LastDate = DateTime.Now;
        }
        private void ListProductions()
        {
            Products = new ObservableCollection<Product>(ProductManager.GetList(x => x.Pos < 3));
            productionDataGrid.ItemsSource = Products;
        }
        private void ListOrders()
        {
            if (!FirstDate.Equals(_firstDateBuffer) || !LastDate.Equals(_lastDateBuffer))
                RefreshOrders();
        }

        private void RefreshOrders()
        {
            ObservableCollection<Order> Orders = new ObservableCollection<Order>(OrderManager.GetList(x => x.Remaining > 0).
                            DoReturn(x => { x.OrderByDescending(y => y.OrderDate).ToList().FirstOrDefault().Do(y => LastRecordDate = y.OrderDate); }).
                            DoReturn(x => { x.OrderBy(y => y.OrderDate).ToList().FirstOrDefault().Do(y => FirstRecordDate = y.OrderDate); }).
                            Where(x => x.OrderDate >= FirstDate && x.OrderDate <= LastDate).ToList());
            _firstDateBuffer = FirstDate;
            _lastDateBuffer = LastDate;
            orderDataGrid.ItemsSource = Orders;
        }

        private void CreateNewOrder(object sender, RoutedEventArgs e)
        {
            var a = SiparişKartı.CreateNew();
            a.ShowDialog();
            RefreshOrders();
        }
        private void EditOrder(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder == null)
                return;
            var a = SiparişKartı.Edit(SelectedOrder);
            a.ShowDialog();
            RefreshOrders();
        }
        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder == null || MessageBoxResult.Yes != MessageBox.Show("Seçili kayıtlar silinsin mi?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information))
                return;
            DeleteOrder(SelectedOrder);
            RefreshOrders();
        }
        private void DeleteOrder(Order order)
        {
            OrderManager.Delete(order);
        }
        private void RefreshOrders(object sender, RoutedEventArgs e)
        {
            RefreshOrders();
        }
        private void CreateNewProduction(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder == null)
                return;
            var a = GörevKartı.CreateNew(SelectedOrder);
            a.ShowDialog();
            ListProductions();
        }
        private void EditProduction(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct == null)
                return;
            var a = GörevKartı.Edit(SelectedProduct);
            a.ShowDialog();
            ListProductions();
        }
        private void DeleteProduction(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct == null || MessageBoxResult.Yes != MessageBox.Show("Seçili kayıtlar silinsin mi?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information))
                return;
            DeleteProduction(SelectedProduct);
            ListProductions();
        }
        private void DeleteProduction(Product product)
        {
            Infrastructure.Main.HandleExceptions(() => product.DoIf(x => x.PosStatus != ProductionStatus.ÜretimiBaşlat, x => ProductManager.Delete(x)));
        }
        private void DeleteAllProductions(object sender, RoutedEventArgs e)
        {
            Products.Do(x =>
            {
                foreach (var item in x)
                {
                    DeleteProduction(item);
                }
            });
            ListProductions();
        }
        private void RefreshProductions(object sender, RoutedEventArgs e)
        {
            ListProductions();
        }
        private void UretimKartı_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DateTimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ListOrders();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListOrders();
            ListProductions();
            batchInfoGrid.ItemsSource = new ObservableCollection<Stock>(StockManager.GetList().OrderBy(x => x.StockType));
        }

        private void StartProduction(object sender, RoutedEventArgs e)
        {
            
        }

        private void DolumButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BosaltimButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void KarisimButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
    }
}
