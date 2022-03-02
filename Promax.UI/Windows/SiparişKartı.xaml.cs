using Extensions;
using Promax.Business;
using Promax.Core;
using Promax.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Promax.UI
{
    /// <summary>
    /// SiparişKartı.xaml etkileşim mantığı
    /// </summary>
    public partial class SiparişKartı : Window
    {
        public static readonly DependencyProperty ClientsProperty =
       DependencyProperty.Register(nameof(Clients), typeof(ObservableCollection<Client>), typeof(SiparişKartı));
        public static readonly DependencyProperty SitesProperty =
       DependencyProperty.Register(nameof(Sites), typeof(ObservableCollection<Site>), typeof(SiparişKartı));
        public static readonly DependencyProperty RecipesProperty =
       DependencyProperty.Register(nameof(Recipes), typeof(ObservableCollection<Recipe>), typeof(SiparişKartı));
        public static readonly DependencyProperty ServiceCategoriesProperty =
       DependencyProperty.Register(nameof(ServiceCategories), typeof(ObservableCollection<ServiceCategory>), typeof(SiparişKartı));
        public static SiparişKartı CreateNew()
        {
            return new SiparişKartı()
            {
                Order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderTime = DateTime.Now
                }
            };
        }
        public static SiparişKartı Edit(Order order)
        {
            var a = new SiparişKartı();
            a.Order = a.Mapper.Map<Order>(a.Mapper.Map<OrderDTO>(order));
            a.oldOrder = order;
            a.Editing = true;
            return a;
        }
        private SiparişKartı()
        {
            InitializeComponent();
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
        public ObservableCollection<ServiceCategory> ServiceCategories
        {
            get => (ObservableCollection<ServiceCategory>)GetValue(ServiceCategoriesProperty); set
            {
                var oldValue = ServiceCategories;
                var newValue = value;
                SetValue(ServiceCategoriesProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(ServiceCategoriesProperty, oldValue, newValue));
            }
        }

        private Order oldOrder;
        private bool Editing { get; set; }
        public IBeeMapper Mapper { get => Infrastructure.Main.Mapper; }
        public IOrderManager OrderManager { get => Infrastructure.Main.OrderManager; }
        public IClientManager ClientManager { get => Infrastructure.Main.ClientManager; }
        public ISiteManager SiteManager { get => Infrastructure.Main.SiteManager; }
        public IRecipeManager RecipeManager { get => Infrastructure.Main.RecipeManager; }
        public IServiceCategoryManager ServiceCategoryManager { get => Infrastructure.Main.ServiceCategoryManager; }
        public Order Order { get; set; }
        public Client Client { get; set; }

        private void TamamButon_Click(object sender, RoutedEventArgs e)
        {
            Infrastructure.Main.HandleExceptions(() =>
            {
                if (Editing)
                {
                    OrderManager.Update(Order);
                }
                else
                {
                    OrderManager.Add(Order);
                }
                this.Close();
            });
        }

        private void İptalButon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Clients = new ObservableCollection<Client>(ClientManager.GetList(x => !x.IsHidden.GetBool()));
            Recipes = new ObservableCollection<Recipe>(RecipeManager.GetList(x => !x.IsHidden.GetBool()));
            ServiceCategories = new ObservableCollection<ServiceCategory>(ServiceCategoryManager.GetList());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClientSelected();
        }
        private void ClientSelected()
        {
            List<Site> list = new List<Site>();
            if (Order != null)
            {
                Order.Client = Clients.FirstOrDefault(x => x.ClientId == Order.ClientId);
                if (Order.Client != null)
                {
                    list.AddRange(Order.Client.ActiveSites);
                }
            }
            Sites = new ObservableCollection<Site>(list);
        }

        private void ComboBoxItem_GotMouseCapture(object sender, MouseEventArgs e)
        {
            ;
        }
    }
}
