using Extensions;
using Promax.Core;
using Promax.UI.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Promax.UI.CustomTools
{
    /// <summary>
    /// Interaction logic for WarningCaller.xaml
    /// </summary>
    public partial class WarningCaller : UserControl
    {
        private ValidationErrorContainer _validationErrorContainer;
        public static DependencyProperty FrameworkElementProperty = DependencyProperty.Register(nameof(FrameworkElement), typeof(FrameworkElement), typeof(WarningCaller));
        private FrameworkElement _frameworkElement;

        public bool ShowErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public FrameworkElement FrameworkElement { get => (FrameworkElement)GetValue(FrameworkElementProperty); set => SetValue(FrameworkElementProperty, value); }
        public ValidationErrorContainer ValidationErrorContainer
        {
            get => _validationErrorContainer; set
            {
                _validationErrorContainer = value;
                value.Do(x => x.ErrorOccured += ErrorOccured);
            }
        }

        private void ErrorOccured(object sender, EventArgs e)
        {
            ValidationErrorContainer.ErrorProperties.FirstOrDefault(x => x.ErrorCode == ErrorCode).DoIfElse(x => ShowErrorMessage, x =>
            {
                WarningWindow.GetBuilder(FrameworkElement).Info(x.ErrorMessage).AlignmentX(AlignmentX.Right).AlignmentY(AlignmentY.Bottom).GetWindow().Show();
            }, x => WarningWindow.GetBuilder(FrameworkElement).AlignmentX(AlignmentX.Right).AlignmentY(AlignmentY.Bottom).GetWindow().Show());
        }

        public WarningCaller()
        {
            InitializeComponent();
        }
    }
}
