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
    /// Kalibrasyon.xaml etkileşim mantığı
    /// </summary>
    public partial class KalibrasyonKartı : Window
    {

        public KalibrasyonKartı()
        {
            InitializeComponent();
            geribtn.Visibility = Visibility.Hidden;
        }

        private int sayfa_no = 1;
        private void İleri_Click(object sender, RoutedEventArgs e)
        {
            if (sayfa_no == 3)
            {
                this.Close();
            }
            if (sayfa_no == 2)
            {
                ileribtn.Content = "Bitir";
                CihazTextBlock.Visibility = Visibility.Visible;
                CihazTextBlock.Text = "Kalibrasyon Noktası";
                AciklamaTextBlock.Text = "Kantara değeri bilinen bir ağırlık yerleştirin. Değeri metin kutusuna yazın ve kantar sarısıntısız ise bitire tıklayın. ";
                TextBox textbox = new TextBox();
                textbox.Margin = new Thickness(5, 5, 5, 5);
                textbox.Padding = new Thickness(2, 2, 0, 0);
                Grid.SetRow(textbox, 1);
                Grid.SetColumn(textbox, 1);
                Grid1.Children.Add(textbox);

                geribtn.Visibility = Visibility.Hidden;
                sayfa_no = 3;
            }
            if (sayfa_no == 1 && ComboBox1.SelectedItem != null)
            {
                sayfa_no = 2;
                ComboBox1.Visibility = Visibility.Hidden;
                CihazTextBlock.Visibility = Visibility.Hidden;
                AciklamaTextBlock.Text = "Kantar Boş ve Sarsıntısız İse İleriyi Tıklayın.";
                geribtn.Visibility = Visibility.Visible;
            }

        }

        private void Geri_Click(object sender, RoutedEventArgs e)
        {
            sayfa_no = 1;
            ComboBox1.Visibility = Visibility.Visible;
            CihazTextBlock.Visibility = Visibility.Visible;
            AciklamaTextBlock.Text = "Kalibre Etmek İstediğiniz Cihazı Seçin.";
            geribtn.Visibility = Visibility.Hidden;
        }

    }
}
