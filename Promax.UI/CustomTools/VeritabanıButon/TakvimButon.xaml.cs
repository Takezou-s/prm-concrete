using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Promax.UI.CustomTools.VeritabanıButon
{
    /// <summary>
    /// TakvimButon.xaml etkileşim mantığı
    /// </summary>
    public partial class TakvimButon : Menu
    {
        //public ObservableCollection<CDrawingFlag> ComboBoxItems { set; get; }

        public TakvimButon()
        {
            InitializeComponent();
            //LoadOperations();
        }
        //    private void LoadOperations()
        //    {
        //        List<string> flag = new List<string>
        //        {
        //            "Bugün",
        //            "Bu Hafta",
        //            "Bu Ay",
        //            "Bu Yıl",
        //            "Tüm Zamanlar",
        //        };

        //        ComboBoxItems = new ObservableCollection<CDrawingFlag>();
        //        for (int i = 0; i < flag.Count; i++)
        //        {
        //            ComboBoxItems.Add(new CDrawingFlag() {comboBoxName = flag[i] });
        //        }
        //        comboBox.ItemsSource = ComboBoxItems;
        //        comboBox.SelectedIndex = 0;
        //    }
        //    //private void Button_Click(object sender, RoutedEventArgs e)
        //    //{
        //    //    comboBox.IsDropDownOpen = true;
        //    //}
    }
    //public class CDrawingFlag
    //{
    //    public string comboBoxName { set; get; }
    //}
}
