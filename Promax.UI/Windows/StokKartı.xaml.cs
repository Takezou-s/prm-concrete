using Promax.Business;
using Promax.Core;
using Promax.Entities;
using System.Windows;

namespace Promax.UI.Windows
{
    /// <summary>
    /// StokKartı.xaml etkileşim mantığı
    /// </summary>
    public partial class StokKartı : Window
    {
        public static StokKartı CreateNew()
        {
            return new StokKartı() { Stock = new Stock() };
        }
        public static StokKartı Edit(Stock stock)
        {
            var a = new StokKartı();
            a.Stock = a.Mapper.Map<Stock>(a.Mapper.Map<StockDTO>(stock));
            a.oldStock = stock;
            a.Editing = true;
            return a;
        }
        private Stock oldStock;
        private bool Editing { get; set; }
        public Stock Stock { get; set; }
        public IStockManager StockManager { get => Infrastructure.Main.StockManager; }
        public IBeeMapper Mapper { get => Infrastructure.Main.Mapper; }
        private StokKartı()
        {
            InitializeComponent();
        }

        private void TamamButon_Click(object sender, RoutedEventArgs e)
        {
            Infrastructure.Main.HandleExceptions(() =>
            {
                if (Editing)
                {
                    StockManager.Update(Stock);
                }
                else
                {
                    StockManager.Add(Stock);
                }
                this.Close();
            });
        }

        private void İptalButon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
