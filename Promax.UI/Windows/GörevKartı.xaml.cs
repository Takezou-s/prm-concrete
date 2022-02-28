using Promax.Business;
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
    /// GörevKartı.xaml etkileşim mantığı
    /// </summary>
    public partial class GörevKartı : Window
    {
        public static readonly DependencyProperty IsSelectingMixerServiceProperty = DependencyProperty.Register(
            nameof(IsSelectingMixerService), typeof(bool), typeof(GörevKartı));
        public static readonly DependencyProperty ValidationErrorProperty = DependencyProperty.Register(
           nameof(ValidationError), typeof(bool), typeof(GörevKartı));
        #region ObservableCollections
        public static readonly DependencyProperty Gate1SelectedProperty = DependencyProperty.Register(
           nameof(Gate1Selected), typeof(bool), typeof(GörevKartı));
        public static readonly DependencyProperty Gate2SelectedProperty = DependencyProperty.Register(
            nameof(Gate2Selected), typeof(bool), typeof(GörevKartı));
        public static readonly DependencyProperty RecipeContentsProperty = DependencyProperty.Register(
            nameof(RecipeContents), typeof(ObservableCollection<RecipeContent>), typeof(GörevKartı));
        public static readonly DependencyProperty ClientsProperty = DependencyProperty.Register(
            nameof(Clients), typeof(ObservableCollection<Client>), typeof(GörevKartı));
        public static readonly DependencyProperty SitesProperty = DependencyProperty.Register(
            nameof(Sites), typeof(ObservableCollection<Site>), typeof(GörevKartı));
        public static readonly DependencyProperty RecipesProperty = DependencyProperty.Register(
            nameof(Recipes), typeof(ObservableCollection<Recipe>), typeof(GörevKartı));
        public static readonly DependencyProperty ServiceCategoriesProperty = DependencyProperty.Register(
            nameof(ServiceCategories), typeof(ObservableCollection<ServiceCategoryDTO>), typeof(GörevKartı));
        public static readonly DependencyProperty CentralServicesProperty = DependencyProperty.Register(
            nameof(CentralServices), typeof(ObservableCollection<Service>), typeof(GörevKartı));
        public static readonly DependencyProperty MixerServicesProperty = DependencyProperty.Register(
            nameof(MixerServices), typeof(ObservableCollection<Service>), typeof(GörevKartı));
        public static readonly DependencyProperty PumpServicesProperty = DependencyProperty.Register(
            nameof(PumpServices), typeof(ObservableCollection<Service>), typeof(GörevKartı));
        public static readonly DependencyProperty DriversProperty = DependencyProperty.Register(
            nameof(Drivers), typeof(ObservableCollection<DriverDTO>), typeof(GörevKartı));
        public static readonly DependencyProperty RecipeNameProperty = DependencyProperty.Register(
            nameof(RecipeName), typeof(string), typeof(GörevKartı));
        public static readonly DependencyProperty ErrorDictionaryProperty = DependencyProperty.Register(
            nameof(ErrorDictionary), typeof(Dictionary<string, bool>), typeof(GörevKartı));
        public ObservableCollection<RecipeContent> RecipeContents
        {
            get => (ObservableCollection<RecipeContent>)GetValue(RecipeContentsProperty); set
            {
                var oldValue = RecipeContents;
                var newValue = value;
                SetValue(RecipeContentsProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(RecipeContentsProperty, oldValue, newValue));
            }
        }
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
        #endregion
        #region Managers
        private IComplexClientManager ClientManager { get => Infrastructure.Main.GetClientManager(); }
        private IComplexRecipeManager RecipeManager { get => Infrastructure.Main.GetRecipeManager(); }
        private IComplexServiceCategoryManager ServiceCategoryManager { get => Infrastructure.Main.GetServiceCategoryManager(); }
        private IComplexServiceManager ServiceManager { get => Infrastructure.Main.GetServiceManager(); }
        private IComplexDriverManager DriverManager { get => Infrastructure.Main.GetDriverManager(); }
        private IComplexMixerManager MixerManager { get => Infrastructure.Main.GetMixerManager(); }
        private IComplexProductManager ProductManager { get => Infrastructure.Main.GetProductManager(); }
        private IBeeMapper Mapper { get => Infrastructure.Main.GetMapper(); }
        #endregion
        public static GörevKartı CreateNew(Order order)
        {
            return new GörevKartı()
            {
                Product = new Product()
                {
                    Client = order.Client,
                    Site = order.Site,
                    Recipe = order.Recipe,
                    ServiceCategory = order.ServiceCategory,
                    Order = order
                }
            };
        }
        public static GörevKartı Edit(Product product)
        {
            var a = new GörevKartı();
            a.Product = new Product();
            a.Mapper.Map<Product, Product>(product, a.Product);
            a.oldProduct = product;
            a.Editing = true;
            return a;
        }
        private GörevKartı()
        {
            InitializeComponent();
            ErrorDictionary = new Dictionary<string, bool>();
        }
        public Dictionary<string, bool> ErrorDictionary
        {
            get
            {
                return (Dictionary<string, bool>)GetValue(ErrorDictionaryProperty);
            }
            set
            {
                var old = ErrorDictionary;
                SetValue(ErrorDictionaryProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(ErrorDictionaryProperty, old, value));
            }
        }
        public bool ValidationError
        {
            get
            {
                return (bool)GetValue(ValidationErrorProperty);
            }
            set
            {
                var old = ValidationError;
                SetValue(ValidationErrorProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(ValidationErrorProperty, old, value));
            }
        }
        public bool Gate1Selected
        {
            get => (bool)GetValue(Gate1SelectedProperty); set
            {
                var oldValue = Gate1Selected;
                var newValue = value;
                SetValue(Gate1SelectedProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(Gate1SelectedProperty, oldValue, newValue));
                Product.DoIf(x => value, x => x.GateNum = 11);
            }
        }
        public bool Gate2Selected
        {
            get => (bool)GetValue(Gate2SelectedProperty); set
            {
                var oldValue = Gate2Selected;
                var newValue = value;
                SetValue(Gate2SelectedProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(Gate2SelectedProperty, oldValue, newValue));
                Product.DoIf(x => value, x => x.GateNum = 12);
            }
        }
        public string RecipeName
        {
            get => (string)GetValue(RecipeNameProperty); set
            {
                var oldValue = RecipeName;
                var newValue = value;
                SetValue(RecipeNameProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(RecipeNameProperty, oldValue, newValue));
            }
        }
        public bool IsSelectingMixerService
        {
            get => (bool)GetValue(IsSelectingMixerServiceProperty); set
            {
                var oldValue = IsSelectingMixerService;
                var newValue = value;
                SetValue(IsSelectingMixerServiceProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(IsSelectingMixerServiceProperty, oldValue, newValue));
            }
        }
        public Service SelectedMixerService
        {
            get => _selectedMixerService; set
            {
                _selectedMixerService = value;
                IsSelectingMixerService = false;
                //if (value != null)
                //            {
                //                Product.MixerServiceId = (value as Service).ServiceId;

                //}
            }
        }
        private bool CentralServiceIsEnabledResource { get { return (bool)Resources["CentralServiceIsEnabled"]; } set { Resources["CentralServiceIsEnabled"] = value; } }
        private bool MixerServiceIsEnabledResource { get { return (bool)Resources["MixerServiceIsEnabled"]; } set { Resources["MixerServiceIsEnabled"] = value; } }
        private bool PumpServiceIsEnabledResource { get { return (bool)Resources["PumpServiceIsEnabled"]; } set { Resources["PumpServiceIsEnabled"] = value; } }
        private ValidationErrorContainer ValidationErrorContainer { get { return (ValidationErrorContainer)Resources["ValidationErrorContainer1"]; } }
        private Mixer Mixer1 { get; set; }
        private Product oldProduct;
        private Service _selectedMixerService;
        private IBeeValidator<Product> Validator { get { return Infrastructure.Main.GetProductValidator(); } }
        public Product Product { get; set; }
        private bool Editing { get; set; }
        private void RecipeComboboxItemHighligted(object sender, MouseEventArgs e)
        {
            (sender as ComboBoxItem).Do(x =>
            {
                var recipe = x.DataContext as Recipe;
                ShowRecipe(recipe);
            },
                () =>
                {
                    RecipeContents = null;
                    RecipeName = string.Empty;
                });
        }

        private void ShowRecipe(Recipe recipe)
        {
            recipe.Do(y =>
            {
                RecipeContents = new ObservableCollection<RecipeContent>();
                foreach (var item in y.RecipeContents.OrderBy(x => x.Stock.StockCatNum))
                {
                    RecipeContents.Add(item);
                }
                RecipeName = recipe.RecipeName;
            },
                            () =>
                            {
                                RecipeContents = null;
                                RecipeName = string.Empty;
                            });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Gate1Selected = true;
            Clients = new ObservableCollection<Client>(ClientManager.GetList(x => !x.IsHidden.GetBool()));
            Recipes = new ObservableCollection<Recipe>(RecipeManager.GetList(x => !x.IsHidden.GetBool()));
            ServiceCategories = new ObservableCollection<ServiceCategoryDTO>(ServiceCategoryManager.GetList());
            CentralServices = new ObservableCollection<Service>(ServiceManager.GetList(x => !x.IsHidden.GetBool() && x.ServiceCatNum == 3));
            MixerServices = new ObservableCollection<Service>(ServiceManager.GetList(x => !x.IsHidden.GetBool() && x.ServiceCatNum == 1));
            PumpServices = new ObservableCollection<Service>(ServiceManager.GetList(x => !x.IsHidden.GetBool() && x.ServiceCatNum == 2));
            Drivers = new ObservableCollection<DriverDTO>(DriverManager.GetList(x => !x.IsHidden.GetBool()));
            Mixer1 = MixerManager.Get(x => x.MixerId == 1);
            Product.Capacity = Mixer1.Capacity;
            ShowRecipe(Product.Recipe);
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
        private void RecipeComboBoxDropDownClosed(object sender, EventArgs e)
        {
            ShowRecipe(Product.Recipe);
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClientSelected();
        }
        private void ClientSelected()
        {
            List<Site> list = new List<Site>();
            if (Product != null)
            {
                Product.Client = Clients.FirstOrDefault(x => x.ClientId == Product.ClientId);
                Product.Client.Do(x => list.AddRange(x.ActiveSites));
            }
            Sites = new ObservableCollection<Site>(list);
        }

        private void İptalButon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BaşlatClicked(object sender, RoutedEventArgs e)
        {
            Validate(Product);
            if (ValidationError)
                return;
            Save();
            ProductManager.Get(x => x.ProductId == Product.ProductId).Do(x => Infrastructure.Main.StartProduction(x));
        }
        private void BekletClicked(object sender, RoutedEventArgs e)
        {
            Validator.Validate(Product);
            Validate(Product);
            if (ValidationError)
                return;
            Product.PosStatus = ProductionStatus.HazırdaBeklet;
            Save();
        }
        private void SeriClicked(object sender, RoutedEventArgs e)
        {
            Validate(Product);
            if (ValidationError)
                return;
            Product.PosStatus = ProductionStatus.SeriÜretim;
            Save();
        }
        private void Save()
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
                this.Close();
            });
        }
        private void Validate(Product product)
        {
            var result = Validator.Validate(product);
            result.DoIfElse(x => !x.IsValid, x =>
            {
                ValidationErrorContainer.SetErrors(new ValidError()
                {
                    ErrorCode = result.Errors[0].ErrorCode,
                    ErrorMessage = result.Errors[0].ErrorMessage,
                    PropertyName = result.Errors[0].PropertyName,
                });
                ValidationError = true;
            }, x => ValidationError = false);
            product.DoIf(prod => (prod.ServiceCatNum == 1 || prod.ServiceCatNum == 2) && !ValidationError, prod => ServiceManager.Get(x => x.ServiceId == product.MixerServiceId).DoIf(x => x.Capacity < product.RealTarget, x =>
            {
                ValidationErrorContainer.SetErrors(new ValidError()
                {
                    ErrorCode = "Capacity is not valid",
                    ErrorMessage = "Kapasite aşıldı!",
                    PropertyName = nameof(Product.RealTarget),
                });
                ValidationError = true;
            }));
        }
    }
}
