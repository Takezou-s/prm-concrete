using Extensions;
using Promax.Business;
using Promax.Core;
using Promax.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility.Binding;
using VirtualPLC;

namespace Promax.Process
{
    public class ProdControlVariables : VirtualPLCObject
    {
        public VirtualPLCProperty TümMalzemelerAlındıProperty { get; private set; }
        public VirtualPLCProperty TümMalzemelerBoşaltıldıProperty { get; private set; }
        public VirtualPLCProperty KarıştırıldıProperty { get; private set; }
        public VirtualPLCProperty BoşaltProperty { get; private set; }
        public VirtualPLCProperty KarıştırBaşlatıldıProperty { get; private set; }
        public VirtualPLCProperty KarıştırProperty { get; private set; }
        public VirtualPLCProperty BoşaltımBaşlatıldıProperty { get; private set; }
        public VirtualPLCProperty PeriyotProperty { get; private set; }
        public VirtualPLCProperty İstenenPeriyotProperty { get; private set; }
        public VirtualPLCProperty ProductProperty { get; private set; }
        public VirtualPLCProperty SenaryoProperty { get; private set; }
        public VirtualPLCProperty AltSenaryoProperty { get; private set; }

        public bool TümMalzemelerAlındı { get => (bool)GetValue(TümMalzemelerAlındıProperty); set => SetValue(TümMalzemelerAlındıProperty, value); }
        public bool TümMalzemelerBoşaltıldı { get => (bool)GetValue(TümMalzemelerBoşaltıldıProperty); set => SetValue(TümMalzemelerBoşaltıldıProperty, value); }
        public bool Karıştırıldı { get => (bool)GetValue(KarıştırıldıProperty); set => SetValue(KarıştırıldıProperty, value); }
        public bool Boşalt { get => (bool)GetValue(BoşaltProperty); set => SetValue(BoşaltProperty, value); }
        public bool KarıştırBaşlatıldı { get => (bool)GetValue(KarıştırBaşlatıldıProperty); set => SetValue(KarıştırBaşlatıldıProperty, value); }
        public bool Karıştır { get => (bool)GetValue(KarıştırProperty); set => SetValue(KarıştırProperty, value); }
        public bool BoşaltımBaşlatıldı { get => (bool)GetValue(BoşaltımBaşlatıldıProperty); set => SetValue(BoşaltımBaşlatıldıProperty, value); }
        public int Periyot { get => (int)GetValue(PeriyotProperty); set => SetValue(PeriyotProperty, value); }
        public int İstenenPeriyot { get => (int)GetValue(İstenenPeriyotProperty); set => SetValue(İstenenPeriyotProperty, value); }
        public Product Product { get => (Product)GetValue(ProductProperty); set => SetValue(ProductProperty, value); }
        public int Senaryo { get => (int)GetValue(SenaryoProperty); set => SetValue(SenaryoProperty, value); }
        public int AltSenaryo { get => (int)GetValue(AltSenaryoProperty); set => SetValue(AltSenaryoProperty, value); }

        public ProdControlVariables(VirtualController controller) : base(controller)
        {
            TümMalzemelerAlındıProperty = VirtualPLCProperty.Register(nameof(TümMalzemelerAlındı), typeof(bool), this, false, true, false);
            TümMalzemelerBoşaltıldıProperty = VirtualPLCProperty.Register(nameof(TümMalzemelerBoşaltıldı), typeof(bool), this, false, true, false);
            KarıştırıldıProperty = VirtualPLCProperty.Register(nameof(Karıştırıldı), typeof(bool), this, false, true, false);
            BoşaltProperty = VirtualPLCProperty.Register(nameof(Boşalt), typeof(bool), this, false, true, false);
            KarıştırBaşlatıldıProperty = VirtualPLCProperty.Register(nameof(KarıştırBaşlatıldı), typeof(bool), this, false, true, false);
            KarıştırProperty = VirtualPLCProperty.Register(nameof(Karıştır), typeof(bool), this, false, true, false);
            BoşaltımBaşlatıldıProperty = VirtualPLCProperty.Register(nameof(BoşaltımBaşlatıldı), typeof(bool), this, false, true, false);
            PeriyotProperty = VirtualPLCProperty.Register(nameof(Periyot), typeof(int), this, false, true, false);
            İstenenPeriyotProperty = VirtualPLCProperty.Register(nameof(İstenenPeriyot), typeof(int), this, false, true, false);
            ProductProperty = new VirtualPLCPropertyBuilder(this).Name(nameof(Product)).Type(typeof(Product)).Retain(true).Get();
            SenaryoProperty = new VirtualPLCPropertyBuilder(this).Name(nameof(Senaryo)).Type(typeof(int)).Input(true).Retain(true).Get();
            AltSenaryoProperty = new VirtualPLCPropertyBuilder(this).Name(nameof(AltSenaryo)).Type(typeof(int)).Retain(true).Get();
        }
    }
    public class ConcreteController : VirtualController
    {
        public ProdControlVariables ProdVariables { get; set; }
        private MyBinding _binding = new MyBinding();
        private List<IMalzemeAl> _malzemeAlanlar = new List<IMalzemeAl>();
        private List<IMalzemeBoşalt> _malzemeBoşaltanlar = new List<IMalzemeBoşalt>();
        private List<StockController> _stockControllers = new List<StockController>();
        private Dictionary<Silo, SiloController> _siloControllers = new Dictionary<Silo, SiloController>();
        private Container<Stock> _stockContainer;
        private SiloInitializer _siloInitializer;
        private IBatchedStockManager _batchedStockManager;
        private IProductManager _productManager;
        #region SiloControllers
        public SiloController Agrega11 { get; private set; }
        public SiloController Agrega12 { get; private set; }
        public SiloController Agrega13 { get; private set; }
        public SiloController Agrega14 { get; private set; }
        public SiloController Agrega15 { get; private set; }
        public SiloController Agrega16 { get; private set; }
        public SiloController Agrega17 { get; private set; }
        public SiloController Agrega18 { get; private set; }

        public SiloController Agrega21 { get; private set; }
        public SiloController Agrega22 { get; private set; }
        public SiloController Agrega23 { get; private set; }
        public SiloController Agrega24 { get; private set; }
        public SiloController Agrega25 { get; private set; }
        public SiloController Agrega26 { get; private set; }
        public SiloController Agrega27 { get; private set; }
        public SiloController Agrega28 { get; private set; }

        public SiloController Çimento31 { get; private set; }
        public SiloController Çimento32 { get; private set; }
        public SiloController Çimento33 { get; private set; }
        public SiloController Çimento34 { get; private set; }

        public SiloController Çimento41 { get; private set; }
        public SiloController Çimento42 { get; private set; }
        public SiloController Çimento43 { get; private set; }
        public SiloController Çimento44 { get; private set; }

        public SiloController Su51 { get; private set; }
        public SiloController Su52 { get; private set; }
        public SiloController Su53 { get; private set; }
        public SiloController Su54 { get; private set; }

        public SiloController Su61 { get; private set; }
        public SiloController Su62 { get; private set; }
        public SiloController Su63 { get; private set; }
        public SiloController Su64 { get; private set; }

        public SiloController Katkı71 { get; private set; }
        public SiloController Katkı72 { get; private set; }
        public SiloController Katkı73 { get; private set; }
        public SiloController Katkı74 { get; private set; }

        public SiloController Katkı81 { get; private set; }
        public SiloController Katkı82 { get; private set; }
        public SiloController Katkı83 { get; private set; }
        public SiloController Katkı84 { get; private set; }
        #endregion
        #region KantarControllers
        public KantarController TartıBandı1 { get; private set; }
        public KantarController TartıBandı2 { get; private set; }
        public KantarController ÇimentoBunkeri1 { get; private set; }
        public KantarController ÇimentoBunkeri2 { get; private set; }
        public KantarController SuBunkeri1 { get; private set; }
        public KantarController SuBunkeri2 { get; private set; }
        public KantarController KatkıBunkeri1 { get; private set; }
        public KantarController KatkıBunkeri2 { get; private set; }
        #endregion
        public SaveBatchController SaveBatchController { get; private set; }
        public IEnumerable<IMalzemeAl> MalzemeAlanlar => _malzemeAlanlar;
        public IEnumerable<IMalzemeBoşalt> MalzemeBoşaltanlar => _malzemeBoşaltanlar;
        public IKarıştır Mikser { get; private set; }
        

        public bool TümMalzemelerAlındı { get => ProdVariables.TümMalzemelerAlındı; private set => ProdVariables.TümMalzemelerAlındı = value; }
        public bool TümMalzemelerBoşaltıldı { get => ProdVariables.TümMalzemelerBoşaltıldı; private set => ProdVariables.TümMalzemelerBoşaltıldı = value; }
        public bool Karıştırıldı { get => ProdVariables.Karıştırıldı; private set => ProdVariables.Karıştırıldı = value; }
        public bool Boşalt { get => ProdVariables.Boşalt; private set => ProdVariables.Boşalt = value; }
        public bool KarıştırBaşlatıldı { get => ProdVariables.KarıştırBaşlatıldı; private set => ProdVariables.KarıştırBaşlatıldı = value; }
        public bool Karıştır { get => ProdVariables.Karıştır; private set => ProdVariables.Karıştır = value; }
        public bool BoşaltımBaşlatıldı { get => ProdVariables.BoşaltımBaşlatıldı; private set => ProdVariables.BoşaltımBaşlatıldı = value; }
        public int Periyot { get => ProdVariables.Periyot; private set => ProdVariables.Periyot = value; }
        public int İstenenPeriyot { get => ProdVariables.İstenenPeriyot; private set => ProdVariables.İstenenPeriyot = value; }
        public Product Product { get => ProdVariables.Product; private set => ProdVariables.Product = value; }
        public int Senaryo { get => ProdVariables.Senaryo; set => ProdVariables.Senaryo = value; }
        public int AltSenaryo { get => ProdVariables.AltSenaryo; set => ProdVariables.AltSenaryo = value; }
        public bool ÜretimVerilebilir { get => Senaryo == 0; }
        public User User { get; set; }
        public ConcreteController(string path) : base(true, path)
        {

        }

        public ConcreteController(string path, Container<Stock> stockContainer, SiloInitializer siloInitializer, IBatchedStockManager batchedStockManager, IProductManager productManager) : this(true, path, stockContainer, siloInitializer, batchedStockManager, productManager)
        {
        }

        public ConcreteController(bool init, string path, Container<Stock> stockContainer, SiloInitializer siloInitializer, IBatchedStockManager batchedStockManager, IProductManager productManager) : base(init, path)
        {
            _stockContainer = stockContainer;
            _siloInitializer = siloInitializer;
            _batchedStockManager = batchedStockManager;
            _productManager = productManager;
        }

        protected override void InitImp()
        {
            ProdVariables = new ProdControlVariables(this);

            InitSiloControllers();
            InitStockControllers();
            InitKantarControllers();
            SaveBatchController = new SaveBatchController(this, _siloControllers, _batchedStockManager);
        }
        private void InitKantarControllers()
        {
            TartıBandı1 = new KantarController(this, "Weg1", "Weg1", ProdVariables.İstenenPeriyotProperty);
            TartıBandı2 = new KantarController(this, "Weg2", "Weg2", ProdVariables.İstenenPeriyotProperty);
            ÇimentoBunkeri1 = new KantarController(this, "Weg3", "Weg3", ProdVariables.İstenenPeriyotProperty);
            ÇimentoBunkeri2 = new KantarController(this, "Weg4", "Weg4", ProdVariables.İstenenPeriyotProperty);
            SuBunkeri1 = new KantarController(this, "Weg5", "Weg5", ProdVariables.İstenenPeriyotProperty);
            SuBunkeri2 = new KantarController(this, "Weg6", "Weg6", ProdVariables.İstenenPeriyotProperty);
            KatkıBunkeri1 = new KantarController(this, "Weg7", "Weg7", ProdVariables.İstenenPeriyotProperty);
            KatkıBunkeri2 = new KantarController(this, "Weg8", "Weg8", ProdVariables.İstenenPeriyotProperty);

            TartıBandı1.Silolar.Add(Agrega11);
            TartıBandı1.Silolar.Add(Agrega12);
            TartıBandı1.Silolar.Add(Agrega13);
            TartıBandı1.Silolar.Add(Agrega14);
            TartıBandı1.Silolar.Add(Agrega15);
            TartıBandı1.Silolar.Add(Agrega16);
            TartıBandı1.Silolar.Add(Agrega17);
            TartıBandı1.Silolar.Add(Agrega18);

            TartıBandı2.Silolar.Add(Agrega21);
            TartıBandı2.Silolar.Add(Agrega22);
            TartıBandı2.Silolar.Add(Agrega23);
            TartıBandı2.Silolar.Add(Agrega24);
            TartıBandı2.Silolar.Add(Agrega25);
            TartıBandı2.Silolar.Add(Agrega26);
            TartıBandı2.Silolar.Add(Agrega27);
            TartıBandı2.Silolar.Add(Agrega28);

            ÇimentoBunkeri1.Silolar.Add(Çimento31);
            ÇimentoBunkeri1.Silolar.Add(Çimento32);
            ÇimentoBunkeri1.Silolar.Add(Çimento33);
            ÇimentoBunkeri1.Silolar.Add(Çimento34);

            ÇimentoBunkeri2.Silolar.Add(Çimento41);
            ÇimentoBunkeri2.Silolar.Add(Çimento42);
            ÇimentoBunkeri2.Silolar.Add(Çimento43);
            ÇimentoBunkeri2.Silolar.Add(Çimento44);

            SuBunkeri1.Silolar.Add(Su51);
            SuBunkeri1.Silolar.Add(Su52);
            SuBunkeri1.Silolar.Add(Su53);
            SuBunkeri1.Silolar.Add(Su54);

            SuBunkeri2.Silolar.Add(Su61);
            SuBunkeri2.Silolar.Add(Su62);
            SuBunkeri2.Silolar.Add(Su63);
            SuBunkeri2.Silolar.Add(Su64);

            KatkıBunkeri1.Silolar.Add(Katkı71);
            KatkıBunkeri1.Silolar.Add(Katkı72);
            KatkıBunkeri1.Silolar.Add(Katkı73);
            KatkıBunkeri1.Silolar.Add(Katkı74);

            KatkıBunkeri2.Silolar.Add(Katkı81);
            KatkıBunkeri2.Silolar.Add(Katkı82);
            KatkıBunkeri2.Silolar.Add(Katkı83);
            KatkıBunkeri2.Silolar.Add(Katkı84);
        }
        #region SiloController
        private void InitSiloControllers()
        {
            Agrega11 = CreateSiloController("AGG11");
            Agrega12 = CreateSiloController("AGG12");
            Agrega13 = CreateSiloController("AGG13");
            Agrega14 = CreateSiloController("AGG14");
            Agrega15 = CreateSiloController("AGG15");
            Agrega16 = CreateSiloController("AGG16");
            Agrega17 = CreateSiloController("AGG17");
            Agrega18 = CreateSiloController("AGG18");

            Agrega21 = CreateSiloController("AGG21");
            Agrega22 = CreateSiloController("AGG22");
            Agrega23 = CreateSiloController("AGG23");
            Agrega24 = CreateSiloController("AGG24");
            Agrega25 = CreateSiloController("AGG25");
            Agrega26 = CreateSiloController("AGG26");
            Agrega27 = CreateSiloController("AGG27");
            Agrega28 = CreateSiloController("AGG28");

            Çimento31 = CreateSiloController("CEM31");
            Çimento32 = CreateSiloController("CEM32");
            Çimento33 = CreateSiloController("CEM33");
            Çimento34 = CreateSiloController("CEM34");

            Çimento41 = CreateSiloController("CEM41");
            Çimento42 = CreateSiloController("CEM42");
            Çimento43 = CreateSiloController("CEM43");
            Çimento44 = CreateSiloController("CEM44");

            Su51 = CreateSiloController("WTR51");
            Su52 = CreateSiloController("WTR52");
            Su53 = CreateSiloController("WTR53");
            Su54 = CreateSiloController("WTR54");

            Su61 = CreateSiloController("WTR61");
            Su62 = CreateSiloController("WTR62");
            Su63 = CreateSiloController("WTR63");
            Su64 = CreateSiloController("WTR64");

            Katkı71 = CreateSiloController("ADV71");
            Katkı72 = CreateSiloController("ADV72");
            Katkı73 = CreateSiloController("ADV73");
            Katkı74 = CreateSiloController("ADV74");

            Katkı81 = CreateSiloController("ADV81");
            Katkı82 = CreateSiloController("ADV82");
            Katkı83 = CreateSiloController("ADV83");
            Katkı84 = CreateSiloController("ADV84");
        }
        private SiloController CreateSiloController(string uniqueName)
        {
            var siloController = new SiloController(this, uniqueName, _siloInitializer);
            var silo = _siloInitializer.SiloContainer.Objects.FirstOrDefault(x => x.UniqueName == uniqueName);
            if (_siloControllers.ContainsKey(silo))
                _siloControllers.Remove(silo);
            _siloControllers.Add(silo, siloController);
            return siloController;
        } 
        #endregion
        #region StockController
        /// <summary>
        /// StockController'ları initialize eder.
        /// </summary>
        private void InitStockControllers()
        {
            foreach (var stock in _stockContainer.Objects)
            {
                //stock null ise bir sonrakina geç.
                if (stock == null)
                    continue;
                //Bir StockController objesi oluşturulur, RuntimeObjects'e eklenir. Method'dan geri dönen bu objenin SiloController'ları düzenlenir.
                CreateStockControllerFromStock(stock);
            }
            _stockContainer.ObjectUnregistered += (o, e) => { RemoveStockController(e); };
            _stockContainer.ObjectRegistered += (o, e) => { CreateStockControllerFromStock(e); };
        }
        /// <summary>
        /// Stock nesnesinden StockController nesnesi oluşturur, RuntimeObjects'e ekler, SiloController'ları ayarlar.
        /// </summary>
        /// <param name="stock"></param>
        private void CreateStockControllerFromStock(Stock stock)
        {
            StockController stockController = AddStockController(stock);
            ArrangeSiloControllers(stockController);
            //stock'un Silos property'si değişirse stockController'ın SiloController'larını ayarlayan binding oluşturulur.
            _binding.CreateBinding().Behaviour(MyBindingBehaviour.Invoke).Source(stock).SourceProperty(nameof(stock.Silos)).WhenSourcePropertyChanged(() => ArrangeSiloControllers(stockController));
        }
        /// <summary>
        /// İlgili Stock nesnesinin StockController'ını sistemden çıkarır.
        /// </summary>
        /// <param name="stock"></param>
        private void RemoveStockController(Stock stock)
        {
            if (stock == null || _stockControllers.FirstOrDefault(x => x.Stock == stock) == null)
                return;
            var stockController = _stockControllers.FirstOrDefault(x => x.Stock == stock);
            stockController.Stock = null;
            _stockControllers.Remove(stockController);
            _binding.RemoveBindingOfSource(stock);
            RemoveRuntimeObject("StockController" + stock.StockId, stockController);
        }
        /// <summary>
        /// Stock nesnesinden StockController oluşturur, RuntimeObjects'e ekler.
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        private StockController AddStockController(Stock stock)
        {
            if (stock == null)
                return null;
            //StockController oluştur.
            var stockController = new StockController(this);
            //StockController'ın Stock property'sini setle.
            stockController.Stock = stock;
            //RuntimeObject olarak ekle.
            AddRuntimeObject("StockController" + stock.StockId, stockController);
            //_stockControllers listesine ekle.
            _stockControllers.Add(stockController);
            return stockController;
        }
        /// <summary>
        /// StockController nesnesinin SiloController'larını düzenler.
        /// </summary>
        /// <param name="stockController"></param>
        private void ArrangeSiloControllers(StockController stockController)
        {
            if (stockController == null)
                return;
            //stockController'ın sahibi SiloController'ların propertyleri temizlenir.
            _siloControllers.Values.Where(x => x.StockController == stockController).ToList().ForEach(x => x.StockController = null);
            //stockController'ın SiloController'ları temizlenir.
            stockController.ClearSiloControllers();
            //stock objesinin içerisindeki silolara bakarak SiloController objeleri StockController'lara eklenir.
            foreach (var silo in stockController.Stock.Silos)
            {
                //silo null veya _siloControllers'da bulunmuyorsa veya SiloControllers null ise bir sonraki siloya geç.
                if (silo == null || !_siloControllers.ContainsKey(silo) || _siloControllers[silo] == null)
                    continue;
                //_siloControllers'dan ilgili SiloController'ı al.
                var siloController = _siloControllers[silo];
                //stockController'a siloController'ı ekle.
                stockController.AddSiloController(siloController);
            }
        } 
        #endregion

        int number;
        bool a;
        protected override void Process()
        {
            //Thread.Sleep(2500);
            //ÜretimVariables.Periyot++;
            //if (a)
            //{
            //    number++;
            //    var abc = new StockController(this);
            //    AddRuntimeObject("Stock" + number, abc);
            //    a = false;
            //}
            YeniProses();
        }
        public void ÜretimBaşlat(Product product)
        {
            if (!ÜretimVerilebilir || product == null)
                return;
            Product = product;
            Senaryo = 1;
        }
        private void YeniProses()
        {
            if(Senaryo == 1)
            {
                _stockControllers.ForEach(x => x.ÜretimBaşıİstenenHesapla(Product));
                İstenenPeriyot = Product.AimBatch;
                Senaryo = 2;
            }
            else if (Senaryo == 2)
            {
                MalzemeAl();
                if(AltSenaryo == 0)
                {
                    if (TümMalzemelerAlındı)
                        AltSenaryo = 1;
                }
                else if(AltSenaryo == 1)
                {
                    MalzemeBoşalt();
                    if(TümMalzemelerBoşaltıldı)
                    {
                        Periyot++;
                        Product.RtmBatch = Periyot;
                        Karıştır = true;
                        AltSenaryo = 2;
                        if(Periyot >= İstenenPeriyot)
                        {
                            Product.PosStatus = ProductionStatus.NormalBitiş;
                            _productManager.Update(Product, Product);
                            Senaryo = 0;
                        }
                    }
                }
            }
            if(Karıştır)
            {
                MikserKarıştır();
                if(Karıştırıldı)
                {
                    Karıştır = false;
                    AltSenaryo = 0;
                }
            }
        }
        private void Eskiproses()
        {
            //if (Periyot >= İstenenPeriyot)
            //    -Bitti -
            //MalzemeAl();
            //if (TümMalzemelerAlındı)
            //{
            //    if (!BoşaltımBaşlatıldı)
            //    {
            //        Boşalt = true;
            //        BoşaltımBaşlatıldı = true;
            //    }
            //}
            //if (Boşalt)
            //{
            //    if (!TümMalzemelerBoşaltıldı)
            //    {
            //        MalzemeBoşalt();
            //    }
            //    else
            //    {
            //        if (!KarıştırBaşlatıldı)
            //        {
            //            Karıştır = true;
            //            KarıştırBaşlatıldı = true;
            //        }
            //    }
            //}
            //if (Karıştır)
            //{
            //    if (!Karıştırıldı)
            //    {
            //        MikserKarıştır();
            //    }
            //    else
            //    {
            //        BoşaltımBaşlatıldı = false;
            //        Boşalt = false;
            //        KarıştırBaşlatıldı = false;
            //        Karıştır = false;
            //        Periyot++;
            //        _malzemeBoşaltanlar.ForEach(x => x.ResetMalzemeBoşalt());
            //    }
            //}
        }

        private void MalzemeAl()
        {
            bool malzemelerAlındı = true;
            foreach (var item in MalzemeAlanlar)
            {
                if (!item.MalzemeAlındı)
                    malzemelerAlındı = false;
                item.MalzemeAl();
            }
            TümMalzemelerAlındı = malzemelerAlındı;
        }

        private void MalzemeBoşalt()
        {
            bool malzemelerBoşaltıldı = true;
            foreach (var item in MalzemeBoşaltanlar)
            {
                if (!item.MalzemeBoşaltıldı)
                    malzemelerBoşaltıldı = false;
                item.MalzemeBoşalt();
            }
            TümMalzemelerBoşaltıldı = malzemelerBoşaltıldı;
        }

        private void MikserKarıştır()
        {
            Mikser.Karıştır();
            Karıştırıldı = Mikser.Karıştırıldı;
        }
    }
}
