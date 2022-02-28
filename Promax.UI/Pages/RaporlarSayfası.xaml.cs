using Promax.Business.Abstract;
using Promax.Core;
using Promax.DataAccess.Abstract;
using Promax.DataAccess.Abstract.LowLevel;
using Promax.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Promax.UI
{
    /// <summary>
    /// RaporlarSayfası.xaml etkileşim mantığı
    /// </summary>
    public partial class RaporlarSayfası : Page
    {
        #region DependencyProperties
        public static readonly DependencyProperty FirstDateProperty =
         DependencyProperty.Register(nameof(FirstDate), typeof(DateTime), typeof(RaporlarSayfası));

        public static readonly DependencyProperty LastDateProperty =
    DependencyProperty.Register(nameof(LastDate), typeof(DateTime), typeof(RaporlarSayfası));
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
        #endregion
        public DateTime FirstRecordDate { get; set; } = new DateTime(1, 1, 1);
        public DateTime LastRecordDate { get; set; } = DateTime.Now;
        private IProductWayBillViewDal ProductWayBillViewDal { get => Infrastructure.Main.GetProductWayBillViewDal(); }
        private ISentViewReader SentViewReader { get => Infrastructure.Main.GetSentViewReader(); }
        private IComplexClientManager ClientManager { get => Infrastructure.Main.GetClientManager(); }
        private IComplexProductDalManager ProductManager { get => Infrastructure.Main.GetProductDalManager(); }
        public RaporlarSayfası()
        {
            InitializeComponent();
        }
        private void Uretim_buton_Click(object sender, RoutedEventArgs e)
        {
            if (UretimRaporlarıPanel.IsVisible == false)
            {
                UretimRaporlarıPanel.Visibility = Visibility.Visible;
                Uretim_btnımage.Source = new BitmapImage(new Uri("pack://application:,,,/Promax.UI;component/Pictures/Icons/uparrow.png"));
            }
            else
            {
                UretimRaporlarıPanel.Visibility = Visibility.Hidden;
                Uretim_btnımage.Source = new BitmapImage(new Uri("pack://application:,,,/Promax.UI;component/Pictures/Icons/downarrow.png"));
            }
        }

        private void Tuketim_buton_Click(object sender, RoutedEventArgs e)
        {
            if (TuketimRaporlarıPanel.IsVisible == false)
            {
                TuketimRaporlarıPanel.Visibility = Visibility.Visible;
                Tuketim_BtnImage.Source = new BitmapImage(new Uri("pack://application:,,,/Promax.UI;component/Pictures/Icons/uparrow.png"));
            }
            else
            {

                TuketimRaporlarıPanel.Visibility = Visibility.Hidden;
                Tuketim_BtnImage.Source = new BitmapImage(new Uri("pack://application:,,,/Promax.UI;component/Pictures/Icons/downarrow.png"));
            }
        }

        private void UretimRaporButon1_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            dynamic TextB = b.Content;
            string icbaslık;
            icbaslık = TextB.Text;
            Başlıh.Text = "ÜRETİM RAPORU" + "(" + icbaslık + ")";
            //HeaderA = "ÜRETİM RAPORU";
            //HeaderB = TextB.Text;
        }

        private void TuketimRaporButon_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            dynamic TextB = b.Content;
            string icbaslık;
            icbaslık = TextB.Text;
            Başlıh.Text = "TÜKETİM RAPORU" + "(" + icbaslık + ")";
        }
        private void ListActive()
        {

        }
        private void ListMikserliServis()
        {
            var list = ProductManager.GetList(x => x.ProductDate >= FirstDate && x.ProductDate <= LastDate);

        }
        private void ListMüşteriReçete()
        {
            var list = SentViewReader.GetList(" where PRODUCT_DATE between '" + FirstDate.ToString("yyyy-MM-dd") + "' and '" + LastDate.ToString("yyyy-MM-dd") + "'");
            var dic = new Dictionary<int, Dictionary<string, ClientRecipeView>>();
            foreach (var sentView in list)
            {
                dic.DoIf(x => !x.ContainsKey(sentView.ClientId), x => x.Add(sentView.ClientId, new Dictionary<string, ClientRecipeView>()));
                dic[sentView.ClientId].DoIf(x => !x.ContainsKey(sentView.RecipeName), x => x.Add(sentView.RecipeName, new ClientRecipeView() { ClientId = sentView.ClientId, RecipeName = sentView.RecipeName }));
                var obj = dic[sentView.ClientId][sentView.RecipeName];
                switch (sentView.Tour)
                {
                    case 0:
                        obj.CentralServiceAmount += sentView.Delivered;
                        break;
                    case 1:
                        obj.MixerServiceAmount += sentView.Delivered;
                        break;
                    case 2:
                        obj.PumpServiceAmount += sentView.Delivered;
                        break;
                    default:
                        break;
                }
                obj.Shipped += sentView.Shipped;
                obj.Delivered += sentView.Delivered;
            }
            var clientRecipeViewList = new List<ClientRecipeView>();
            foreach (var item in dic)
            {
                foreach (var item1 in item.Value)
                {
                    clientRecipeViewList.Add(item1.Value);
                }
            }
            clientRecipeViewList.ForEach(x =>
            {
                ClientManager.Get(y => y.ClientId == x.ClientId).Do(z => x.ClientName = z.ClientName);
            });
            müşteriReçeteDataGrid.ItemsSource = new ObservableCollection<ClientRecipeView>(clientRecipeViewList);
            müşteriReçeteÖzetDataGrid.ItemsSource = new ObservableCollection<ClientRecipeView>(clientRecipeViewList);
        }
        private void ListMüşteriŞantiyeReçete()
        {
            var list = SentViewReader.GetList(" where PRODUCT_DATE between '" + FirstDate.ToString("yyyy-MM-dd") + "' and '" + LastDate.ToString("yyyy-MM-dd") + "'");
            var dic = new Dictionary<int, Dictionary<string, Dictionary<string, ClientRecipeView>>>();
            foreach (var sentView in list)
            {
                dic.DoIf(x => !x.ContainsKey(sentView.ClientId), x => x.Add(sentView.ClientId, new Dictionary<string, Dictionary<string, ClientRecipeView>>()));
                dic[sentView.ClientId].DoIf(x => !x.ContainsKey(sentView.SiteName), x => x.Add(sentView.SiteName, new Dictionary<string, ClientRecipeView>()));
                dic[sentView.ClientId][sentView.SiteName].DoIf(x => !x.ContainsKey(sentView.RecipeName), x => x.Add(sentView.RecipeName, new ClientRecipeView() { ClientId = sentView.ClientId, RecipeName = sentView.RecipeName }));
                var obj = dic[sentView.ClientId][sentView.SiteName][sentView.RecipeName];
                switch (sentView.Tour)
                {
                    case 0:
                        obj.CentralServiceAmount += sentView.Delivered;
                        break;
                    case 1:
                        obj.MixerServiceAmount += sentView.Delivered;
                        break;
                    case 2:
                        obj.PumpServiceAmount += sentView.Delivered;
                        break;
                    default:
                        break;
                }
                obj.Shipped += sentView.Shipped;
                obj.Delivered += sentView.Delivered;
            }
            var clientRecipeViewList = new List<ClientRecipeView>();
            foreach (var item in dic)
            {
                foreach (var item1 in item.Value)
                {
                    foreach (var item2 in item1.Value)
                    {
                        clientRecipeViewList.Add(item2.Value);
                    }
                }
            }
            clientRecipeViewList.ForEach(x =>
            {
                ClientManager.Get(y => y.ClientId == x.ClientId).Do(z => x.ClientName = z.ClientName);
            });
            müşteriŞantiyeReçeteDataGrid.ItemsSource = new ObservableCollection<ClientRecipeView>(clientRecipeViewList);
        }
        private void ListMüşteriÜretim()
        {
            müşteriÜretimDataGrid.ItemsSource = new ObservableCollection<ProductWayBillViewDTO>(ProductWayBillViewDal.GetList(x => x.ProductDate >= FirstDate && x.ProductDate <= LastDate));
        }

        private void DateTimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ListActive();
        }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Equals(FirstDateProperty) || e.Property.Equals(LastDateProperty))
            {
                HandleMenuItems();
            }
        }
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
            ListActive();
        }
        private void BuHaftaClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate().StartOfWeek(DayOfWeek.Monday);
            LastDate = DateTime.Now.MakeFirstDate().EndOfWeek(DayOfWeek.Monday);
            ListActive();
        }
        private void BuAyClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate().StartOfMonth();
            LastDate = DateTime.Now.MakeFirstDate().EndOfMonth();
            ListActive();
        }
        private void BuYılClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = DateTime.Now.MakeFirstDate().StartOfYear();
            LastDate = DateTime.Now.MakeFirstDate().EndOfYear();
            ListActive();
        }
        private void TümüClicked(object sender, RoutedEventArgs e)
        {
            FirstDate = FirstRecordDate.MakeFirstDate();
            LastDate = DateTime.Now.MakeFirstDate().EndOfWeek(DayOfWeek.Monday);
            ListActive();
        }
    }
}
