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
    /// SürücüKartı.xaml etkileşim mantığı
    /// </summary>
    public partial class SürücüKartı : Window
    {
        public static SürücüKartı CreateNew()
        {
            return new SürücüKartı() { Driver = new DriverDTO() };
        }
        public static SürücüKartı Edit(DriverDTO driver)
        {
            var a = new SürücüKartı();
            a.Driver = a.Mapper.Map<DriverDTO>(a.Mapper.Map<DriverDTO2>(driver));
            a.oldDriver = driver;
            a.Editing = true;
            return a;
        }
        private bool Editing { get; set; }

        public DriverDTO Driver { get; set; }

        private DriverDTO oldDriver;

        public IComplexDriverManager DriverManager { get => Infrastructure.Main.GetDriverManager(); }
        public IBeeMapper Mapper { get => Infrastructure.Main.GetMapper(); }
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
                    DriverManager.Update(Driver, oldDriver);
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
