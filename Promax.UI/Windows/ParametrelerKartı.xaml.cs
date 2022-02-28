using Promax.Business.Abstract;
using Promax.Core;
using Promax.Entities;
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

namespace Promax.UI.Windows
{
    /// <summary>
    /// Parametreler.xaml etkileşim mantığı
    /// </summary>
    public partial class ParametrelerKartı : Window
    {
        public static readonly DependencyProperty ChangedProperty = DependencyProperty.Register(
            nameof(Changed), typeof(bool), typeof(ParametrelerKartı));
        public ParametrelerKartı()
        {
            InitializeComponent();
        }
        private IBeeMapper Mapper { get => Infrastructure.Main.GetMapper(); }
        private IComplexWeigherManager WeigherManager { get => Infrastructure.Main.GetWeigherManager(); }
        private IComplexSiloManager SiloManager { get => Infrastructure.Main.GetSiloManager(); }
        private IComplexSettingsManager SettingsManager { get => Infrastructure.Main.GetSettingsManager(); }
        private IComplexTransferManager TransferManager { get => Infrastructure.Main.GetTransferManager(); }
        private IComplexAggBunkerManager AggBunkerManager { get => Infrastructure.Main.GetAggBunkerManager(); }
        private IComplexBucketManager BucketManager { get => Infrastructure.Main.GetBucketManager(); }
        private IComplexMixerManager MixerManager { get => Infrastructure.Main.GetMixerManager(); }
        private IComplexMixerGateManager MixerGateManager { get => Infrastructure.Main.GetMixerGateManager(); }
        private Visibility BekletmeBunkeriVisibility { get { return (Visibility)Resources["BekletmeBunkeriVisibility"]; } set { Resources["BekletmeBunkeriVisibility"] = value; } }
        private Visibility TransferBandıVisibility { get { return (Visibility)Resources["TransferBandıVisibility"]; } set { Resources["TransferBandıVisibility"] = value; } }
        private Visibility KovaVisibility { get { return (Visibility)Resources["KovaVisibility"]; } set { Resources["KovaVisibility"] = value; } }
        private Visibility Kapak2Visibility { get { return (Visibility)Resources["Kapak2Visibility"]; } set { Resources["Kapak2Visibility"] = value; } }
        public GeneralSettingsDTO Settings { get; set; } = new GeneralSettingsDTO();
        public TransferBelt TransferBandı { get; set; } = new TransferBelt();
        public AggregateBunker BekletmeBunkeri { get; set; } = new AggregateBunker();
        public Bucket Kova { get; set; } = new Bucket();
        public Mixer Mikser { get; set; } = new Mixer();
        public MixerGate Kapak1 { get; set; } = new MixerGate();
        public MixerGate Kapak2 { get; set; } = new MixerGate();
        public List<Silo> Silos = new List<Silo>();
        public List<Weigher> Weighers = new List<Weigher>();
        private List<object> _changedObjects = new List<object>();
        public bool Changed
        {
            get { return (bool)GetValue(ChangedProperty); }
            set
            {
                var oldValue = Changed;
                var newValue = value;
                SetValue(ChangedProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(ChangedProperty, oldValue, newValue));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WeigherManager.GetList(x => x.Enabled.GetBool()).ForEach(
                x => x.Do(
                    y =>
                    {
                        Weigher weigher = new Weigher();
                        Mapper.Map(y, weigher);
                        Weighers.Add(weigher);
                        //_oldNewWeigherPairs.Add(weigher, y);
                    }));
            SiloManager.GetList(x => x.Enabled.GetBool()).ForEach(
                x => x.Do(
                    y =>
                    {
                        Silo silo = new Silo();
                        Mapper.Map(y, silo);
                        silo.Stock = y.Stock;
                        foreach (var s in y.AvailableStocks)
                        {
                            silo.AvailableStocks.Add(s);
                        }
                        Silos.Add(silo);
                        //_oldNewWeigherPairs.Add(weigher, y);
                    }));
            weigherDataGrid.ItemsSource = new ObservableCollection<Weigher>(Weighers);
            siloDataGrid.ItemsSource = new ObservableCollection<Silo>(Silos);
            Mapper.Map(Infrastructure.Main.Settings, Settings);
            Mapper.Map(Infrastructure.Main.TransferBelt, TransferBandı);
            Mapper.Map(Infrastructure.Main.AggregateBunker, BekletmeBunkeri);
            Mapper.Map(Infrastructure.Main.Bucket, Kova);
            Mapper.Map(Infrastructure.Main.Mixer, Mikser);
            Infrastructure.Main.Mixer.Do(x => Mapper.Map(x.AllMixerGates.FirstOrDefault(y => y.GateNo == 1), Kapak1));
            Infrastructure.Main.Mixer.Do(x => Mapper.Map(x.AllMixerGates.FirstOrDefault(y => y.GateNo == 2), Kapak2));
            CheckVisibilities();
            Weighers.ForEach(x => x.PropertyChanged += PropertyChanged);
            Silos.ForEach(x => x.PropertyChanged += PropertyChanged);
            Settings.PropertyChanged += PropertyChanged;
            TransferBandı.PropertyChanged += PropertyChanged;
            BekletmeBunkeri.PropertyChanged += PropertyChanged;
            Kova.PropertyChanged += PropertyChanged;
            Mikser.PropertyChanged += PropertyChanged;
            Kapak1.PropertyChanged += PropertyChanged;
            Kapak2.PropertyChanged += PropertyChanged;
        }

        private void Apply()
        {
            if (!Changed)
                return;
            TransferBandı.DoIf(obj => IsChanged(obj), obj => { Infrastructure.Main.HandleExceptions(() => TransferManager.Try(x => x.Update(TransferBandı, Infrastructure.Main.TransferBelt))); RemoveFromChangedList(obj); });
            BekletmeBunkeri.DoIf(obj => IsChanged(obj), obj => { Infrastructure.Main.HandleExceptions(() => AggBunkerManager.Try(x => x.Update(BekletmeBunkeri, Infrastructure.Main.AggregateBunker))); RemoveFromChangedList(obj); });
            Kova.DoIf(obj => IsChanged(obj), obj => { Infrastructure.Main.HandleExceptions(() => BucketManager.Try(x => x.Update(Kova, Infrastructure.Main.Bucket))); RemoveFromChangedList(obj); });
            Mikser.DoIf(obj => IsChanged(obj), obj => { Infrastructure.Main.HandleExceptions(() => MixerManager.Try(x => x.Update(Mikser, Infrastructure.Main.Mixer))); RemoveFromChangedList(obj); });
            Kapak1.DoIf(obj => IsChanged(obj), obj => { Infrastructure.Main.HandleExceptions(() => MixerGateManager.Try(x => x.Update(Kapak1, Infrastructure.Main.Mixer.AllMixerGates.FirstOrDefault(y => y.GateNo == 1)))); RemoveFromChangedList(obj); });
            Kapak2.DoIf(obj => IsChanged(obj), obj => { Infrastructure.Main.HandleExceptions(() => MixerGateManager.Try(x => x.Update(Kapak2, Infrastructure.Main.Mixer.AllMixerGates.FirstOrDefault(y => y.GateNo == 2)))); RemoveFromChangedList(obj); });
            Infrastructure.Main.HandleExceptions(() => Silos.ForEach(x => x.DoIf(x1 => IsChanged(x1), x1 => x.Do(y => { SiloManager.Update(y, SiloManager.Get(z => z.SiloId == y.SiloId)); RemoveFromChangedList(x); }))));
            Infrastructure.Main.HandleExceptions(() => Weighers.ForEach(x => x.DoIf(x1 => IsChanged(x1), x1 => x.Do(y => { WeigherManager.Update(y, WeigherManager.Get(z => z.WegId == y.WegId)); RemoveFromChangedList(x); }))));
            Settings.DoIf(obj => IsChanged(obj), obj => { Infrastructure.Main.HandleExceptions(() => SettingsManager.Try(x => x.Update(Settings, Mapper.Map<GeneralSettingsDTO>(Infrastructure.Main.Settings)))); RemoveFromChangedList(obj); });
            Changed = false;
        }
        private bool IsChanged(object obj)
        {
            bool result = false;
            obj.DoIf(x => _changedObjects.Contains(x), x => result = true);
            return result;
        }
        private void RemoveFromChangedList(object obj)
        {
            obj.DoIf(x => _changedObjects.Contains(x), x => _changedObjects.Remove(x));
        }
        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Changed = true;
            _changedObjects.DoIf(x => !x.Contains(sender), x => x.Add(sender));
        }

        private void CheckVisibilities()
        {
            Mikser.DoIfElse(x => x.GateCnt >= 2, x =>
            {
                Kapak2Visibility = Visibility.Visible;
            }, x =>
            {
                Kapak2Visibility = Visibility.Collapsed;
            });
            Settings.DoIfElse(x => x.System == 0,
                            x =>
                            {
                                BekletmeBunkeriVisibility = Visibility.Visible;
                                TransferBandıVisibility = Visibility.Visible;
                                KovaVisibility = Visibility.Collapsed;
                            }, x => x.DoIfElse(y => y.System == 1, y =>
                            {
                                BekletmeBunkeriVisibility = Visibility.Collapsed;
                                TransferBandıVisibility = Visibility.Collapsed;
                                KovaVisibility = Visibility.Visible;
                            }, y => y.DoIfElse(z => z.System == 2, z =>
                            {
                                BekletmeBunkeriVisibility = Visibility.Collapsed;
                                TransferBandıVisibility = Visibility.Visible;
                                KovaVisibility = Visibility.Collapsed;
                            }, z =>
                            {
                                BekletmeBunkeriVisibility = Visibility.Collapsed;
                                TransferBandıVisibility = Visibility.Visible;
                                KovaVisibility = Visibility.Collapsed;
                            })));
        }

        //private void weigherDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        //{
        //    Weigher weigher = e.Row.Item as Weigher;
        //    weigher.Do(x => WeigherManager.Update(weigher, WeigherManager.Get(y => y.WegId == weigher.WegId)));
        //}

        //private void siloDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        //{
        //    Silo silo = e.Row.Item as Silo;
        //    silo.Do(x => SiloManager.Update(silo, SiloManager.Get(y => y.SiloId== silo.SiloId)));
        //}

        private void UygulaButtonClicked(object sender, RoutedEventArgs e)
        {
            Apply();
        }
        private void TamamButtonClicked(object sender, RoutedEventArgs e)
        {
            Apply();
            this.Close();
        }
        private void İptalButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
