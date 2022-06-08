using Promax.Core;
using System.Collections.Generic;
using Utility.Binding;
using VirtualPLC;

namespace Promax.Process
{
    public class MikserController : VirtualPLCObject, IKarıştır, IVariableOwner, ICommander
    {
        #region VirtualPLCProperty'ler
        public VirtualPLCProperty KarıştırıldıProperty { get; private set; }
        public VirtualPLCProperty KarıştırSenaryoProperty { get; private set; }
        public VirtualPLCProperty MixingDoneInfoProperty { get; private set; }
        #endregion
        #region Senaryo adımları
        private readonly int _karıştırKomutuSenaryo = 0;
        private readonly int _karıştırıldıİzleSenaryo = 1;
        private readonly int _karıştırıldıResetKontrolSenaryo = 2;
        #endregion
        /// <summary>
        /// Karıştır senaryosunun adımını tutan değişken.
        /// </summary>
        private int KarıştırSenaryo { get => (int)GetValue(KarıştırSenaryoProperty); set => SetValue(KarıştırSenaryoProperty, value); }
        /// <summary>
        /// Karıştırmanın tamamlandığını belirtir.
        /// </summary>
        public bool Karıştırıldı { get => (bool)GetValue(KarıştırıldıProperty); private set => SetValue(KarıştırıldıProperty, value); }
        /// <summary>
        /// Dışarıdan gelen "Karıştırıldı" bilgisi. Karıştır komutu verildikten sonra bu bilgi takip edilir.
        /// </summary>
        public bool MixingDoneInfo { get => (bool)GetValue(MixingDoneInfoProperty); set => SetValue(MixingDoneInfoProperty, value); }

        public MikserController(VirtualController controller, string variableOwnerName, string commanderName) : base(controller)
        {
            VariableOwnerName = variableOwnerName;
            CommanderName = commanderName;
            var builder = new VirtualPLCPropertyBuilder(this);
            KarıştırıldıProperty = builder.Reset().Name(nameof(Karıştırıldı)).Type(typeof(bool)).Retain(true).Get();
            KarıştırSenaryoProperty = builder.Reset().Name(nameof(KarıştırSenaryo)).Type(typeof(int)).Retain(true).Get();
            MixingDoneInfoProperty = builder.Reset().Name(nameof(MixingDoneInfo)).Type(typeof(bool)).Input(true).Retain(true).Get();
            InitVariables();
            InitCommands();
        }
        /// <summary>
        /// Karıştırma işlemiyle ilgilenen method. Karıştırma tamamlanmışsa işlem yapmaz.
        /// </summary>
        public void Karıştır()
        {
            if (Karıştırıldı)
                return;
            //Karıştır komutu verilir.
            if (KarıştırSenaryo == _karıştırKomutuSenaryo)
            {
                InvokeCommand(CommandNames.MixCommand);
                KarıştırSenaryo = _karıştırıldıİzleSenaryo;
            }
            //Harici karıştırıldı bilgisi geliyorsa cevap komutu verilir.
            else if (KarıştırSenaryo == _karıştırıldıİzleSenaryo)
            {
                if (MixingDoneInfo)
                {
                    InvokeCommand(CommandNames.MixingDoneInfoResponse);
                    KarıştırSenaryo = _karıştırıldıResetKontrolSenaryo;
                }
            }
            //Harici karıştırıldı bilgisi resetlenmiş ise Karıştırıldı setlenir.
            else if(KarıştırSenaryo == _karıştırıldıResetKontrolSenaryo)
            {
                if (!MixingDoneInfo)
                    Karıştırıldı = true;
            }
        }
        /// <summary>
        /// Tekrar karıştırmaya hazırlanır.
        /// </summary>
        public void ResetKarıştır()
        {
            KarıştırSenaryo = _karıştırKomutuSenaryo;
            Karıştırıldı = false;
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
            _variables.Add(nameof(MixingDoneInfo));
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
            _commander.RegisterCommand(CommandNames.MixCommand);
            _commander.RegisterCommand(CommandNames.MixingDoneInfoResponse);
        }
        #endregion
    }
}
