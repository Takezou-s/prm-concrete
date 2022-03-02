using Promax.Business;
using Promax.Core;
using Promax.Entities;
using System.Windows;

namespace Promax.UI.Windows
{
    /// <summary>
    /// ŞantiyeKartı.xaml etkileşim mantığı
    /// </summary>
    public partial class ŞantiyeKartı : Window
    {
        public static ŞantiyeKartı CreateNew(Client client)
        {
            var a = new ŞantiyeKartı();
            a.Site = new Site();
            a.Site.Client = client;
            return a;
        }
        public static ŞantiyeKartı Edit(Site site)
        {
            var a = new ŞantiyeKartı();
            //a.Site = a.Mapper.Map<Site>(a.Mapper.Map<SiteDTO>(site));
            a.Site = new Site();
            a.Mapper.Map<Site, Site>(site, a.Site);
            a.oldSite = site;
            a.Editing = true;
            return a;
        }
        private bool Editing { get; set; }
        public Site Site { get; set; }

        private Site oldSite;

        public ISiteManager SiteManager { get => Infrastructure.Main.SiteManager; }
        public IBeeMapper Mapper { get => Infrastructure.Main.Mapper; }
        private ŞantiyeKartı()
        {
            InitializeComponent();
        }

        private void TamamButon_Click(object sender, RoutedEventArgs e)
        {
            Infrastructure.Main.HandleExceptions(() =>
            {
                if (Editing)
                {
                    SiteManager.Update(Site);
                }
                else
                {
                    SiteManager.Add(Site);
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
