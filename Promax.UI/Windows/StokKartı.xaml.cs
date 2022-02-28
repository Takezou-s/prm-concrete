using Promax.Business.Abstract;
using Promax.Business.Mappers;
using Promax.Core;
using Promax.Entities;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

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
        public IComplexStockManager StockManager { get => Infrastructure.Main.GetStockManager(); }
        public IBeeMapper Mapper { get => Infrastructure.Main.GetMapper(); }
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
                    StockManager.Update(Stock, oldStock);
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
