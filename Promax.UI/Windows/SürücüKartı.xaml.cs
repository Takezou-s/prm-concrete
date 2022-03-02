using Promax.Business;
using Promax.Core;
using Promax.Entities;
using System.Windows;

namespace Promax.UI.Windows
{
    /// <summary>
    /// SürücüKartı.xaml etkileşim mantığı
    /// </summary>
    public partial class SürücüKartı : Window
    {
        public static SürücüKartı CreateNew()
        {
            return new SürücüKartı() { Driver = new Driver() };
        }
        public static SürücüKartı Edit(Driver driver)
        {
            var a = new SürücüKartı();
            a.Driver = a.Mapper.Map<Driver>(a.Mapper.Map<DriverDTO>(driver));
            a.oldDriver = driver;
            a.Editing = true;
            return a;
        }
        private bool Editing { get; set; }

        public Driver Driver { get; set; }

        private Driver oldDriver;

        public IDriverManager DriverManager { get => Infrastructure.Main.DriverManager; }
        public IBeeMapper Mapper { get => Infrastructure.Main.Mapper; }
        private SürücüKartı()
        {
            InitializeComponent();
        }

        private void TamamButon_Click(object sender, RoutedEventArgs e)
        {
            Infrastructure.Main.HandleExceptions(() =>
            {
                if (Editing)
                {
                    DriverManager.Update(Driver);
                }
                else
                {
                    DriverManager.Add(Driver);
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
