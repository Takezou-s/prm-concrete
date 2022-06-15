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

namespace Promax.UI
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
        }
        private void MainTabControlAyarlarButon_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Kalibrasyon_Click(object sender, RoutedEventArgs e)
        {
            Windows.KalibrasyonKartı kalibrasyonKartı = new Windows.KalibrasyonKartı();
            kalibrasyonKartı.Show();
        }

        private void Firma_Bilgileri_Click(object sender, RoutedEventArgs e)
        {
            Windows.FirmaKartı firmaKartı = new Windows.FirmaKartı();
            firmaKartı.Show();
        }

        private void Kullanici_Degistir_Click(object sender, RoutedEventArgs e)
        {
            Windows.LoginKartı loginKartı = new Windows.LoginKartı();
            loginKartı.Show();
        }

        private void Kullanici_Paneli_Click(object sender, RoutedEventArgs e)
        {
            Windows.KullanıcıPaneliKartı kullanıcıPaneliKartı = new Windows.KullanıcıPaneliKartı();
            kullanıcıPaneliKartı.Show();
        }

        private void AnaSayfa_Loaded(object sender, RoutedEventArgs e)
        {
            ScadaButon.ShowPage();
        }
    }
}
