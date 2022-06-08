using Promax.Core;
using System.Collections.Generic;
using Utility.Binding;
using VirtualPLC;

namespace Promax.Process
{
    public class MikserController : VirtualPLCObject, IKarıştır, IVariableOwner, ICommander
    {
        public VirtualPLCProperty KarıştırıldıProperty { get; private set; }
        public VirtualPLCProperty KarıştırSenaryoProperty { get; private set; }
        public VirtualPLCProperty MixingDoneInfoProperty { get; private set; }

        private readonly int _karıştırKomutuSenaryo = 0;
        private readonly int _karıştırıldıİzleSenaryo = 1;
        private readonly int _karıştırıldıResetKontrolSenaryo = 2;

        private int KarıştırSenaryo { get => (int)GetValue(KarıştırSenaryoProperty); set => SetValue(KarıştırSenaryoProperty, value); }

        public bool Karıştırıldı { get => (bool)GetValue(KarıştırıldıProperty); private set => SetValue(KarıştırıldıProperty, value); }
        public bool MixingDoneInfo { get => (bool)GetValue(MixingDoneInfoProperty); set => SetValue(MixingDoneInfoProperty, value); }

        public MikserController(VirtualController controller, string variableOwnerName, string commanderName) : base(controller)
        {
            VariableOwnerName = variableOwnerName;
            CommanderName = commanderName;
            KarıştırıldıProperty = VirtualPLCProperty.Register(nameof(Karıştırıldı), typeof(bool), this, false, true, false);
            KarıştırSenaryoProperty = VirtualPLCProperty.Register(nameof(KarıştırSenaryo), typeof(int), this, false, true, false);
            MixingDoneInfoProperty = VirtualPLCProperty.Register(nameof(MixingDoneInfo), typeof(bool), this, true, true, false);
            InitVariables();
            InitCommands();
        }

        public void Karıştır()
        {
            if (Karıştırıldı)
                return;
            if (KarıştırSenaryo == _karıştırKomutuSenaryo)
            {
                InvokeCommand(CommandNames.MixCommand);
                KarıştırSenaryo = _karıştırıldıİzleSenaryo;
            }
            else if (KarıştırSenaryo == _karıştırıldıİzleSenaryo)
            {
                if (MixingDoneInfo)
                {
                    InvokeCommand(CommandNames.MixingDoneInfoResponse);
                    KarıştırSenaryo = _karıştırıldıResetKontrolSenaryo;
                }
            }
            else if(KarıştırSenaryo == _karıştırıldıResetKontrolSenaryo)
            {
                if (!MixingDoneInfo)
                    Karıştırıldı = true;
            }
        }

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
