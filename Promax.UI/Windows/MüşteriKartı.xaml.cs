using Promax.Business;
using Promax.Core;
using Promax.Entities;
using System.Windows;

namespace Promax.UI.Windows
{
    /// <summary>
    /// MüşteriKartı.xaml etkileşim mantığı
    /// </summary>
    public partial class MüşteriKartı : Window
    {
        public static MüşteriKartı CreateNew()
        {
            return new MüşteriKartı() { Client = new Client() };
        }
        public static MüşteriKartı Edit(Client client)
        {
            var a = new MüşteriKartı();
            a.Client = a.Mapper.Map<Client>(a.Mapper.Map<ClientDTO>(client));
            a.oldClient = client;
            a.Editing = true;
            return a;
        }
        private Visibility KurumsalVisibility { get { return (Visibility)Resources["KurumsalMüşteriSelectedVisibility"]; } set { Resources["KurumsalMüşteriSelectedVisibility"] = value; } }
        private Visibility BireyselVisibility { get { return (Visibility)Resources["BireyselMüşteriSelectedVisibility"]; } set { Resources["BireyselMüşteriSelectedVisibility"] = value; } }
        private bool Editing { get; set; }
        private Client oldClient;
        public Client Client { get; set; }
        public IClientManager ClientManager { get => Infrastructure.Main.ClientManager; }
        public IBeeMapper Mapper { get => Infrastructure.Main.Mapper; }

        private MüşteriKartı()
        {
            InitializeComponent();
        }

        private void TamamButon_Click(object sender, RoutedEventArgs e)
        {
            Infrastructure.Main.HandleExceptions(() =>
            {
                if (Editing)
                {
                    ClientManager.Update(Client);
                }
                else
                {
                    ClientManager.Add(Client);
                }
                this.Close();
            });
        }
        private void SelectKurumsalMüşteri(object sender, RoutedEventArgs e)
        {
            KurumsalVisibility = Visibility.Visible;
            BireyselVisibility = Visibility.Collapsed;
        }
        private void SelectBireyselMüşteri(object sender, RoutedEventArgs e)
        {
            BireyselVisibility = Visibility.Visible;
            KurumsalVisibility = Visibility.Collapsed;
        }

        private void İptalButon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
