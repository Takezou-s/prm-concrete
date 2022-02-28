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

namespace Promax.UI.CustomTools
{
    /// <summary>
    /// TarihButon.xaml etkileşim mantığı
    /// </summary>
    public partial class TarihButon : Button, INotifyPropertyChanged
    {
        public ObservableCollection<CDrawingFlag> ComboBoxItems { set; get; }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("comboboxIcon", typeof(ImageSource), typeof(TarihButon));

        public event PropertyChangedEventHandler PropertyChanged;

        public TarihButon()
        {
            InitializeComponent();
            LoadOperations();
        }
        private void LoadOperations()
        {
            List<string> imagePath = new List<string>
            {
                @"/Promax.UI;component/images\greenloader.png",
                @"/Promax.UI;component/images\grid.png",
                @"/Promax.UI;component/images\hydrolic.png",
                @"/Promax.UI;component/images\lock.png",
                @"/Promax.UI;component/images\menu.png",
                @"/Promax.UI;component/images\plus_button.png",
            };

            List<string> flag = new List<string>
            {
                "Ellipse",
                "Line",
                "Rectangle",
                "Text",
                "Angle",
                "Edit"
            };

            ComboBoxItems = new ObservableCollection<CDrawingFlag>();
            for (int i = 0; i < imagePath.Count; i++)
            {
                ComboBoxItems.Add(new CDrawingFlag() { comboboxImage = imagePath[i], comboboxName = flag[i] });
            }
            comboBox.ItemsSource = ComboBoxItems;
            comboBox.SelectedIndex = 0;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            comboBox.IsDropDownOpen = true;
        }
        public ImageSource comboboxIcon
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceProperty);
            }
            set
            {
                this.SetValue(ImageSourceProperty, value);
                OnPropertyChanged(nameof(comboboxIcon));
            }
        }


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
    public class CDrawingFlag
    {
        public string comboboxImage { set; get; }
        public string comboboxName { set; get; }
    }

}
