using Promax.Business;
using Promax.Core;
using Promax.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Promax.UI.Windows
{
    /// <summary>
    /// Belge.xaml etkileşim mantığı
    /// </summary>
    public partial class BelgeKartı : Window
    {
        public static readonly DependencyProperty StocksProperty =
       DependencyProperty.Register(nameof(Stocks), typeof(ObservableCollection<Stock>), typeof(BelgeKartı));
        public static readonly DependencyProperty SilosProperty =
      DependencyProperty.Register(nameof(Silos), typeof(ObservableCollection<Silo>), typeof(BelgeKartı));

        public static BelgeKartı CreateNew()
        {
            return new BelgeKartı()
            {
                StockEntry = new StockEntry()
                {
                    EntryDate = DateTime.Now,
                    EntryTime = DateTime.Now
                }
            };
        }
        public static BelgeKartı Edit(StockEntry stockEntry)
        {
            var a = new BelgeKartı();
            a.StockEntry = a.Mapper.Map<StockEntry>(a.Mapper.Map<StockEntryDTO>(stockEntry));
            a.oldStockEntry = stockEntry;
            a.Editing = true;
            return a;
        }
        private StockEntry oldStockEntry;
        private bool Editing { get; set; }
        public object PropertyChanged { get; private set; }
        public IBeeMapper Mapper { get => Infrastructure.Main.Mapper; }
        public IStockManager StockManager { get => Infrastructure.Main.StockManager; }
        public ISiloManager SiloManager { get => Infrastructure.Main.SiloManager; }
        public IStockEntryManager StockEntryManager { get => Infrastructure.Main.StockEntryManager; }
        public StockEntry StockEntry { get; set; }
        public ObservableCollection<Stock> Stocks
        {
            get => (ObservableCollection<Stock>)GetValue(StocksProperty); set
            {
                var oldValue = Stocks;
                var newValue = value;
                SetValue(StocksProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(StocksProperty, oldValue, newValue));
            }
        }
        public ObservableCollection<Silo> Silos
        {
            get => (ObservableCollection<Silo>)GetValue(SilosProperty); set
            {
                var oldValue = Silos;
                var newValue = value;
                SetValue(SilosProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(SilosProperty, oldValue, newValue));
            }
        }
        private BelgeKartı()
        {
            InitializeComponent();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StockSelected();
        }

        private void TamamButon_Click(object sender, RoutedEventArgs e)
        {
            Infrastructure.Main.HandleExceptions(() =>
            {
                if (Editing)
                {
                    StockEntryManager.Update(StockEntry, oldStockEntry);
                }
                else
                {
                    StockEntryManager.Add(StockEntry);
                }
                this.Close();
            });
        }

        private void İptalButon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Stocks = new ObservableCollection<Stock>(StockManager.GetList());
        }

        private void StockSelected()
        {
            List<Silo> list = new List<Silo>();
            if (StockEntry != null)
            {
                StockEntry.Stock = Stocks.FirstOrDefault(x => x.StockId == StockEntry.StockId);
                if (StockEntry.Stock != null)
                {
                    var abq = StockEntry.Stock.Silos.FirstOrDefault(x => x.SiloId == StockEntry.SiloId);
                    if (abq == null)
                        StockEntry.Silo = null;
                    list.AddRange(StockEntry.Stock.Silos);
                }
            }
            Silos = new ObservableCollection<Silo>(list);
        }
    }
}
