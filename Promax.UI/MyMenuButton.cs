using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Xceed.Wpf.Toolkit;

namespace Promax.UI
{
    public class MyMenuButton : ToggleButton
    {
        public static readonly DependencyProperty FrameProperty =
        DependencyProperty.Register("TheFrame", typeof(Frame), typeof(MyMenuButton));

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
        public MyMenuButton() : base()
        {

        }
        private bool open;
        protected override void OnClick()
        {
            if (TheFrame != null && TheWindow != null)
            {
                TheFrame.Content = TheWindow;
            }
            base.OnClick();
        }
        protected override void OnChecked(RoutedEventArgs e)
        {
            if (open)
            {
                base.OnChecked(e);
            }
            this.IsChecked = open;

        }
        protected override void OnUnchecked(RoutedEventArgs e)
        {
            if (!open)
            {
                base.OnUnchecked(e);
            }
            this.IsChecked = open;
        }
    }
    public class TodayButton : Button
    {
        public static readonly DependencyProperty DateTimePickerProperty =
        DependencyProperty.Register(nameof(DateTimePicker), typeof(DateTimePicker), typeof(TodayButton));

        public DateTimePicker DateTimePicker
        {
            get
            {
                return (DateTimePicker)GetValue(DateTimePickerProperty);
            }
            set
            {
                SetValue(DateTimePickerProperty, value);
            }
        }
        public TodayButton() : base()
        {
        }
        private bool open;
        protected override void OnClick()
        {
            if (DateTimePicker != null)
            {
                DateTimePicker.Value = DateTime.Now;
            }
            base.OnClick();
        }
    }
}
