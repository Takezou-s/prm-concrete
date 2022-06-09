using Extensions;
using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;
using VirtualPLC;

namespace Promax.Process
{
    /// <summary>
    /// Kantarların çalışmasını kontrol eder.
    /// </summary>
    public class KantarController : VirtualPLCObject, IKantar, IVariableOwner, ICommander
    {
        #region VirtualPLCProperty'ler
        public VirtualPLCProperty MalzemeAlındıProperty { get; private set; }
        public VirtualPLCProperty MalzemeBoşaltıldıProperty { get; private set; }
        public VirtualPLCProperty MalzemeBoşaltSenaryoProperty { get; private set; }
        public VirtualPLCProperty EjectedInfoProperty { get; private set; }
        public VirtualPLCProperty İstenenPeriyotProperty { get; private set; }
        public VirtualPLCProperty MalzemeAlTamamlananPeriyotProperty { get; private set; }
        public VirtualPLCProperty MalzemeBoşaltTamamlananPeriyotProperty { get; private set; }
        #endregion
        #region Senaryo adımlarını belirten değişkenler.
        private readonly int _boşaltKomutuSenaryo = 0;
        private readonly int _boşaltıldıİzleSenaryo = 1;
        private readonly int _boşaltıldıResetleSenaryo = 2;
        private readonly int _boşaltıldıResetKontrolSenaryo = 3; 
        #endregion
        /// <summary>
        /// Malzeme boşaltım senaryosunun adımını tutan değişken.
        /// </summary>
        private int MalzemeBoşaltSenaryo { get => (int)GetValue(MalzemeBoşaltSenaryoProperty); set => SetValue(MalzemeBoşaltSenaryoProperty, value); }
        /// <summary>
        /// Siloların StockController'ları.
        /// </summary>
        private List<StockController> Stocks
        {
            get
            {
                var stocks = new List<StockController>();
                Silolar.ForEach(x => x.StockController.DoIf(y => !stocks.Contains(y), y => stocks.Add(y)));
                return stocks;
            }
        }
        /// <summary>
        /// Siloların bulunduğu liste.
        /// </summary>
        public List<SiloController> Silolar { get; set; } = new List<SiloController>();
        /// <summary>
        /// Malzeme alımının tamamlandığını belirtir.
        /// </summary>
        public bool MalzemeAlındı { get => (bool)GetValue(MalzemeAlındıProperty); private set => SetValue(MalzemeAlındıProperty, value); }
        /// <summary>
        /// Malzeme boşaltımının tamamlandığını belirtir.
        /// </summary>
        public bool MalzemeBoşaltıldı { get => (bool)GetValue(MalzemeBoşaltıldıProperty); private set => SetValue(MalzemeBoşaltıldıProperty, value); }
        /// <summary>
        /// Malzeme boşaltımının yapılıyor olduğunu belirtir.
        /// </summary>
        public bool MalzemeBoşaltılıyor { get => MalzemeBoşaltSenaryo == _boşaltıldıİzleSenaryo; }
        /// <summary>
        /// Dışarıdan gelen "Boşaltıldı" bilgisi. Boşalt komutu verildikten sonra bu bilgi takip edilir.
        /// </summary>
        public bool EjectedInfo { get => (bool)GetValue(EjectedInfoProperty); set => SetValue(EjectedInfoProperty, value); }
        /// <summary>
        /// Kaç periyot işlem yapılacağını belirtir.
        /// </summary>
        public int İstenenPeriyot { get => (int)GetValue(İstenenPeriyotProperty); set => SetValue(İstenenPeriyotProperty, value); }
        /// <summary>
        /// Kaç kere malzeme alım işleminin yapıldığını belirtir.
        /// </summary>
        public int MalzemeAlTamamlananPeriyot { get => (int)GetValue(MalzemeAlTamamlananPeriyotProperty); set => SetValue(MalzemeAlTamamlananPeriyotProperty, value); }
        /// <summary>
        /// Kaç kere malzeme boşaltım işleminin yapıldığını belirtir.
        /// </summary>
        public int MalzemeBoşaltTamamlananPeriyot { get => (int)GetValue(MalzemeBoşaltTamamlananPeriyotProperty); private set => SetValue(MalzemeBoşaltTamamlananPeriyotProperty, value); }
        /// <summary>
        /// İstenen sayı kadar malzeme alımının yapıldığını belirtir.
        /// </summary>
        public bool MalzemeAlPeriyotTamamlandı => MalzemeAlTamamlananPeriyot >= İstenenPeriyot;
        /// <summary>
        /// İstenen sayı kadar malzeme boşaltımının yapıldığını belirtir.
        /// </summary>
        public bool MalzemeBoşaltPeriyotTamamlandı => MalzemeBoşaltTamamlananPeriyot >= İstenenPeriyot;


        public KantarController(VirtualController controller, string variableOwnerName, string commanderName) : base(controller)
        {
            VariableOwnerName = variableOwnerName;
            CommanderName = commanderName;
            var builder = new VirtualPLCPropertyBuilder(this);
            MalzemeAlındıProperty = builder.Reset().Name(nameof(MalzemeAlındı)).Type(typeof(bool)).Retain(true).Get();
            MalzemeBoşaltıldıProperty = builder.Reset().Name(nameof(MalzemeBoşaltıldı)).Type(typeof(bool)).Retain(true).Get();
            MalzemeBoşaltSenaryoProperty = builder.Reset().Name(nameof(MalzemeBoşaltSenaryo)).Type(typeof(int)).Retain(true).Get();
            EjectedInfoProperty = builder.Reset().Name(nameof(EjectedInfo)).Type(typeof(bool)).Input(true).Retain( true).Get();
            İstenenPeriyotProperty = builder.Reset().Name(nameof(İstenenPeriyot)).Type(typeof(int)).Retain(true).Get();
            MalzemeAlTamamlananPeriyotProperty = builder.Reset().Name(nameof(MalzemeAlTamamlananPeriyot)).Type(typeof(int)).Retain(true).Get();
            MalzemeBoşaltTamamlananPeriyotProperty = builder.Reset().Name(nameof(MalzemeBoşaltTamamlananPeriyot)).Type(typeof(int)).Retain(true).Get();
            InitVariables();
            InitCommands();
        }
        /// <summary>
        /// Malzeme alma işlemiyle ilgilenen method. Periyot tamamlandıysa, malzeme alımı tamamlanmışsa işlem yapmaz.
        /// </summary>
        public void MalzemeAl()
        {
            //Malzeme al periyodu tamamlanmışsa geri dön.
            if (MalzemeAlPeriyotTamamlandı)
                return;
            //Malzeme alındı ise geri dön.
            if (MalzemeAlındı)
                return;
            //Tüm siloların boşaltım tamamlandı durumuna göre _alındı değişkeninin değeri belirlenir. Boşaltımı bitmemiş ilk silonun MalzemeBoşalt methodu çağırılır.
            bool _alındı = true;
            foreach (var silo in Silolar)
            {
                if (!silo.MalzemeBoşaltıldı)
                {
                    _alındı = false;
                    silo.MalzemeBoşalt();
                    break;
                }
            }
            //Periyot artırılır, malzeme alındı setlenir.
            if (_alındı)
            {
                MalzemeAlındı = true;
                MalzemeAlTamamlananPeriyot++;
                if(!MalzemeAlPeriyotTamamlandı)
                {
                    Stocks.ForEach(x => x.YeniPeriyotHesapla(MalzemeAlTamamlananPeriyot, İstenenPeriyot));
                }
            }
        }
        /// <summary>
        /// Tekrar malzeme almaya hazır hale gelir. Periyot resetlenmez!
        /// </summary>
        public void ResetMalzemeAl()
        {
            foreach (var silo in Silolar)
            {
                silo.ResetMalzemeBoşalt();
            }
            MalzemeAlındı = false;
        }
        /// <summary>
        /// Malzeme boşaltım işlemiyle ilgilenen method. Periyot tamamlandıysa, malzeme boşaltımı tamamlanmışsa işlem yapmaz.
        /// </summary>
        public void MalzemeBoşalt()
        {
            //Boşalt periyodu tamamlandıysa geri dön.
            if (MalzemeBoşaltPeriyotTamamlandı)
                return;
            //Boşaltıldı ise geri dön.
            if (MalzemeBoşaltıldı)
                return;
            //İlk senaryo, boşalt komutu verilir ve bir sonraki senaryo adımına geçilir.
            if(MalzemeBoşaltSenaryo == _boşaltKomutuSenaryo)
            {
                InvokeCommand(CommandNames.EjectCommand);
                MalzemeBoşaltSenaryo = _boşaltıldıİzleSenaryo;
            }
            //Boşaltıldı bilgisi True ise bir sonraki senaryo adımına geçilir.
            else if(MalzemeBoşaltSenaryo == _boşaltıldıİzleSenaryo)
            {
                if (EjectedInfo)
                    MalzemeBoşaltSenaryo = _boşaltıldıResetleSenaryo;
            }
            //Boşaltıldı bilgisinin alındığını belirten komut verilir, bir sonraki senaryo adımına geçilir.
            else if (MalzemeBoşaltSenaryo == _boşaltıldıResetleSenaryo)
            {
                InvokeCommand(CommandNames.EjectedInfoResponse);
                MalzemeBoşaltSenaryo = _boşaltıldıResetKontrolSenaryo;
            }
            //Boşaltıldı bilgisi resetlenmesi başarılıysa malzeme al resetlenir, bu sayede diğer periyodun malzeme alma işlemini yapabilir.
            //Malzeme boşaltıldı setlenir ve periyot bir artırılır.
            else if (MalzemeBoşaltSenaryo == _boşaltıldıResetKontrolSenaryo)
            {
                if (!EjectedInfo)
                {
                    ResetMalzemeAl();
                    MalzemeBoşaltıldı = true;
                    MalzemeBoşaltTamamlananPeriyot++;
                }
            }
        }
        /// <summary>
        /// Tekrar malzeme boşaltmaya hazır hale gelir. Periyot resetlenmez!
        /// </summary>
        public void ResetMalzemeBoşalt()
        {
            MalzemeBoşaltSenaryo = 0;
            MalzemeBoşaltıldı = false;
        }
        #region IVariableOwner
        private ConverterContainer _variableConverters = new ConverterContainer();
        private List<string> _variables = new List<string>();

        public string VariableOwnerName { get; set; }
        IEnumerable<string> IVariableOwner.Variables => _variables;

        IVariableOwner IVariableOwner.SetConverter(string propertyName, IBeeValueConverter converter)
        {
            _variableConverters.SetConverter(propertyName, converter);
            return this;
        }

        IBeeValueConverter IVariableOwner.GetConverterForVariable(string propertyName)
        {
            return _variableConverters.GetConverter(propertyName);
        }
        private void InitVariables()
        {
            _variables.Add(nameof(EjectedInfo));
        }
        #endregion
        #region ICommander
        private Commander _commander = new Commander();

        public string CommanderName { get => ((ICommander)_commander).CommanderName; set => ((ICommander)_commander).CommanderName = value; }
        public IReadOnlyDictionary<string, MyCommand> Commands => ((ICommander)_commander).Commands;

        public void InvokeCommand(string command, object[] parameters = null)
        {
            ((ICommander)_commander).InvokeCommand(command, parameters);
        }
        private void InitCommands()
        {
            _commander.RegisterCommand(CommandNames.EjectCommand);
            _commander.RegisterCommand(CommandNames.EjectedInfoResponse);
        }
        #endregion
    }
}
