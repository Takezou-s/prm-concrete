using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Core
{
    [Serializable]
    public class RemoteVariable<T> : IRemoteVariable<T>
    {
        private T _readValue, _writeValue;
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public RemoteVariable()
        {
            
        }

        public RemoteVariable(IVariableDefinition definition)
        {
            Definition = definition;
        }

        public RemoteVariable(T writeValue)
        {
            WriteValue = writeValue;
        }

        public RemoteVariable(T writeValue, IVariableDefinition definiton) : this(writeValue)
        {
            Definition = definiton;
        }
        public string VariableName { get; set; }
        public T WriteValue
        {
            get
            {
                return _writeValue;
            }
            set
            {
                bool changed = !WriteValue.IsEqual(value);
                _writeValue = value;
                WriteValueChanged?.Invoke(this, new EventArgs());
                changed.DoIf(x => x, x => OnPropertyChanged(nameof(WriteValue)));
            }
        }
        public T ReadValue
        {
            get
            {
                return _readValue;
            }
            set
            {
                bool changed = !ReadValue.IsEqual(value);
                _readValue = value;
                ReadValueChanged?.Invoke(this, new EventArgs());
                changed.DoIf(x=>x,x=>OnPropertyChanged(nameof(ReadValue)));
            }
        }

        public Type Type { get { return typeof(T); } }
        public IVariableDefinition Definition { get; set; }
        
        public IVariableCommunicator Communicator { get; set; }

        object IRemoteValue.WriteValue
        {
            get
            {
                return WriteValue;
            }
            set
            {
                try
                {
                    WriteValue = (T)Convert.ChangeType(value,typeof(T));
                }
                catch
                {
                    throw new InvalidCastException("Değişken gereken tipte değil!");
                }
            }
        }
        object IRemoteValue.ReadValue
        {
            get
            {
                return ReadValue;
            }
            set
            {
                try
                {
                    ReadValue = (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    throw new InvalidCastException("Değişken gereken tipte değil!");
                }
            }
        }

        public event EventHandler ReadValueChanged;
        public event EventHandler WriteValueChanged;
    }
    public class RemoteValue<T> : IRemoteValue<T>
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region FieldsByAutoPropCreator
        private T _writeValue;
        private T _readValue;
        #endregion
        #region PropertiesByAutoPropCreator
        public T WriteValue
        {
            get
            {
                return _writeValue;
            }
            set
            {
                bool changed = false;
                if (!_writeValue.IsEqual(value))
                    changed = true;
                _writeValue = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(WriteValue));
                }
            }
        }
        public T ReadValue
        {
            get
            {
                return _readValue;
            }
            set
            {
                bool changed = false;
                if (!_readValue.IsEqual(value))
                    changed = true;
                _readValue = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ReadValue));
                }
            }
        }
        #endregion

        object IRemoteValue.WriteValue
        {
            get
            {
                return WriteValue;
            }
            set
            {
                try
                {
                    WriteValue = (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    throw new InvalidCastException("Değişken gereken tipte değil!");
                }
            }
        }
        object IRemoteValue.ReadValue
        {
            get
            {
                return ReadValue;
            }
            set
            {
                try
                {
                    ReadValue = (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    throw new InvalidCastException("Değişken gereken tipte değil!");
                }
            }
        }
    }
}
