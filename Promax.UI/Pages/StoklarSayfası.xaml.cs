using FirebirdSql.Data.FirebirdClient;
using Promax.Business.Abstract;
using Promax.Business.Mappers;
using Promax.Core;
using Promax.DataAccess;
using Promax.DataAccess.Abstract;
using Promax.DataAccess.Concrete.EntityFramework;
using Promax.Entities;
using Promax.UI.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
    /// StoklarSayfası.xaml etkileşim mantığı
    /// </summary>
    public partial class StoklarSayfası : Page
    {
        public static readonly DependencyProperty SiloStockQuantityProperty =
       DependencyProperty.Register(nameof(SiloStockQuantity), typeof(string), typeof(StoklarSayfası));

        public static readonly DependencyProperty FirstDateProperty =
      DependencyProperty.Register(nameof(FirstDate), typeof(DateTime), typeof(StoklarSayfası));

        public static readonly DependencyProperty LastDateProperty =
    DependencyProperty.Register(nameof(LastDate), typeof(DateTime), typeof(StoklarSayfası));

        public static readonly DependencyProperty BugünProperty =
DependencyProperty.Register(nameof(Bugün), typeof(bool), typeof(StoklarSayfası));

        public static readonly DependencyProperty BuHaftaProperty =
DependencyProperty.Register(nameof(BuHafta), typeof(bool), typeof(StoklarSayfası));

        public static readonly DependencyProperty BuAyProperty =
DependencyProperty.Register(nameof(BuAy), typeof(bool), typeof(StoklarSayfası));

        public static readonly DependencyProperty BuYılProperty =
DependencyProperty.Register(nameof(BuYıl), typeof(bool), typeof(StoklarSayfası));

        public static readonly DependencyProperty TümüProperty =
DependencyProperty.Register(nameof(Tümü), typeof(bool), typeof(StoklarSayfası));

        private object _selectedStock;
        private object _selectedStockEntry;
        private DateTime _firstDateBuffer;
        private DateTime _lastDateBuffer;

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

        public IComplexStockManager StockManager { get => Infrastructure.Main.GetStockManager(); }
        public IComplexStockEntryDalManager StockEntryManager { get => Infrastructure.Main.GetStockEntryManager(); }
        public IComplexSiloManager SiloManager { get => Infrastructure.Main.GetSiloManager(); }
        public IBeeMapper Mapper { get => Infrastructure.Main.GetMapper(); }
        public IStockMovementViewReader BatchedStockMovementViewReader { get => Infrastructure.Main.GetBatchedStockViewReader(); }
        public IStockMovementViewReader StockEntryViewReader { get => Infrastructure.Main.GetStockEntryViewReader(); }
        public IStockMovementViewReader ConsumedStockMovementViewReader { get => Infrastructure.Main.GetConsumedStockViewReader(); }
        public object selectedStock
        {
            get => _selectedStock; set
            {
                _selectedStock = value;
                StockSelected();
            }
        }
        public object selectedStockEntry
        {
            get => _selectedStockEntry; set
            {
                _selectedStockEntry = value;
            }
        }
        public StockEntry SelectedStockEntry { get => selectedStockEntry as StockEntry; }
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
        public Stock SelectedStock { get { return selectedStock as Stock; } }
        public string SiloStockQuantity
        {
            get { return (string)GetValue(SiloStockQuantityProperty); }
            set
            {
                var oldValue = SiloStockQuantity;
                var newValue = value;
                SetValue(SiloStockQuantityProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(SiloStockQuantityProperty, oldValue, newValue));
            }
        }
        public StoklarSayfası()
        {
            InitializeComponent();
            FirstDate = DateTime.Now;
            LastDate = DateTime.Now;
        }
        public DateTime FirstRecordDate { get; set; }
        public DateTime LastRecordDate { get; set; }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Equals(FirstDateProperty) || e.Property.Equals(LastDateProperty))
            {
                HandleMenuItems();
            }
        }

        private void CreateNewStock(object sender, RoutedEventArgs e)
        {
            StokKartı stokKartı = StokKartı.CreateNew();
            stokKartı.ShowDialog();
            ListStocks();
        }

        private void EditStock(object sender, RoutedEventArgs e)
        {
            if (SelectedStock == null)
                return;
            StokKartı stokKartı = StokKartı.Edit(SelectedStock);
            stokKartı.ShowDialog();
            ListStocks();
        }

        private void DeleteStock(object sender, RoutedEventArgs e)
        {
            if (SelectedStock == null || MessageBoxResult.Yes != MessageBox.Show("Seçili kayıtlar silinsin mi?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information))
                return;
            DeleteStock(SelectedStock);
        }
        private void DeleteStock(Stock stock)
        {
            var stockDto = new Stock();
            Mapper.Map<Stock, Stock>(stock, stockDto);
            StockManager.Delete(stockDto);
            ListStocks();
        }
        private void RefreshStockEntries(object sender, RoutedEventArgs e)
        {
            RefreshStockEntries();
        }
        private void RefreshStocks(object sender, RoutedEventArgs e)
        {
            ListStocks();
        }
        private void CreateNewStockEntry(object sender, RoutedEventArgs e)
        {
            BelgeKartı belgeKartı = BelgeKartı.CreateNew();
            belgeKartı.ShowDialog();
            RefreshStockEntries();
        }

        private void EditStockEntry(object sender, RoutedEventArgs e)
        {
            if (SelectedStockEntry == null)
                return;
            BelgeKartı belgeKartı = BelgeKartı.Edit(SelectedStockEntry);
            belgeKartı.ShowDialog();
            RefreshStockEntries();
        }
        private void DeleteStockEntry(object sender, RoutedEventArgs e)
        {
            if (SelectedStockEntry == null || MessageBoxResult.Yes != MessageBox.Show("Seçili kayıtlar silinsin mi?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information))
                return;
            DeleteStockEntry(SelectedStockEntry);
            RefreshStockEntries();
        }

        private void DeleteStockEntry(StockEntry stockEntry)
        {
            StockEntryManager.Delete(stockEntry);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListStocks();
            ListStockEntries();
        }
        private void ListStockEntries()
        {
            if (!FirstDate.Equals(_firstDateBuffer) || !LastDate.Equals(_lastDateBuffer))
                RefreshStockEntries();
        }

        private void RefreshStockEntries()
        {
            var list = StockEntryManager.GetList(x => x.EntryDate >= FirstDate && x.EntryDate <= LastDate);
            foreach (var item in list)
            {
                InitStockEntry(item);
            }

            var batchedStockViewList = new List<StockMovementDTO>();
            batchedStockViewList = BatchedStockMovementViewReader.GetList(" where INV_DATE between '" + FirstDate.ToString("yyyy.MM.dd") + "' and '" + LastDate.ToString("yyyy.MM.dd") + "'");

            var consumedStockViewList = new List<StockMovementDTO>();
            consumedStockViewList = ConsumedStockMovementViewReader.GetList(" where INV_DATE between '" + FirstDate.ToString("yyyy.MM.dd") + "' and '" + LastDate.ToString("yyyy.MM.dd") + "'");

            ObservableCollection<StockEntry> stockEntries = new ObservableCollection<StockEntry>(list);
            stockEntryDataGrid.ItemsSource = stockEntries;

            stockEntryViewDataGrid.ItemsSource = new ObservableCollection<StockEntry>(
                list.MergeResults(
                nameof(StockEntry.StockId),
                x => x.Entry = 0,
                (x, y) => { x.Entry += y.Entry; }));

            batchedViewDataGrid.ItemsSource = new ObservableCollection<StockMovementDTO>(
                batchedStockViewList
                .MergeResults(
                nameof(StockMovementDTO.StockId),
                x => x.Quantity = 0,
                (x, y) => { x.Quantity += y.Quantity; }));

            consumedViewDataGrid.ItemsSource = new ObservableCollection<StockMovementDTO>(
                consumedStockViewList.MergeResults(
                nameof(StockMovementDTO.StockId),
                x => x.Quantity = 0,
                (x, y) => { x.Quantity += y.Quantity; }));

            _firstDateBuffer = FirstDate;
            _lastDateBuffer = LastDate;
            var obj = StockEntryViewReader.Get(" order by INV_DATE asc");
            obj?.Do(x => FirstRecordDate = obj.InvDate);

            obj = StockEntryViewReader.Get(" order by INV_DATE desc");
            obj?.Do(x => LastRecordDate = obj.InvDate);
        }

        private void ListStocks()
        {
            var list = StockManager.GetList();
            ObservableCollection<Stock> stocks = new ObservableCollection<Stock>(list);
            stockDataGrid.ItemsSource = stocks;
        }
        private void InitStockEntry(StockEntry item)
        {
            if (item == null)
                return;
            item.Stock = StockManager.Get(x => x.StockId == item.StockId);
            item.Silo = SiloManager.Get(x => x.SiloId == item.SiloId);
        }

        private void StockSelected()
        {
            ObservableCollection<Silo> silos = new ObservableCollection<Silo>();
            SiloStockQuantity = string.Empty;
            if (SelectedStock != null)
            {
                foreach (var item in SelectedStock.Silos)
                {
                    silos.Add(item);
                }
            }
            siloDataGrid.ItemsSource = silos;
            if (silos.Count > 0)
            {
                double d = 0;
                foreach (var item in silos)
                {
                    d += item.Balance;
                }
                SiloStockQuantity = d.ToString();
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditStock(null, null);
        }

        private void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            ListStockEntries();
        }

        private void DatePicker_KeyDown(object sender, KeyEventArgs e)
        {
            ListStockEntries();
        }

        private void BugünClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate();
            LastDate = DateTime.Now.MakeFirstDate();
            ListStockEntries();
        }
        private void BuHaftaClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate().StartOfWeek(DayOfWeek.Monday);
            LastDate = DateTime.Now.MakeFirstDate().EndOfWeek(DayOfWeek.Monday);
            ListStockEntries();
        }
        private void BuAyClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate().StartOfMonth();
            LastDate = DateTime.Now.MakeFirstDate().EndOfMonth();
            ListStockEntries();
        }
        private void BuYılClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate().StartOfYear();
            LastDate = DateTime.Now.MakeFirstDate().EndOfYear();
            ListStockEntries();
        }
        private void TümüClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = FirstRecordDate.MakeFirstDate();
            LastDate = DateTime.Now.MakeFirstDate().EndOfWeek(DayOfWeek.Monday);
            ListStockEntries();
        }
        private void HandleMenuItems()
        {
            Bugün = FirstDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate()) && LastDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate());
            BuHafta = FirstDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().StartOfWeek(DayOfWeek.Monday)) && LastDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().EndOfWeek(DayOfWeek.Monday));
            BuAy = FirstDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().StartOfMonth()) && LastDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().EndOfMonth());
            BuYıl = FirstDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().StartOfYear()) && LastDate.MakeFirstDate().IsSame(DateTime.Now.MakeFirstDate().EndOfYear());
            Tümü = (FirstDate.MakeFirstDate().IsEarlierThan(FirstRecordDate.MakeFirstDate()) || FirstDate.MakeFirstDate().IsSame(FirstRecordDate.MakeFirstDate())) && (LastDate.MakeFirstDate().IsLaterThan(LastRecordDate.MakeFirstDate()) || LastDate.MakeFirstDate().IsSame(LastRecordDate.MakeFirstDate()));
        }

        private void DataGridRow_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            EditStockEntry(null, null);
        }
    }
}
