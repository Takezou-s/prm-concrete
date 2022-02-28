using Extensions;
using Promax.Core;
using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;

namespace Promax.Business
{
    public abstract class ParameterDomain
    {
        protected IVariables _parameterScope;
        protected MyBinding _binding;
        protected ParameterOwnerContextSerializer _serializer;


        private bool _autoSerializeOnChange;


        protected Dictionary<object, IRemoteVariable> BoundVariables { get; set; } = new Dictionary<object, IRemoteVariable>();


        public virtual ParameterOwnerContext ParameterOwnerContext { get; protected set; }
        public virtual bool AutoSerializeOnChange
        {
            get => _autoSerializeOnChange; set
            {
                _autoSerializeOnChange = value;
                value.DoIfElse(x => x, x => _serializer.AutoSerialize(ParameterOwnerContext), x => _serializer.DisableAutoSerialize());
            }
        }
        public virtual bool AutoSendOnChange { get; set; }
        protected ParameterDomain(IVariables parameterScope, MyBinding binding, ParameterOwnerContextSerializer serializer)
        {
            _parameterScope = parameterScope;
            _binding = binding;
            _serializer = serializer;
            LoadParameters();
            InitDomain();
        }
        protected virtual void InitDomain()
        {
            foreach (var parameterOwner in ParameterOwnerContext.ParameterOwners)
            {
                foreach (var parameter in parameterOwner.Parameters)
                {
                    _parameterScope.GetVariable(parameterOwner.Name, parameter.Code).Do(x =>
                    {
                        BoundVariables.DoIf(y => !y.ContainsKey(parameter), y => BoundVariables.Add(parameter, x));
                        _binding.CreateBinding().Source(parameter).SourceProperty(nameof(IParameter<Type>.Value)).
                        Target(x).TargetProperty(nameof(IRemoteVariable.WriteValue)).
                        Mode(MyBindingMode.TwoWay).WhenSourcePropertyChanged(() =>
                        {
                            AutoWriteVariable(x);
                        });
                    });
                }
            }
            _binding.InitialMapping();
        }

        protected abstract void LoadParameters();

        public virtual void SaveParameters()
        {
            _serializer.Save(ParameterOwnerContext);
        }
        public virtual void SendParameters()
        {
            foreach (var item in BoundVariables)
            {
                WriteVariable(item.Value);
            }
        }
        public virtual void SendParameter(object parameter)
        {
            parameter.DoIf(x => BoundVariables.ContainsKey(x), x => BoundVariables[x].Write());
        }
        public virtual void Bind(IParameterOwner parameterOwner)
        {
            parameterOwner.BindDefault(ParameterOwnerContext, _binding);
        }
        private void AutoWriteVariable(IRemoteVariable variable)
        {
            if (AutoSendOnChange)
                variable.Write(variable.Communicator);
        }
        private void WriteVariable(IRemoteVariable variable)
        {
            variable.Write(variable.Communicator);
        }
    }
    public abstract class ParameterDomain<T> : ParameterDomain
       where T : ParameterOwnerContext, new()
    {
        private T _parameterOwnerContext;

        public new T ParameterOwnerContext
        {
            get => _parameterOwnerContext; protected set
            {
                _parameterOwnerContext = value;
                base.ParameterOwnerContext = value;
            }
        }

        protected ParameterDomain(IVariables parameterScope, MyBinding binding, ParameterOwnerContextSerializer serializer) : base(parameterScope, binding, serializer)
        {

        }
        protected override void LoadParameters()
        {
            _serializer.TryCatch(x =>
            {
                ParameterOwnerContext = new T();
                x.Load(ParameterOwnerContext);
            }, () => {; });
        }
    }
    public class MyParameterDomain : ParameterDomain<MyParameterOwnerContext>
    {
        private MyBinding _internalBindings = new MyBinding();
        private MyBinding _parameterOwnerBindings = new MyBinding();

        public ParameterOwnerContainer ParameterOwnerContainer { get; private set; }
        public MyParameterDomain(IVariables parameterScope, ParameterOwnerContainer parameterOwnerContainer) : this(parameterScope, parameterOwnerContainer, new MyBinding(), new ParameterOwnerContextSerializer("saves\\Parameters.txt"))
        {

        }
        public MyParameterDomain(IVariables parameterScope, ParameterOwnerContainer parameterOwnerContainer, MyBinding binding, ParameterOwnerContextSerializer serializer) : base(parameterScope, binding, serializer)
        {
            AutoSendOnChange = true;
            AutoSerializeOnChange = true;
            ParameterOwnerContainer = parameterOwnerContainer;
            ParameterOwnerContainer.ObjectRegistered += ParameterOwnerContainer_ObjectRegistered;
            BindObjectsInParameterOwnerContainer();
        }

        private void ParameterOwnerContainer_ObjectRegistered(object container, IParameterOwner registeredObject)
        {
            registeredObject.BindDefault(ParameterOwnerContext, _parameterOwnerBindings);
            _parameterOwnerBindings.InitialMapping();
        }

        private void BindObjectsInParameterOwnerContainer()
        {
            _parameterOwnerBindings.ClearBindings();
            ParameterOwnerContainer.Objects.ToList().ForEach(x => x.BindDefault(ParameterOwnerContext, _parameterOwnerBindings));
            _parameterOwnerBindings.InitialMapping();
        }
    }
}
