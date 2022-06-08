using Promax.Core;
using System.Collections.Generic;
using Utility.Binding;
using VirtualPLC;

namespace Promax.Process
{
    public class TransferBandıController : VirtualPLCObject, IMalzemeBoşalt, IVariableOwner, ICommander
    {
        #region VirtualPLCProperty'ler
        public VirtualPLCProperty MalzemeBoşaltıldıProperty { get; private set; }
        public VirtualPLCProperty MalzemeBoşaltSenaryoProperty { get; private set; }
        public VirtualPLCProperty EjectedInfoProperty { get; private set; }
        #endregion
        #region Senaryo adımları
        private readonly int _runKomutuSenaryo = 0;
        private readonly int _kantarBoşaltSenaryo = 1;
        private readonly int _boşaltıldıİzleSenaryo = 2;
        private readonly int _boşaltıldıResetKontrolSenaryo = 3;
        #endregion
        /// <summary>
        /// Malzeme boşaltım senaryosunun adımını tutan değişken.
        /// </summary>
        private int MalzemeBoşaltSenaryo { get => (int)GetValue(MalzemeBoşaltSenaryoProperty); set => SetValue(MalzemeBoşaltSenaryoProperty, value); }
        /// <summary>
        /// Malzeme boşaltımının tamamlandığını belirtir.
        /// </summary>
        public bool MalzemeBoşaltıldı { get => (bool)GetValue(MalzemeBoşaltıldıProperty); private set => SetValue(MalzemeBoşaltıldıProperty, value); }
        /// <summary>
        /// Malzeme boşaltımının yapılıyor olduğunu belirtir.
        /// </summary>
        public bool MalzemeBoşaltılıyor { get => MalzemeBoşaltSenaryo == _boşaltıldıİzleSenaryo || MalzemeBoşaltSenaryo == _kantarBoşaltSenaryo; }
        /// <summary>
        /// Dışarıdan gelen "Boşaltıldı" bilgisi. Boşalt komutu verildikten sonra bu bilgi takip edilir.
        /// </summary>
        public bool EjectedInfo { get => (bool)GetValue(EjectedInfoProperty); set => SetValue(EjectedInfoProperty, value); }
        /// <summary>
        /// Kendisini besleyen kantar.
        /// </summary>
        public IMalzemeBoşalt Kantar { get; set; }

        public TransferBandıController(VirtualController controller, string variableOwnerName, string commanderName) : base(controller)
        {
            VariableOwnerName = variableOwnerName;
            CommanderName = commanderName;
            var builder = new VirtualPLCPropertyBuilder(this);
            MalzemeBoşaltıldıProperty = builder.Reset().Name(nameof(MalzemeBoşaltıldı)).Type(typeof(bool)).Retain(true).Get();
            MalzemeBoşaltSenaryoProperty = builder.Reset().Name(nameof(MalzemeBoşaltSenaryo)).Type(typeof(int)).Retain(true).Get();
            EjectedInfoProperty = builder.Reset().Name(nameof(EjectedInfo)).Type(typeof(bool)).Input(true).Retain(true).Get();
        }
        /// <summary>
        /// Malzeme boşaltım işlemiyle ilgilenen method. Periyot tamamlandıysa, malzeme boşaltımı tamamlanmışsa işlem yapmaz.
        /// </summary>
        public void MalzemeBoşalt()
        {
            //Boşaltıldı ise geri dön.
            if (MalzemeBoşaltıldı)
                return;
            //Run komutu verilir.
            if (MalzemeBoşaltSenaryo == _runKomutuSenaryo)
            {
                InvokeCommand(CommandNames.RunCommand);
                MalzemeBoşaltSenaryo = _kantarBoşaltSenaryo;
            }
            //Kantar malzeme boşaltımı tamamlamadıysa boşalt methodu çağırılır. İşlem tamamlandığında Stop komutu verilir.
            else if (MalzemeBoşaltSenaryo == _kantarBoşaltSenaryo)
            {
                if (!Kantar.MalzemeBoşaltıldı)
                    Kantar.MalzemeBoşalt();
                else
                {
                    InvokeCommand(CommandNames.StopCommand);
                    MalzemeBoşaltSenaryo = _boşaltıldıİzleSenaryo;
                }
            }
            //Harici boşaltıldı bilgisi True ise cevap komutu verilir.
            else if (MalzemeBoşaltSenaryo == _boşaltıldıİzleSenaryo)
            {
                if (EjectedInfo)
                {
                    InvokeCommand(CommandNames.EjectedInfoResponse);
                    MalzemeBoşaltSenaryo = _boşaltıldıResetKontrolSenaryo;
                }
            }
            //Harici boşaltıldı bilgisi resetlenmişse MalzemeBoşaltıldı setlenir.
            else if (MalzemeBoşaltSenaryo == _boşaltıldıResetKontrolSenaryo)
            {
                if (!EjectedInfo)
                {
                    MalzemeBoşaltıldı = true;
                }
            }
        }
        /// <summary>
        /// Tekrar malzeme boşaltmaya hazır hale gelir.
        /// </summary>
        public void ResetMalzemeBoşalt()
        {
            MalzemeBoşaltSenaryo = 0;
            MalzemeBoşaltıldı = false;
            Kantar.ResetMalzemeBoşalt();
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
            _commander.RegisterCommand(CommandNames.RunCommand);
            _commander.RegisterCommand(CommandNames.StopCommand);
            _commander.RegisterCommand(CommandNames.EjectedInfoResponse);
        }
        #endregion
    }
}
