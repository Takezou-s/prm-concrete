using Extensions;
using Promax.UI.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
using Utility;
using Utility.Binding;
using VirtualPLC;

namespace Promax.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool _init;
        private static DoOnce RunWorkerOnce = new DoOnce();

        public static bool ICloseApp { get; set; }

        private MyBinding _binding = new MyBinding();
        private BackgroundWorker worker;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
            new VirtualController();
        }
        private void Load()
        {
            if (_init)
                return;
            _init = true;
            //Lisans.Value();
            //bool condition = true;
            //condition = Lisans.CheckLicStatus();
            //if (!condition)
            //{
            //    condition = LicenseView.Register();
            //}
            //if (!condition)
            //{
            //    App.Current.Shutdown();
            //}
            //if (LoginView.UserLogin())
            //{
            //    new KabaScadaWindow().Show();
            //}
            //else
            //{
            //    App.Current.Shutdown();
            //}
            new ConsoleView().Show();
            RunWorkerOnce.Perform(() =>
            {
                worker = new BackgroundWorker();
                worker.DoWork += RunBackgroundThread;
                worker.RunWorkerCompleted += BackgroundThreadCompleted;
                worker.RunWorkerAsync();
            });
        }

        private void RunBackgroundThread(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(100);
            //Infrastructure.Main.HandleExceptionsNotNotify(() => BackgroundProcessor.List.ForEach(x => x.Run()));
        }

        private void BackgroundThreadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Infrastructure.Main.HandleExceptionsNotNotify(() => BackgroundProcessor.List.ForEach(x => x.WhenCompleted()));
            worker.DoIf(x => !x.IsBusy, x => x.RunWorkerAsync());
        }
    }
}
