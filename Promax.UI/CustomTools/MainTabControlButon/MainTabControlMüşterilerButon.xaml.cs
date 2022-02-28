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

namespace Promax.UI
{
    /// <summary>
    /// MainTabControlMüşterilerButon.xaml etkileşim mantığı
    /// </summary>
    public partial class MainTabControlMüşterilerButon : Button
    {
        public static readonly DependencyProperty FrameProperty =
        DependencyProperty.Register("TheFrame", typeof(Frame), typeof(MainTabControlMüşterilerButon));

        public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.Register("IsChecked", typeof(bool), typeof(MainTabControlMüşterilerButon));

        private bool open;
        public bool IsChecked
        {
            get
            {
                return (bool)GetValue(IsCheckedProperty);
            }
            set
            {
                this.SetValue(IsCheckedProperty, value);
            }
        }
        private Page _theWindow;
        public Page TheWindow
        {
            get
            {
                return _theWindow;
            }
            set
            {
                _theWindow = value;
                if (value != null)
                {
                    //value.Unloaded += (o, e) =>
                    //{
                    //    e.Cancel = true;
                    //    value.Visibility = Visibility.Hidden;
                    //};
                    value.IsVisibleChanged += (o, e) =>
                    {
                        this.open = value.IsVisible;
                        this.IsChecked = open;
                    };
                }
            }
        }
        public Frame TheFrame
        {
            get
            {
                return (Frame)GetValue(FrameProperty);
            }
            set
            {
                SetValue(FrameProperty, value);
            }
        }
        public MainTabControlMüşterilerButon()
        {
            InitializeComponent();
        }

        protected override void OnClick()
        {
            if (TheFrame != null && TheWindow != null)
            {
                TheFrame.Content = TheWindow;
            }
            base.OnClick();
        }
    }
}
