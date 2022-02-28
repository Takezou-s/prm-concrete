using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Core
{
    public class VariableBuilder : IVariableBuilder
    {
        bool _writeValueSet, _readValueSet;

        IVariableDefinition _definition;
        object _writeValue, _readValue;
        Type _type;

        public virtual IVariableBuilder SetDefinition(IVariableDefinition definition)
        {
            _definition = definition;
            return this;
        }

        public virtual IVariableBuilder SetWriteValue<T>(T value)
        {
            _writeValue = value;
            _writeValueSet = true;
            _type = typeof(T);
            return this;
        }

        public virtual IRemoteVariable GetVariable<T>()
        {
            var rv = new RemoteVariable<T>(_definition);
            if (_writeValueSet)
                rv.WriteValue = (T)_writeValue;
            if (_readValueSet)
                rv.ReadValue = (T)_readValue;
            return rv;
        }

        public virtual IRemoteVariable<T> GetVariableAsGeneric<T>()
        {
            var rv = new RemoteVariable<T>(_definition);
            if (_writeValueSet)
                rv.WriteValue = (T)_writeValue;
            if (_readValueSet)
                rv.ReadValue = (T)_readValue;
            return rv;
        }

        public IVariableBuilder SetReadValue<T>(T value)
        {
            _readValue = value;
            _readValueSet = true;
            _type = typeof(T);
            return this;
        }

        public IVariableBuilder Reset()
        {
            _writeValueSet = false;
            _readValueSet = false;
            _definition = null;
            _writeValue = null;
            _readValue = null;
            _type = null;
            return this;
        }
    }
}
