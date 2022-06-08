using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VirtualPLC;

namespace Promax.Process
{
    public class ÜretimVariables : VirtualPLCObject
    {
        public VirtualPLCProperty TümMalzemelerAlındıProperty { get; private set; }
        public VirtualPLCProperty TümMalzemelerBoşaltıldıProperty { get; private set; }
        public VirtualPLCProperty KarıştırıldıProperty { get; private set; }
        public VirtualPLCProperty BoşaltProperty { get; private set; }
        public VirtualPLCProperty KarıştırBaşlatıldıProperty { get; private set; }
        public VirtualPLCProperty KarıştırProperty { get; private set; }
        public VirtualPLCProperty BoşaltımBaşlatıldıProperty { get; private set; }
        public VirtualPLCProperty PeriyotProperty { get; set; }
        public VirtualPLCProperty İstenenPeriyotProperty { get; set; }

        public bool TümMalzemelerAlındı { get => (bool)GetValue(TümMalzemelerAlındıProperty); set => SetValue(TümMalzemelerAlındıProperty, value); }
        public bool TümMalzemelerBoşaltıldı { get => (bool)GetValue(TümMalzemelerBoşaltıldıProperty); set => SetValue(TümMalzemelerBoşaltıldıProperty, value); }
        public bool Karıştırıldı { get => (bool)GetValue(KarıştırıldıProperty); set => SetValue(KarıştırıldıProperty, value); }
        public bool Boşalt { get => (bool)GetValue(BoşaltProperty); set => SetValue(BoşaltProperty, value); }
        public bool KarıştırBaşlatıldı { get => (bool)GetValue(KarıştırBaşlatıldıProperty); set => SetValue(KarıştırBaşlatıldıProperty, value); }
        public bool Karıştır { get => (bool)GetValue(KarıştırProperty); set => SetValue(KarıştırProperty, value); }
        public bool BoşaltımBaşlatıldı { get => (bool)GetValue(BoşaltımBaşlatıldıProperty); set => SetValue(BoşaltımBaşlatıldıProperty, value); }
        public int Periyot { get => (int)GetValue(PeriyotProperty); set => SetValue(PeriyotProperty, value); }
        public int İstenenPeriyot { get => (int)GetValue(İstenenPeriyotProperty); set => SetValue(İstenenPeriyotProperty, value); }

        public ÜretimVariables(VirtualController controller) : base(controller)
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
        }
    }
    public class ConcreteController : VirtualController
    {
        private List<IMalzemeAl> _malzemeAlanlar = new List<IMalzemeAl>();
        private List<IMalzemeBoşalt> _malzemeBoşaltanlar = new List<IMalzemeBoşalt>();

        public IEnumerable<IMalzemeAl> MalzemeAlanlar => _malzemeAlanlar;
        public IEnumerable<IMalzemeBoşalt> MalzemeBoşaltanlar => _malzemeBoşaltanlar;
        public IKarıştır Mikser { get; private set; }
        public ÜretimVariables ÜretimVariables { get; set; }

        public bool TümMalzemelerAlındı { get; private set; }
        public bool TümMalzemelerBoşaltıldı { get; private set; }
        public bool Karıştırıldı { get; private set; }
        public bool Boşalt { get; private set; }
        public bool KarıştırBaşlatıldı { get; private set; }
        public bool Karıştır { get; private set; }
        public bool BoşaltımBaşlatıldı { get; private set; }
        public int Periyot { get; private set; }
        public int İstenenPeriyot { get; private set; }

        public ConcreteController(string path) : base(path)
        {
        }

        public ConcreteController(bool init, string path) : base(init, path)
        {
        }

        protected override void InitImp()
        {
            //ÜretimVariables = new ÜretimVariables(this);
        }

        protected override void Process()
        {
            //Thread.Sleep(2500);
            //ÜretimVariables.Periyot++; 
            //if (Periyot >= İstenenPeriyot)
            //    -Bitti -
            MalzemeAl();
            if (TümMalzemelerAlındı)
            {
                if (!BoşaltımBaşlatıldı)
                {
                    Boşalt = true;
                    BoşaltımBaşlatıldı = true;
                }
            }
            if (Boşalt)
            {
                if (!TümMalzemelerBoşaltıldı)
                {
                    MalzemeBoşalt();
                }
                else
                {
                    if (!KarıştırBaşlatıldı)
                    {
                        Karıştır = true;
                        KarıştırBaşlatıldı = true;
                    }
                }
            }
            if (Karıştır)
            {
                if (!Karıştırıldı)
                {
                    MikserKarıştır();
                }
                else
                {
                    BoşaltımBaşlatıldı = false;
                    Boşalt = false;
                    KarıştırBaşlatıldı = false;
                    Karıştır = false;
                    Periyot++;
                    _malzemeBoşaltanlar.ForEach(x => x.ResetMalzemeBoşalt();
                }
            }
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
