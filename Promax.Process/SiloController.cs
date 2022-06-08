using Extensions;
using Promax.Business;
using Promax.Core;
using Promax.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Utility.Binding;
using VirtualPLC;

namespace Promax.Process
{
    public class SiloController : VirtualPLCObject, IMalzemeBoşalt, IVariableOwner, ICommander
    {
        public VirtualPLCProperty MalzemeBoşaltıldıProperty { get; private set; }
        public VirtualPLCProperty MalzemeBoşaltılıyorProperty { get; private set; }
        public VirtualPLCProperty MalzemeBoşaltSenaryoProperty { get; private set; }
        public VirtualPLCProperty EjectedInfoProperty { get; private set; }
        public VirtualPLCProperty SaveBatchedProperty { get; private set; }
        public VirtualPLCProperty DesiredProperty { get; private set; }

        private readonly int _boşaltKomutuSenaryo = 0;
        private readonly int _boşaltıldıİzleSenaryo = 1;
        private readonly int _boşaltıldıResetleSenaryo = 2;
        private readonly int _boşaltıldıResetKontrolSenaryo = 3;
        private readonly int _batchKaydedildiİzleSenaryo = 4;
        private int MalzemeBoşaltSenaryo { get => (int)GetValue(MalzemeBoşaltSenaryoProperty); set => SetValue(MalzemeBoşaltSenaryoProperty, value); }

        public ParameterOwnerBase ParameterOwner { get; private set; }
        public IVariables ParameterScope { get; private set; }
        public IVariables RecipeScope { get; private set; }
        public string NameInRecipeScope { get; set; }
        public string NameInParameterScope { get; set; }

        public bool MalzemeBoşaltıldı { get => (bool)GetValue(MalzemeBoşaltıldıProperty); private set => SetValue(MalzemeBoşaltıldıProperty, value); }
        public bool MalzemeBoşaltılıyor { get => (bool)GetValue(MalzemeBoşaltılıyorProperty); private set => SetValue(MalzemeBoşaltılıyorProperty, value); }
        public bool EjectedInfo { get => (bool)GetValue(EjectedInfoProperty); set => SetValue(EjectedInfoProperty, value); }
        public bool SaveBatched { get => (bool)GetValue(SaveBatchedProperty); set => SetValue(SaveBatchedProperty, value); }
        public double Desired { get => (double)GetValue(DesiredProperty); set => SetValue(DesiredProperty, value); }

        public SiloController(VirtualController controller, ParameterOwnerBase parameterOwner, string nameInRecipeScope, string nameInParameterScope, string variableOwnerName, string commanderName, IVariables parameterScope, IVariables recipeScope) : base(controller)
        {
            ParameterOwner = parameterOwner;
            ParameterScope = parameterScope;
            RecipeScope = recipeScope;
            NameInRecipeScope = nameInRecipeScope;
            NameInParameterScope = nameInParameterScope;
            VariableOwnerName = variableOwnerName;
            CommanderName = commanderName;
            MalzemeBoşaltıldıProperty = VirtualPLCProperty.Register(nameof(MalzemeBoşaltıldı), typeof(bool), this, false, true, false);
            MalzemeBoşaltılıyorProperty = VirtualPLCProperty.Register(nameof(MalzemeBoşaltılıyor), typeof(bool), this, false, true, false);
            MalzemeBoşaltSenaryoProperty = VirtualPLCProperty.Register(nameof(MalzemeBoşaltSenaryo), typeof(int), this, false, true, false);
            EjectedInfoProperty = VirtualPLCProperty.Register(nameof(EjectedInfo), typeof(bool), this, true, true, false);
            SaveBatchedProperty = VirtualPLCProperty.Register(nameof(SaveBatched), typeof(bool), this, true, true, false);
            DesiredProperty = VirtualPLCProperty.Register(nameof(Desired), typeof(double), this, false, true, false);
            ParameterOwner.Parameters.ForEach(x => x.PropertyChanged += ParameterPropertyChanged);
        }

        private void ParameterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.IsEqual(nameof(IParameter.Value)) || !MalzemeBoşaltılıyor)
                return;
            WriteParameter(sender as IParameter);
        }

        public void MalzemeBoşalt()
        {
            if (MalzemeBoşaltıldı)
                return;
            if (MalzemeBoşaltSenaryo == _boşaltKomutuSenaryo)
            {
                if (Desired <= 0)
                {
                    MalzemeBoşaltıldı = true;
                    return;
                }
                RecipeScope.Write(NameInRecipeScope, "İstenen", Desired);
                WriteParameters();
                InvokeCommand(CommandNames.EjectCommand);
                MalzemeBoşaltSenaryo = _boşaltıldıİzleSenaryo;
            }
            else if (MalzemeBoşaltSenaryo == _boşaltıldıİzleSenaryo)
            {
                if (EjectedInfo)
                    MalzemeBoşaltSenaryo = _boşaltıldıResetleSenaryo;
            }
            else if (MalzemeBoşaltSenaryo == _boşaltıldıResetKontrolSenaryo)
            {
                if (!EjectedInfo)
                {
                    SaveBatched = true;
                    MalzemeBoşaltSenaryo = _batchKaydedildiİzleSenaryo;
                }
            }
            else if (MalzemeBoşaltSenaryo == _batchKaydedildiİzleSenaryo)
            {
                if (!SaveBatched)
                {
                    MalzemeBoşaltıldı = true;
                    MalzemeBoşaltSenaryo = _batchKaydedildiİzleSenaryo;
                }
            }
            MalzemeBoşaltılıyor = MalzemeBoşaltSenaryo == _boşaltıldıİzleSenaryo;
        }

        public void ResetMalzemeBoşalt()
        {
            MalzemeBoşaltSenaryo = 0;
            MalzemeBoşaltıldı = false;
        }

        private void WriteParameters()
        {
            ParameterOwner.Parameters.ForEach(x => WriteParameter(x));
        }

        private void WriteParameter(IParameter parameter)
        {
            ParameterScope.Write(NameInParameterScope, parameter.Code, parameter.Value);
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
