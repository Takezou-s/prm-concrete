using Promax.Business.Abstract;
using Promax.Business.Mappers;
using Promax.Core;
using Promax.Entities;
using Promax.UI.Windows;
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
    /// ServislerSayfası.xaml etkileşim mantığı
    /// </summary>
    public partial class ServislerSayfası : Page
    {
        public ServislerSayfası()
        {
            InitializeComponent();
        }

        public object selectedDriver { get; set; }
        public object selectedMixerService { get; set; }
        public object selectedPumpService { get; set; }
        public object selectedCentralService { get; set; }

        public DriverDTO SelectedDriver { get => selectedDriver as DriverDTO; }
        public Service SelectedMixerService { get => selectedDriver as Service; }
        public Service SelectedPumpService { get => selectedPumpService as Service; }
        public Service SelectedCentralService { get => selectedCentralService as Service; }
        public IComplexDriverManager DriverManager { get => Infrastructure.Main.GetDriverManager(); }
        public IComplexServiceManager ServiceManager { get => Infrastructure.Main.GetServiceManager(); }
        public IBeeMapper Mapper { get => Infrastructure.Main.GetMapper(); }
        private void CreateNewDriver(object sender, RoutedEventArgs e)
        {
            var a = SürücüKartı.CreateNew();
            a.ShowDialog();
            ListDrivers();
        }
        private void EditDriver(object sender, RoutedEventArgs e)
        {
            if (SelectedDriver == null)
                return;
            var a = SürücüKartı.CreateNew();
            a.ShowDialog();
            ListDrivers();
            ListAllServices();
        }

        private void DeleteDriver(object sender, RoutedEventArgs e)
        {
            if (SelectedDriver == null || MessageBoxResult.Yes != MessageBox.Show("Seçili kayıtlar silinsin mi?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information))
                return;
            DeleteDriver(SelectedDriver);
        }
        private void DeleteDriver(DriverDTO driver)
        {
            var driverDto = new DriverDTO();
            Mapper.Map<DriverDTO, DriverDTO>(driver, driverDto);
            driverDto.IsHidden = "true";
            DriverManager.Update(driverDto, driver);
            ListDrivers();
            ListAllServices();
        }
        private void RefreshDrivers(object sender, RoutedEventArgs e)
        {
            ListDrivers();
            ListAllServices();
        }
        private void DriverDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditDriver(null, null);
        }


        private void CreateNewMixerService(object sender, RoutedEventArgs e)
        {
            var a = ServisKartı.CreateNew(1);
            a.ShowDialog();
            ListMixerServices();
        }
        private void EditMixerService(object sender, RoutedEventArgs e)
        {
            if (SelectedMixerService == null)
                return;
            var a = ServisKartı.Edit(SelectedMixerService);
            a.ShowDialog();
            ListMixerServices();
        }
        private void DeleteMixerService(object sender, RoutedEventArgs e)
        {
            if (SelectedMixerService == null || MessageBoxResult.Yes != MessageBox.Show("Seçili kayıtlar silinsin mi?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information))
                return;
            DeleteMixerService(SelectedMixerService);
        }
        private void DeleteMixerService(Service service)
        {
            var serviceDto = new Service();
            Mapper.Map<Service, Service>(service, serviceDto);
            ServiceManager.Update(serviceDto.DoReturn(x => x.IsHidden = "true"), service);
            ListMixerServices();
        }
        private void RefreshMixerServices(object sender, RoutedEventArgs e)
        {
            ListMixerServices();
        }
        private void MixerServiceDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditMixerService(null, null);
        }

        private void CreateNewPumpService(object sender, RoutedEventArgs e)
        {
            var a = ServisKartı.CreateNew(2);
            a.ShowDialog();
            ListPumpServices();
        }
        private void EditPumpService(object sender, RoutedEventArgs e)
        {
            if (SelectedPumpService == null)
                return;
            var a = ServisKartı.Edit(SelectedPumpService);
            a.ShowDialog();
            ListPumpServices();
        }
        private void DeletePumpService(object sender, RoutedEventArgs e)
        {
            if (SelectedPumpService == null || MessageBoxResult.Yes != MessageBox.Show("Seçili kayıtlar silinsin mi?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information))
                return;
            DeletePumpService(SelectedPumpService);
        }
        private void DeletePumpService(Service service)
        {
            var serviceDto = new Service();
            Mapper.Map<Service, Service>(service, serviceDto);
            ServiceManager.Update(serviceDto.DoReturn(x => x.IsHidden = "true"), service);
            ListPumpServices();
        }
        private void RefreshPumpServices(object sender, RoutedEventArgs e)
        {
            ListPumpServices();
        }
        private void PumpServiceDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditPumpService(null, null);
        }


        private void CreateNewCentralService(object sender, RoutedEventArgs e)
        {
            var a = ServisKartı.CreateNew(3);
            a.ShowDialog();
            ListCentralServices();
        }
        private void EditCentralService(object sender, RoutedEventArgs e)
        {
            if (SelectedCentralService == null)
                return;
            var a = ServisKartı.Edit(SelectedCentralService);
            a.ShowDialog();
            ListCentralServices();

        }
        private void DeleteCentralService(object sender, RoutedEventArgs e)
        {
            if (SelectedCentralService == null || MessageBoxResult.Yes != MessageBox.Show("Seçili kayıtlar silinsin mi?", string.Empty, MessageBoxButton.YesNo, MessageBoxImage.Information))
                return;
            DeleteCentralService(SelectedCentralService);
        }
        private void DeleteCentralService(Service service)
        {
            var serviceDto = new Service();
            Mapper.Map<Service, Service>(service, serviceDto);
            ServiceManager.Update(serviceDto.DoReturn(x => x.IsHidden = "true"), service);
            ListCentralServices();
        }
        private void RefreshCentralServices(object sender, RoutedEventArgs e)
        {
            ListCentralServices();
        }
        private void CentralServiceDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditCentralService(null, null);
        }


        private void DriverGravityClicked(object sender, RoutedEventArgs e)
        {
            DriverDTO driver = driverDataGrid.CurrentItem as DriverDTO;
            driver.Do(x => DriverManager.Update(driver, driver));
        }
        private void MixerServiceGravityClicked(object sender, RoutedEventArgs e)
        {
            Service service = mixerServiceDataGrid.CurrentItem as Service;
            service.Do(x => ServiceManager.Update(service, service));
        }
        private void PumpServiceGravityClicked(object sender, RoutedEventArgs e)
        {
            Service service = pumpServiceDataGrid.CurrentItem as Service;
            service.Do(x => ServiceManager.Update(service, service));
        }
        private void CentralServiceGravityClicked(object sender, RoutedEventArgs e)
        {
            Service service = centralServiceDataGrid.CurrentItem as Service;
            service.Do(x => ServiceManager.Update(service, service));
        }

        //private void Servis_Click(object sender, RoutedEventArgs e)
        //{
        //    double opaklik;
        //    opaklik = 0.5;//opaklik değeri 0-1 arasında
        //    Windows.ServisKartı servisKartı = new Windows.ServisKartı();
        //    servisKartı.Kapasite.IsEnabled = false;
        //    servisKartı.Kapasite_.IsEnabled = false;
        //    servisKartı.Kapasite_.Opacity = opaklik;
        //    servisKartı.Kapasite.Opacity = opaklik;
        //    servisKartı.Show();
        //}

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListDrivers();
            ListMixerServices();
            ListPumpServices();
            ListCentralServices();
        }

        private void ListDrivers()
        {
            ObservableCollection<DriverDTO> drivers = new ObservableCollection<DriverDTO>(DriverManager.GetList(x => x.IsHidden == "false"));
            driverDataGrid.ItemsSource = drivers;
        }
        private void ListMixerServices()
        {
            var list = new List<Service>(ServiceManager.GetList(x => x.IsHidden == "false" && x.ServiceCatNum == 1));
            ObservableCollection<Service> services = new ObservableCollection<Service>(list);
            mixerServiceDataGrid.ItemsSource = services;
        }
        private void ListPumpServices()
        {
            var list = new List<Service>(ServiceManager.GetList(x => x.IsHidden == "false" && x.ServiceCatNum == 2));
            ObservableCollection<Service> services = new ObservableCollection<Service>(list);
            pumpServiceDataGrid.ItemsSource = services;
        }
        private void ListCentralServices()
        {
            var list = new List<Service>(ServiceManager.GetList(x => x.IsHidden == "false" && x.ServiceCatNum == 3));
            ObservableCollection<Service> services = new ObservableCollection<Service>(list);
            centralServiceDataGrid.ItemsSource = services;
        }
        private void ListAllServices()
        {
            ListMixerServices();
            ListPumpServices();
            ListCentralServices();
        }
    }
}
