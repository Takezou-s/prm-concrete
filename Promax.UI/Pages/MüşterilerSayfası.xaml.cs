using Promax.Business.Abstract;
using Promax.Business.Mappers;
using Promax.Core;
using Promax.DataAccess.Abstract;
using Promax.Entities;
using Promax.UI.Windows;
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
using System.Windows.Threading;

namespace Promax.UI
{
    /// <summary>
    /// MüşterilerSayfası.xaml etkileşim mantığı
    /// </summary>
    public partial class MüşterilerSayfası : Page, INotifyPropertyChanged
    {
        public IComplexClientManager ComplexClientManager { get => Infrastructure.Main.GetClientManager(); }
        public IComplexSiteManager ComplexSiteManager { get => Infrastructure.Main.GetSiteManager(); }
        public ISentViewReader SentViewReader { get => Infrastructure.Main.GetSentViewReader(); }
        public IBeeMapper Mapper { get => Infrastructure.Main.GetMapper(); }

        public object selectedClient
        {
            get => _selectedClient; set
            {
                _selectedClient = value;
                ClientSelected();
            }
        }
        public object selectedSite
        {
            get => _selectedSite; set
            {
                _selectedSite = value;
            }
        }
        private Client SelectedClient { get { return selectedClient == null ? null : (Client)selectedClient; } }
        private Site SelectedSite { get { return selectedSite == null ? null : (Site)selectedSite; } }

        public event PropertyChangedEventHandler PropertyChanged;
        private DateTime _now;
        public string KodColumn;
        private object _selectedClient, _selectedSite;

        public DateTime CurrentDateTime
        {
            get { return _now; }
            private set
            {
                _now = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDateTime"));
            }
        }

        public MüşterilerSayfası()
        {
            _now = DateTime.Now;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            CurrentDateTime = DateTime.Now;
        }

        private void CreateNewClient(object sender, RoutedEventArgs e)
        {
            MüşteriKartı müşteriKartı = MüşteriKartı.CreateNew();
            müşteriKartı.ShowDialog();
            ListClients();
        }
        private void EditClient(object sender, RoutedEventArgs e)
        {
            if (SelectedClient == null)
                return;
            MüşteriKartı müşteriKartı = MüşteriKartı.Edit(SelectedClient);
            müşteriKartı.ShowDialog();
        }

        private void CreateNewSite(object sender, RoutedEventArgs e)
        {
            if (SelectedClient == null)
                return;
            Windows.ŞantiyeKartı şantiyeKartı = ŞantiyeKartı.CreateNew(SelectedClient);
            şantiyeKartı.ShowDialog();
            ClientSelected();
        }
        private void EditSite(object sender, RoutedEventArgs e)
        {
            if (SelectedSite == null)
                return;
            Windows.ŞantiyeKartı şantiyeKartı = ŞantiyeKartı.Edit(SelectedSite);
            şantiyeKartı.ShowDialog();
            ClientSelected();
        }

        private void MusterilerPage_Loaded(object sender, RoutedEventArgs e)
        {
            ListClients();
        }

        private void ListClients()
        {
            var list = ComplexClientManager.GetList(x => x.IsHidden == "false");
            ObservableCollection<Client> clients = new ObservableCollection<Client>(list);
            clientDataGrid.ItemsSource = clients;
        }

        private void ClientSelected()
        {
            ObservableCollection<Site> sites = new ObservableCollection<Site>();
            ObservableCollection<SentViewDTO> sentViews = new ObservableCollection<SentViewDTO>();
            if (SelectedClient != null)
            {
                List<Site> list = new List<Site>();
                foreach (var item in SelectedClient.ActiveSites)
                {
                    list.Add(item);
                }
                sites = new ObservableCollection<Site>(list);
                sentViews = new ObservableCollection<SentViewDTO>(SentViewReader.GetList(" where PRODUCT_DATE = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and CLIENT_ID='" + SelectedClient.ClientId.ToString() + "'"));
            }
            sentDataGrid.ItemsSource = sentViews;
            siteDataGrid.ItemsSource = sites;
        }

        private void RefreshClients(object sender, RoutedEventArgs e)
        {
            ListClients();
        }

        private void DeleteClient(object sender, RoutedEventArgs e)
        {
            if (SelectedClient == null || MessageBoxResult.Yes != MessageBox.Show("Seçili kayıtlar silinsin mi?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information))
                return;
            DeleteClient(SelectedClient);
        }
        private void DeleteSite(object sender, RoutedEventArgs e)
        {
            if (SelectedSite == null || MessageBoxResult.Yes != MessageBox.Show("Seçili kayıtlar silinsin mi?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information))
                return;
            DeleteSite(SelectedSite);
        }
        private void DeleteClient(Client client)
        {
            var clientDto = new Client();
            Mapper.Map<Client, Client>(client, clientDto);
            clientDto.IsHidden = "true";
            ComplexClientManager.Update(clientDto, client);
            ListClients();
        }

        private void ClientGravityCheckClicked(object sender, RoutedEventArgs e)
        {
            Client client = clientDataGrid.CurrentItem as Client;
            if (client != null)
            {
                ComplexClientManager.Update(client, client);
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditClient(null, null);
        }

        private void DataGridRow_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            EditSite(null, null);
        }

        private void DeleteSite(Site site)
        {
            var siteDto = new Site();
            Mapper.Map<Site, Site>(site, siteDto);
            siteDto.IsHidden = "true";
            ComplexSiteManager.Update(siteDto, site);
            //SiteManager.Update(siteDto);
            //InitClient(SelectedClient);
            ClientSelected();
        }
    }
}
