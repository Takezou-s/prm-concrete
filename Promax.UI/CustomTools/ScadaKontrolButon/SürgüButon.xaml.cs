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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Promax.UI.CustomTools.ScadaKontrolButon
{
    /// <summary>
    /// SürgüButon.xaml etkileşim mantığı
    /// </summary>
    public partial class SürgüButon : UserControl
    {
        public SürgüButon()
        {
            InitializeComponent();
        }
        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            slValue.Value += slValue.SmallChange;
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            slValue.Value -= slValue.SmallChange;
        }
    }
}
