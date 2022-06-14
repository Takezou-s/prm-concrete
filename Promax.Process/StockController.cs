using Promax.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;
using VirtualPLC;

namespace Promax.Process
{
    public class StockController : VirtualPLCObject
    {
        private MyBinding _siloBindings = new MyBinding();
        private MyBinding _bindings = new MyBinding();
        private List<SiloController> _siloControllers = new List<SiloController>();

        public VirtualPLCProperty İstenenProperty { get; private set; }
        public VirtualPLCProperty İlaveMiktarProperty { get; private set; }
        public VirtualPLCProperty ToplamİstenenProperty { get; private set; }
        public VirtualPLCProperty ToplamÖlçülenProperty { get; private set; }

        public IEnumerable<SiloController> SiloControllers => _siloControllers;
        public double İstenen { get => (double)GetValue(İstenenProperty); set => SetValue(İstenenProperty, value); }
        public double İlaveMiktar { get => (double)GetValue(İlaveMiktarProperty); set => SetValue(İlaveMiktarProperty, value); }
        public double Toplamİstenen { get => (double)GetValue(ToplamİstenenProperty); set => SetValue(ToplamİstenenProperty, value); }
        public double ToplamÖlçülen { get => (double)GetValue(ToplamÖlçülenProperty); set => SetValue(ToplamÖlçülenProperty, value); }
        public double Ölçülen
        {
            get
            {
                double d = 0;
                _siloControllers.ForEach(x => d += x.Ölçülen);
                return d;
            }
        }
        public double Kalan { get => İstenen - Ölçülen + İlaveMiktar; }
        public Stock Stock { get; set; }

        public StockController(VirtualController controller) : base(controller)
        {
            var builder = new VirtualPLCPropertyBuilder(this);
            İstenenProperty = builder.Reset().Name(nameof(İstenen)).Type(typeof(double)).Retain(true).Output(true).Get();
            İlaveMiktarProperty = builder.Reset().Name(nameof(İlaveMiktar)).Type(typeof(double)).Retain(true).Output(true).Get();
            ToplamİstenenProperty = builder.Reset().Name(nameof(Toplamİstenen)).Type(typeof(double)).Retain(true).Output(true).Get();
            ToplamÖlçülenProperty = builder.Reset().Name(nameof(ToplamÖlçülen)).Type(typeof(double)).Retain(true).Output(true).Get();
            _bindings.CreateBinding().Behaviour(MyBindingBehaviour.Invoke).Source(this).SourceProperty(nameof(Ölçülen)).WhenSourcePropertyChanged(() => OnPropertyChanged(nameof(Kalan)));
            _bindings.CreateBinding().Behaviour(MyBindingBehaviour.Invoke).Source(this).SourceProperty(nameof(İstenen)).WhenSourcePropertyChanged(() => OnPropertyChanged(nameof(Kalan)));
        }
        public void ÜretimBaşıİstenenHesapla(Product product)
        {
            double d = 0;
            product.Recipe.RecipeContents.Where(x => x.Stock == Stock).ToList().ForEach(x => d += x.Quantity * product.Ubm);
            İstenen = d;
            Toplamİstenen = İstenen * product.AimBatch;
        }
        public void YeniPeriyotHesapla(int periyot, int istenenPeriyot)
        {
            var kalanPeriyot = istenenPeriyot - periyot;
            if (kalanPeriyot <= 0)
            {
                kalanPeriyot = 1;
            }
            var toplamKalan = Toplamİstenen - ToplamÖlçülen;
            İstenen = toplamKalan / kalanPeriyot;
        }

        #region Add, Remove, Clear
        public void AddSiloController(SiloController siloController)
        {
            if (_siloControllers.Contains(siloController))
                return;
            _siloControllers.Add(siloController);
            siloController.StockController = this;
            _siloBindings.CreateBinding().Behaviour(MyBindingBehaviour.Invoke).Source(siloController).SourceProperty(nameof(siloController.Ölçülen)).WhenSourcePropertyChanged(() => OnPropertyChanged(nameof(Ölçülen)));
        }

        public void RemoveSiloController(SiloController siloController)
        {
            if (!_siloControllers.Contains(siloController))
                return;
            _siloControllers.Remove(siloController);
            _siloBindings.RemoveBindingOfSource(siloController);
        }

        public void ClearSiloControllers()
        {
            _siloBindings.ClearBindings();
            _siloControllers.Clear();
        }
        #endregion
    }
}
