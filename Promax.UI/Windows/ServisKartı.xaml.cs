using Promax.Business.Abstract;
using Promax.Business.Mappers;
using Promax.Core;
using Promax.Entities;
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
    /// ServisKartı.xaml etkileşim mantığı
    /// </summary>
    public partial class ServisKartı : Window
    {
        public static readonly DependencyProperty KapasiteEnabledProperty =
            DependencyProperty.Register(nameof(KapasiteEnabled), typeof(bool), typeof(ServisKartı));
        public static ServisKartı CreateNew(int ServiceCatNum)
        {
            return new ServisKartı()
            {
                Service = new Service()
                {
                    ServiceCatNum = ServiceCatNum
                }
            };
        }
        public static ServisKartı Edit(Service service)
        {
            var a = new ServisKartı();
            a.Service = a.Mapper.Map<Service>(a.Mapper.Map<ServiceDTO>(service));
            a.oldService = service;
            a.Editing = true;
            return a;
        }
        private Visibility KapasiteVisibility { get { return (Visibility)Resources["KapasiteVisibility"]; } set { Resources["KapasiteVisibility"] = value; } }
        private bool Editing { get; set; }

        public bool KapasiteEnabled
        {
            get => (bool)GetValue(KapasiteEnabledProperty);
            set
            {
                var oldValue = KapasiteEnabled;
                var newValue = value;
                SetValue(KapasiteEnabledProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(KapasiteEnabledProperty, oldValue, newValue));
            }
        }
        public Service Service { get; set; }

        private Service oldService;

        public IComplexServiceManager ServiceManager { get => Infrastructure.Main.GetServiceManager(); }
        public IBeeMapper Mapper { get => Infrastructure.Main.GetMapper(); }
        private ServisKartı()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            KapasiteEnabled = Service.ServiceCatNum == 1;
        }

        private void TamamButon_Click(object sender, RoutedEventArgs e)
        {
            Infrastructure.Main.HandleExceptions(() =>
            {
                if (Editing)
                {
                    ServiceManager.Update(Service, oldService);
                }
                else
                {
                    ServiceManager.Add(Service);
                }
                this.Close();
            });
        }

        private void İptalButon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
