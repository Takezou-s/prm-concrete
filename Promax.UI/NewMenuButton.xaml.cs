using Promax.Core;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Promax.UI
{
    /// <summary>
    /// MainTabControlMüşterilerButon.xaml etkileşim mantığı
    /// </summary>
    public partial class NewMenuButton : Button, INotifyPropertyChanged
    {
        public static readonly DependencyProperty FrameProperty =
        DependencyProperty.Register("TheFrame", typeof(Frame), typeof(NewMenuButton));

        public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.Register("IsChecked", typeof(bool), typeof(NewMenuButton));

        public static readonly DependencyProperty ImageSourceProperty =
       DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(NewMenuButton));

        public static readonly DependencyProperty TextProperty =
       DependencyProperty.Register("Text", typeof(string), typeof(NewMenuButton));

        private bool open;
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                this.SetValue(TextProperty, value);
                OnPropertyChanged(nameof(Text));
            }
        }
        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceProperty);
            }
            set
            {
                this.SetValue(ImageSourceProperty, value);
                OnPropertyChanged(nameof(ImageSource));
            }
        }
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

        public event PropertyChangedEventHandler PropertyChanged;

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
                    //value.IsVisibleChanged += (o, e) =>
                    //{
                    //    this.open = value.IsVisible;
                    //    this.IsChecked = open;
                    //};
                    value.IsVisibleChanged += Value_IsVisibleChanged;
                }
            }
        }

        private void Value_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.open = (sender as Page).IsVisible;
            this.IsChecked = open;
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
        public NewMenuButton()
        {
            InitializeComponent();
        }
        public void ShowPage()
        {
            if (TheFrame != null && TheWindow != null)
            {
                TheFrame.Content = TheWindow;
            }
        }
        protected override void OnClick()
        {
            ShowPage();
            base.OnClick();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
