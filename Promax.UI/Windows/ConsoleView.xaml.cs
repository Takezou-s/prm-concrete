using Extensions;
using RemoteVariableHandler.Core;
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
using System.Windows.Shapes;
using Utility.Binding;

namespace Promax.UI.Windows
{
    /// <summary>
    /// Interaction logic for ConsoleView.xaml
    /// </summary>
    public partial class ConsoleView : Window
    {
        private object _selectedVariable;
        private MyBinding _bind;

        public ConsoleView()
        {
            InitializeComponent();
        }
        public object SelectedVariable
        {
            get => _selectedVariable; set
            {
                _selectedVariable = value;
                if (value != null && value is HideableVariable)
                {
                    txtReadValue.Text = (value as HideableVariable).Variable.ReadValue.ToString();
                    txtWriteValue.Text = (value as HideableVariable).Variable.WriteValue.ToString();
                    txtDeğişkenAdı.Text = (value as HideableVariable).Variable.VariableName;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<IRemoteVariable> list = new List<IRemoteVariable>(Infrastructure.Main.VariableScope.RemoteVariables);
            list.AddRange(Infrastructure.Main.RecipeScope.RemoteVariables);
            list.AddRange(Infrastructure.Main.CommandScope.RemoteVariables);
            list.AddRange(Infrastructure.Main.RetentiveParameters.RemoteVariables);
            ObservableCollection<HideableVariable> vars = new ObservableCollection<HideableVariable>();
            list.ForEach(x => vars.Add(new HideableVariable(x, Visibility.Visible)));
            variablesListBox.ItemsSource = vars;

            registersDataGrid.ItemsSource = Infrastructure.Main.VariableCommunicator.RegisterValues.ToArray();
        }

        private void SetReadValue(object sender, RoutedEventArgs e)
        {
            if (SelectedVariable == null || (SelectedVariable != null && !(SelectedVariable is HideableVariable)))
                return;
            _bind = new MyBinding();
            _bind.CreateBinding().Behaviour(MyBindingBehaviour.Map).Source(txtReadValue).SourceProperty(nameof(txtReadValue.Text)).Target((SelectedVariable as HideableVariable).Variable).TargetProperty("ReadValue");
            _bind.InitialMapping();
            _bind.ClearBindings();
            _bind = null;
        }

        private void SetWriteValue(object sender, RoutedEventArgs e)
        {
            if (SelectedVariable == null || (SelectedVariable != null && !(SelectedVariable is HideableVariable)))
                return;
            _bind = new MyBinding();
            _bind.CreateBinding().Behaviour(MyBindingBehaviour.Map).Source(txtWriteValue).SourceProperty(nameof(txtWriteValue.Text)).Target((SelectedVariable as HideableVariable).Variable).TargetProperty("WriteValue");
            _bind.InitialMapping();
            _bind.ClearBindings();
            _bind = null;
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            var a = Promax.Core.ExtensionMethods.Vars;
            StringBuilder vars = new StringBuilder();
            a.ForEach(x => vars.AppendLine(x));

            var b = Promax.Core.ExtensionMethods.Params;
            StringBuilder paramss = new StringBuilder();
            b.ForEach(x => paramss.AppendLine(x));
            var search = txtSearch.Text;
            if (string.IsNullOrEmpty(search))
            {
                foreach (var item in (variablesListBox.ItemsSource as ObservableCollection<HideableVariable>))
                {
                    item.Visibility = Visibility.Visible;
                }
            }
            else
            {
                foreach (var item in (variablesListBox.ItemsSource as ObservableCollection<HideableVariable>))
                {
                    item.Visibility = item.Variable.VariableName.ToLower().Contains(search.ToLower()) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private class HideableVariable : INotifyPropertyChanged
        {
            #region INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
            private IRemoteVariable _variable;
            private Visibility _visibility;
            public HideableVariable(IRemoteVariable variable, Visibility visibility)
            {
                Variable = variable;
                Visibility = visibility;
            }
            public IRemoteVariable Variable
            {
                get
                {
                    return _variable;
                }
                set
                {
                    bool changed = false;
                    if (!_variable.IsEqual(value))
                        changed = true;
                    _variable = value;
                    if (changed)
                    {
                        OnPropertyChanged(nameof(Variable));
                    }
                }
            }
            public Visibility Visibility
            {
                get
                {
                    return _visibility;
                }
                set
                {
                    bool changed = false;
                    if (!_visibility.IsEqual(value))
                        changed = true;
                    _visibility = value;
                    if (changed)
                    {
                        OnPropertyChanged(nameof(Visibility));
                    }
                }
            }
        }

        private void RefreshRegister(object sender, RoutedEventArgs e)
        {
            registersDataGrid.ItemsSource = Infrastructure.Main.VariableCommunicator.RegisterValues.ToArray();
        }

        private void txtSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Search(null, null);
        }

        private void txtReadValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SetReadValue(null, null);
        }

        private void txtWriteValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SetWriteValue(null, null);
        }
    }
}
