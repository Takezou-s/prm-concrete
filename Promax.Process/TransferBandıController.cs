using Promax.Core;
using System.Collections.Generic;
using Utility.Binding;
using VirtualPLC;

namespace Promax.Process
{
    public class TransferBandıController : VirtualPLCObject, IMalzemeBoşalt, IVariableOwner, ICommander
    {
        public VirtualPLCProperty MalzemeBoşaltıldıProperty { get; private set; }
        public VirtualPLCProperty MalzemeBoşaltSenaryoProperty { get; private set; }
        public VirtualPLCProperty EjectedInfoProperty { get; private set; }

        private readonly int _runKomutuSenaryo = 0;
        private readonly int _kantarBoşaltSenaryo = 1;
        private readonly int _boşaltıldıİzleSenaryo = 2;
        private readonly int _boşaltıldıResetKontrolSenaryo = 3;
        private int MalzemeBoşaltSenaryo { get => (int)GetValue(MalzemeBoşaltSenaryoProperty); set => SetValue(MalzemeBoşaltSenaryoProperty, value); }

        public bool MalzemeBoşaltıldı { get => (bool)GetValue(MalzemeBoşaltıldıProperty); private set => SetValue(MalzemeBoşaltıldıProperty, value); }
        public bool MalzemeBoşaltılıyor { get => MalzemeBoşaltSenaryo == _boşaltıldıİzleSenaryo || MalzemeBoşaltSenaryo == _kantarBoşaltSenaryo; }
        public bool EjectedInfo { get => (bool)GetValue(EjectedInfoProperty); set => SetValue(EjectedInfoProperty, value); }
        public IMalzemeBoşalt Kantar { get; set; }

        public TransferBandıController(VirtualController controller, string variableOwnerName, string commanderName) : base(controller)
        {
            VariableOwnerName = variableOwnerName;
            CommanderName = commanderName;
            MalzemeBoşaltıldıProperty = VirtualPLCProperty.Register(nameof(MalzemeBoşaltıldı), typeof(bool), this, false, true, false);
            MalzemeBoşaltSenaryoProperty = VirtualPLCProperty.Register(nameof(MalzemeBoşaltSenaryo), typeof(int), this, false, true, false);
            EjectedInfoProperty = VirtualPLCProperty.Register(nameof(EjectedInfo), typeof(bool), this, true, true, false);
        }

        public void MalzemeBoşalt()
        {
            if (MalzemeBoşaltıldı)
                return;
            if (MalzemeBoşaltSenaryo == _runKomutuSenaryo)
            {
                InvokeCommand(CommandNames.RunCommand);
                MalzemeBoşaltSenaryo = _kantarBoşaltSenaryo;
            }
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
            else if (MalzemeBoşaltSenaryo == _boşaltıldıİzleSenaryo)
            {
                if (EjectedInfo)
                {
                    InvokeCommand(CommandNames.EjectedInfoResponse);
                    MalzemeBoşaltSenaryo = _boşaltıldıResetKontrolSenaryo;
                }
            }
            else if (MalzemeBoşaltSenaryo == _boşaltıldıResetKontrolSenaryo)
            {
                if (!EjectedInfo)
                {
                    MalzemeBoşaltıldı = true;
                }
            }
        }

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
