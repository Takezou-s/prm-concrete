using Extensions;
using Promax.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using System.Windows.Shapes;

namespace Promax.UI.Windows
{
    /// <summary>
    /// Interaction logic for WarningWindow.xaml
    /// </summary>
    public partial class WarningWindow : Window
    {
        public static WarningWindowBuilder GetBuilder(FrameworkElement frameworkElement)
        {
            return new WarningWindowBuilder(frameworkElement);
        }
        public static void ShowWarning(FrameworkElement visual, AlignmentX alignmentX, AlignmentY alignmentY, string header, string info)
        {
            WarningWindow a = new WarningWindow();
            a.WarningHeader = header;
            a.WarningInfo = info;
            a.visual = visual;
            a.alignmentX = alignmentX;
            a.alignmentY = alignmentY;
            //var point = a.thePath.TranslatePoint(new Point(0, 0), a);

            //double x = 0;
            //if (alignmentX == AlignmentX.Right)
            //    x = visual.ActualWidth;
            //else if (alignmentX == AlignmentX.Center)
            //    x = visual.ActualWidth / 2;

            //double y = 0;
            //if (alignmentY == AlignmentY.Bottom)
            //    y = visual.ActualHeight;
            //else if (alignmentY == AlignmentY.Center)
            //    y = visual.ActualHeight / 2;

            //var screenPoint = visual.PointToScreen(new Point(x, y));
            //var showPoint = new Point(screenPoint.X - point.X, screenPoint.Y - point.Y+15);
            //a.Left = showPoint.X;
            //a.Top = showPoint.Y;
            a.Show();
        }
        public static void ShowWarning(FrameworkElement visual, AlignmentX alignmentX, AlignmentY alignmentY)
        {
            WarningWindow a = new WarningWindow();
            a.WarningHeader = "Uyarı";
            a.WarningInfo = "Bu Alan Bir Değer İçermelidir.";
            a.visual = visual;
            a.alignmentX = alignmentX;
            a.alignmentY = alignmentY;
            //var point = a.thePath.TranslatePoint(new Point(0, 0), a);

            //double x = 0;
            //if (alignmentX == AlignmentX.Right)
            //    x = visual.ActualWidth;
            //else if (alignmentX == AlignmentX.Center)
            //    x = visual.ActualWidth / 2;

            //double y = 0;
            //if (alignmentY == AlignmentY.Bottom)
            //    y = visual.ActualHeight;
            //else if (alignmentY == AlignmentY.Center)
            //    y = visual.ActualHeight / 2;

            //var screenPoint = visual.PointToScreen(new Point(x, y));
            //var showPoint = new Point(screenPoint.X - point.X, screenPoint.Y - point.Y+15);
            //a.Left = showPoint.X;
            //a.Top = showPoint.Y;
            a.Show();
        }
        public static DependencyProperty WarningHeaderProperty = DependencyProperty.Register(
            nameof(WarningHeader), typeof(string), typeof(WarningWindow));
        public static DependencyProperty WarningInfoProperty = DependencyProperty.Register(
            nameof(WarningInfo), typeof(string), typeof(WarningWindow));

        private AlignmentX alignmentX;
        private AlignmentY alignmentY;
        private FrameworkElement visual;
        private BackgroundWorker worker;

        public string WarningHeader
        {
            get => (string)GetValue(WarningHeaderProperty);
            set
            {
                var oldValue = WarningHeader;
                var newValue = value;
                SetValue(WarningHeaderProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(WarningHeaderProperty, oldValue, newValue));
            }
        }
        public string WarningInfo
        {
            get => (string)GetValue(WarningInfoProperty);
            set
            {
                var oldValue = WarningInfo;
                var newValue = value;
                SetValue(WarningInfoProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(WarningInfoProperty, oldValue, newValue)); ;
            }
        }
        public int Duration { get; set; } = 3000;
        private WarningWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Locate();
            worker = new BackgroundWorker();
            worker.DoWork += BackgroundThreadRun;
            worker.RunWorkerCompleted += BackgroundThreadRunCompleted;
            worker.RunWorkerAsync();
        }

        private void Locate()
        {
            var point = thePath.TranslatePoint(new Point(0, 0), this);

            double x = 0;
            if (alignmentX == AlignmentX.Right)
                x = visual.ActualWidth;
            else if (alignmentX == AlignmentX.Center)
                x = visual.ActualWidth / 2;

            double y = 0;
            if (alignmentY == AlignmentY.Bottom)
                y = visual.ActualHeight;
            else if (alignmentY == AlignmentY.Center)
                y = visual.ActualHeight / 2;

            var screenPoint = visual.PointToScreen(new Point(x, y));
            var showPoint = new Point(screenPoint.X - point.X, screenPoint.Y - point.Y);
            Left = showPoint.X;
            Top = showPoint.Y;
        }

        private void BackgroundThreadRun(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(Duration);
        }
        private void BackgroundThreadRunCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            visual.Do(x => x.Focus());
            this.Close();
        }
        public class WarningWindowBuilder
        {
            private WarningWindow window;
            internal WarningWindowBuilder(FrameworkElement frameworkElement)
            {
                window = new WarningWindow();
                window.visual = frameworkElement;
            }
            public WarningWindowBuilder AlignmentX(AlignmentX alignmentX)
            {
                window.alignmentX = alignmentX;
                return this;
            }
            public WarningWindowBuilder AlignmentY(AlignmentY alignmentY)
            {
                window.alignmentY = alignmentY;
                return this;
            }
            public WarningWindowBuilder Header(string header)
            {
                window.WarningHeader = header;
                return this;
            }
            public WarningWindowBuilder Info(string info)
            {
                window.WarningInfo = info;
                return this;
            }
            public WarningWindow GetWindow()
            {
                return window;
            }
        }
    }
}
