using Extensions;
using Promax.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;
using VirtualPLC;

namespace Promax.Process
{
    public class KantarController : VirtualPLCObject, IMalzemeAl, IMalzemeBoşalt, IVariableOwner, ICommander
    {
        public VirtualPLCProperty MalzemeAlındıProperty { get; private set; }
        public VirtualPLCProperty MalzemeBoşaltıldıProperty { get; private set; }
        public VirtualPLCProperty MalzemeBoşaltSenaryoProperty { get; private set; }
        public VirtualPLCProperty EjectedInfoProperty { get; private set; }
        public VirtualPLCProperty İstenenPeriyotProperty { get; private set; }
        public VirtualPLCProperty MalzemeAlTamamlananPeriyotProperty { get; private set; }
        public VirtualPLCProperty MalzemeBoşaltTamamlananPeriyotProperty { get; private set; }

        private readonly int _boşaltKomutuSenaryo = 0;
        private readonly int _boşaltıldıİzleSenaryo = 1;
        private readonly int _boşaltıldıResetleSenaryo = 2;
        private readonly int _boşaltıldıResetKontrolSenaryo = 3;
        private int MalzemeBoşaltSenaryo { get => (int)GetValue(MalzemeBoşaltSenaryoProperty); set => SetValue(MalzemeBoşaltSenaryoProperty, value); }

        public List<IMalzemeBoşalt> Silolar { get; set; } = new List<IMalzemeBoşalt>();
        public bool MalzemeAlındı { get => (bool)GetValue(MalzemeAlındıProperty); private set => SetValue(MalzemeAlındıProperty, value); }
        public bool MalzemeBoşaltıldı { get => (bool)GetValue(MalzemeBoşaltıldıProperty); private set => SetValue(MalzemeBoşaltıldıProperty, value); }
        public bool EjectedInfo { get => (bool)GetValue(EjectedInfoProperty); set => SetValue(EjectedInfoProperty, value); }
        public int İstenenPeriyot { get => (int)GetValue(İstenenPeriyotProperty); set => SetValue(İstenenPeriyotProperty, value); }
        public int MalzemeAlTamamlananPeriyot { get => (int)GetValue(MalzemeAlTamamlananPeriyotProperty); set => SetValue(MalzemeAlTamamlananPeriyotProperty, value); }
        public int MalzemeBoşaltTamamlananPeriyot { get => (int)GetValue(MalzemeBoşaltTamamlananPeriyotProperty); private set => SetValue(MalzemeBoşaltTamamlananPeriyotProperty, value); }
        public bool MalzemeAlPeriyotTamamlandı => MalzemeAlTamamlananPeriyot >= İstenenPeriyot;
        public bool MalzemeBoşaltPeriyotTamamlandı => MalzemeBoşaltTamamlananPeriyot >= İstenenPeriyot;


        public KantarController(VirtualController controller, string variableOwnerName, string commanderName) : base(controller)
        {
            VariableOwnerName = variableOwnerName;
            CommanderName = commanderName;
            MalzemeAlındıProperty = VirtualPLCProperty.Register(nameof(MalzemeAlındı), typeof(bool), this, false, true, false);
            MalzemeBoşaltıldıProperty = VirtualPLCProperty.Register(nameof(MalzemeBoşaltıldı), typeof(bool), this, false, true, false);
            MalzemeBoşaltSenaryoProperty = VirtualPLCProperty.Register(nameof(MalzemeBoşaltSenaryo), typeof(int), this, false, true, false);
            EjectedInfoProperty = VirtualPLCProperty.Register(nameof(EjectedInfo), typeof(bool), this, true, true, false);
            İstenenPeriyotProperty = VirtualPLCProperty.Register(nameof(İstenenPeriyot), typeof(int), this, false, true, false);
            MalzemeAlTamamlananPeriyotProperty = VirtualPLCProperty.Register(nameof(MalzemeAlTamamlananPeriyot), typeof(int), this, false, true, false);
            MalzemeBoşaltTamamlananPeriyotProperty = VirtualPLCProperty.Register(nameof(MalzemeBoşaltTamamlananPeriyot), typeof(int), this, false, true, false);
            InitVariables();
            InitCommands();
        }

        public void MalzemeAl()
        {
            if (MalzemeAlPeriyotTamamlandı)
                return;
            if (MalzemeAlındı)
                return;
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
            if (_alındı)
            {
                MalzemeAlındı = true;
                MalzemeAlTamamlananPeriyot++;
            }
        }

        public void ResetMalzemeAl()
        {
            foreach (var silo in Silolar)
            {
                silo.ResetMalzemeBoşalt();
            }
            MalzemeAlındı = false;
        }

        public void MalzemeBoşalt()
        {
            if (MalzemeBoşaltPeriyotTamamlandı)
                return;
            if (MalzemeBoşaltıldı)
                return;
            if(MalzemeBoşaltSenaryo == _boşaltKomutuSenaryo)
            {
                InvokeCommand(CommandNames.EjectCommand);
                MalzemeBoşaltSenaryo = _boşaltıldıİzleSenaryo;
            }
            else if(MalzemeBoşaltSenaryo == _boşaltıldıİzleSenaryo)
            {
                if (EjectedInfo)
                    MalzemeBoşaltSenaryo = _boşaltıldıResetleSenaryo;
            }
            else if (MalzemeBoşaltSenaryo == _boşaltıldıResetleSenaryo)
            {
                InvokeCommand(CommandNames.EjectedInfoResponse);
                MalzemeBoşaltSenaryo = _boşaltıldıResetKontrolSenaryo;
            }
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
    public class SiloController : VirtualPLCObject
    {
        public VirtualPLCProperty MalzemeBoşaltıldıProperty { get; private set; }
        public VirtualPLCProperty MalzemeBoşaltılıyorProperty { get; private set; }

        public ParameterOwnerBase ParameterOwner { get; private set; }
        public IVariables ParameterScope { get; private set; }
        public IVariables RecipeScope { get; private set; }

        public bool MalzemeBoşaltıldı { get => (bool)GetValue(MalzemeBoşaltıldıProperty); private set => SetValue(MalzemeBoşaltıldıProperty, value); }
        public bool MalzemeBoşaltılıyor { get => (bool)GetValue(MalzemeBoşaltılıyorProperty); private set => SetValue(MalzemeBoşaltılıyorProperty, value); }

        public SiloController(VirtualController controller, ParameterOwnerBase parameterOwner, IVariables parameterScope, IVariables recipeScope) : base(controller)
        {
            ParameterOwner = parameterOwner;
            ParameterScope = parameterScope;
            RecipeScope = recipeScope;
            MalzemeBoşaltıldıProperty = VirtualPLCProperty.Register(nameof(MalzemeBoşaltıldı), typeof(bool), this, false, true, false);
            MalzemeBoşaltılıyorProperty = VirtualPLCProperty.Register(nameof(MalzemeBoşaltılıyor), typeof(bool), this, false, true, false);
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
        }
        
        public void ResetMalzemeBoşalt()
        {

        }

        private void WriteParameters()
        {
            ParameterOwner.Parameters.ForEach(x => WriteParameter(x));
        }

        private void WriteParameter(IParameter parameter)
        {
            ParameterScope.GetVariable(parameter.Name, parameter.Code).Do(x =>
             {
                 ReflectionController.SetPropertyValue(x, nameof(x.WriteValue), parameter.Value);
                 x.Write();
             });
        }
    }
}
