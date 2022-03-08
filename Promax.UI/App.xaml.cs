using Promax.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Promax.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IBeeLogger _logger { get => Infrastructure.Main.Logger; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var a = Infrastructure.Main;
            SetupExceptionHandling();
        }

        private void SetupExceptionHandling()
        {
            //_logger = Infrastructure.Main.GetLogger();
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

            DispatcherUnhandledException += (s, e) =>
            {
                LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
                e.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
                e.SetObserved();
            };
        }

        private void LogUnhandledException(Exception exception, string source)
        {
            string message = $"Unhandled exception ({source})";
            try
            {
                //MessageBox.Show(exception.Message, string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show(Infrastructure.Main.GetExceptionMessage(exception), string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                System.Reflection.AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
                message = string.Format("Unhandled exception in {0} v{1}", assemblyName.Name, assemblyName.Version);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "Exception in LogUnhandledException");
                Infrastructure.Main.LogException(new Exception("Exception in LogUnhandledException", ex));
            }
            finally
            {
                //_logger.Error(exception, message);
                Infrastructure.Main.LogException(new Exception(message, exception));
            }
        }
    }
}
