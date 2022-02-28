using Promax.Business.Abstract;
using Promax.Core;
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
using System.Windows.Shapes;

namespace Promax.UI.Windows
{
    /// <summary>
    /// ÜretimKartı.xaml etkileşim mantığı
    /// </summary>
    public partial class ÜretimKartı : Window
    {
        public static readonly DependencyProperty ClientsProperty = DependencyProperty.Register(
            nameof(Clients), typeof(ObservableCollection<Client>), typeof(ÜretimKartı));
        public static readonly DependencyProperty SitesProperty = DependencyProperty.Register(
            nameof(Sites), typeof(ObservableCollection<Site>), typeof(ÜretimKartı));
        public static readonly DependencyProperty RecipesProperty = DependencyProperty.Register(
            nameof(Recipes), typeof(ObservableCollection<Recipe>), typeof(ÜretimKartı));
        public static readonly DependencyProperty ServiceCategoriesProperty = DependencyProperty.Register(
            nameof(ServiceCategories), typeof(ObservableCollection<ServiceCategoryDTO>), typeof(ÜretimKartı));
        public static readonly DependencyProperty CentralServicesProperty = DependencyProperty.Register(
            nameof(CentralServices), typeof(ObservableCollection<Service>), typeof(ÜretimKartı));
        public static readonly DependencyProperty MixerServicesProperty = DependencyProperty.Register(
            nameof(MixerServices), typeof(ObservableCollection<Service>), typeof(ÜretimKartı));
        public static readonly DependencyProperty PumpServicesProperty = DependencyProperty.Register(
            nameof(PumpServices), typeof(ObservableCollection<Service>), typeof(ÜretimKartı));
        public static readonly DependencyProperty DriversProperty = DependencyProperty.Register(
            nameof(Drivers), typeof(ObservableCollection<DriverDTO>), typeof(ÜretimKartı));
        public ObservableCollection<Client> Clients
        {
            get => (ObservableCollection<Client>)GetValue(ClientsProperty); set
            {
                var oldValue = Clients;
                var newValue = value;
                SetValue(ClientsProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(ClientsProperty, oldValue, newValue));
            }
        }
        public ObservableCollection<Site> Sites
        {
            get => (ObservableCollection<Site>)GetValue(SitesProperty); set
            {
                var oldValue = Sites;
                var newValue = value;
                SetValue(SitesProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(SitesProperty, oldValue, newValue));
            }
        }
        public ObservableCollection<Recipe> Recipes
        {
            get => (ObservableCollection<Recipe>)GetValue(RecipesProperty); set
            {
                var oldValue = Recipes;
                var newValue = value;
                SetValue(RecipesProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(RecipesProperty, oldValue, newValue));
            }
        }
        public ObservableCollection<ServiceCategoryDTO> ServiceCategories
        {
            get => (ObservableCollection<ServiceCategoryDTO>)GetValue(ServiceCategoriesProperty); set
            {
                var oldValue = ServiceCategories;
                var newValue = value;
                SetValue(ServiceCategoriesProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(ServiceCategoriesProperty, oldValue, newValue));
            }
        }
        public ObservableCollection<Service> CentralServices
        {
            get => (ObservableCollection<Service>)GetValue(CentralServicesProperty); set
            {
                var oldValue = CentralServices;
                var newValue = value;
                SetValue(CentralServicesProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(CentralServicesProperty, oldValue, newValue));
            }
        }
        public ObservableCollection<Service> MixerServices
        {
            get => (ObservableCollection<Service>)GetValue(MixerServicesProperty); set
            {
                var oldValue = MixerServices;
                var newValue = value;
                SetValue(MixerServicesProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(MixerServicesProperty, oldValue, newValue));
            }
        }
        public ObservableCollection<Service> PumpServices
        {
            get => (ObservableCollection<Service>)GetValue(PumpServicesProperty); set
            {
                var oldValue = PumpServices;
                var newValue = value;
                SetValue(PumpServicesProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(PumpServicesProperty, oldValue, newValue));
            }
        }
        public ObservableCollection<DriverDTO> Drivers
        {
            get => (ObservableCollection<DriverDTO>)GetValue(DriversProperty); set
            {
                var oldValue = Drivers;
                var newValue = value;
                SetValue(DriversProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(DriversProperty, oldValue, newValue));
            }
        }
        #region Managers
        private IComplexClientManager ClientManager { get => Infrastructure.Main.GetClientManager(); }
        private IComplexRecipeManager RecipeManager { get => Infrastructure.Main.GetRecipeManager(); }
        private IComplexServiceCategoryManager ServiceCategoryManager { get => Infrastructure.Main.GetServiceCategoryManager(); }
        private IComplexServiceManager ServiceManager { get => Infrastructure.Main.GetServiceManager(); }
        private IComplexDriverManager DriverManager { get => Infrastructure.Main.GetDriverManager(); }
        #endregion
        private bool CentralServiceIsEnabledResource { get { return (bool)Resources["CentralServiceIsEnabled"]; } set { Resources["CentralServiceIsEnabled"] = value; } }
        private bool MixerServiceIsEnabledResource { get { return (bool)Resources["MixerServiceIsEnabled"]; } set { Resources["MixerServiceIsEnabled"] = value; } }
        private bool PumpServiceIsEnabledResource { get { return (bool)Resources["PumpServiceIsEnabled"]; } set { Resources["PumpServiceIsEnabled"] = value; } }

        public static ÜretimKartı CreateNew()
        {
            return new ÜretimKartı() { Product = new Product() };
        }
        public static ÜretimKartı Edit(Product product)
        {
            var a = new ÜretimKartı();
            a.Product = a.Mapper.Map<Product>(a.Mapper.Map<ProductDTO>(product));
            a.oldProduct = product;
            a.Editing = true;
            return a;
        }
        private bool Editing { get; set; }
        private Product oldProduct;
        public Product Product { get; set; }
        public IBeeMapper Mapper { get => Infrastructure.Main.GetMapper(); }
        private IComplexProductDalManager ProductManager { get => Infrastructure.Main.GetProductDalManager(); }
        private ÜretimKartı()
        {
            InitializeComponent();
        }

        private void TamamButon_Click(object sender, RoutedEventArgs e)
        {
            Infrastructure.Main.HandleExceptions(() =>
            {
                if (Editing)
                {
                    ProductManager.Update(Product, oldProduct);
                }
                else
                {
                    ProductManager.Add(Product);
                }
            });
        }

        private void İptalButon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClientSelected(object sender, SelectionChangedEventArgs e)
        {
            List<Site> list = new List<Site>();
            if (Product != null)
            {
                Product.Client = Clients.FirstOrDefault(x => x.ClientId == Product.ClientId);
                Product.Client.Do(x => list.AddRange(x.ActiveSites));
            }
            Sites = new ObservableCollection<Site>(list);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Clients = new ObservableCollection<Client>(ClientManager.GetList(x => !x.IsHidden.GetBool()));
            Recipes = new ObservableCollection<Recipe>(RecipeManager.GetList(x => !x.IsHidden.GetBool()));
            ServiceCategories = new ObservableCollection<ServiceCategoryDTO>(ServiceCategoryManager.GetList());
            CentralServices = new ObservableCollection<Service>(ServiceManager.GetList(x => !x.IsHidden.GetBool() && x.ServiceCatNum == 3));
            MixerServices = new ObservableCollection<Service>(ServiceManager.GetList(x => !x.IsHidden.GetBool() && x.ServiceCatNum == 1));
            PumpServices = new ObservableCollection<Service>(ServiceManager.GetList(x => !x.IsHidden.GetBool() && x.ServiceCatNum == 2));
            Drivers = new ObservableCollection<DriverDTO>(DriverManager.GetList(x => !x.IsHidden.GetBool()));
            ServiceCategorySelected(null, null);
        }

        private void ServiceCategorySelected(object sender, SelectionChangedEventArgs e)
        {
            Product.DoIfElse(x => x.ServiceCatNum == 1, x =>
            {
                CentralServiceIsEnabledResource = false;
                MixerServiceIsEnabledResource = true;
                PumpServiceIsEnabledResource = false;
            }, x => x.DoIfElse(y => y.ServiceCatNum == 2, y =>
            {
                CentralServiceIsEnabledResource = false;
                MixerServiceIsEnabledResource = true;
                PumpServiceIsEnabledResource = true;
            }, y => y.DoIf(z => z.ServiceCatNum == 3, z =>
            {
                CentralServiceIsEnabledResource = true;
                MixerServiceIsEnabledResource = false;
                PumpServiceIsEnabledResource = false;
            })));
            CentralServiceIsEnabledResource.DoIf(x => !x, x => Product.Do(y =>
            {
                Product.SelfDriverId = 0;
                Product.SelfServiceId = 0;
            }));
            MixerServiceIsEnabledResource.DoIf(x => !x, x => Product.Do(y =>
            {
                Product.MixerDriverId = 0;
                Product.MixerServiceId = 0;
            }));
            PumpServiceIsEnabledResource.DoIf(x => !x, x => Product.Do(y =>
            {
                Product.PumpDriverId = 0;
                Product.PumpServiceId = 0;
            }));
        }
    }
}
