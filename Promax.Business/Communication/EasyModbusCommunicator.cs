using EasyModbus;
using Extensions;
using Promax.Core;
using RemoteVariableHandler.Core;
using RemoteVariableHandler.Modbus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Promax.Business
{
    public class EasyModbusCommunicator : IVariableCommunicator
    {
        private ModbusCommunicator _modbusCommunicator;
        private EasyModbusDriver _modbusDriver;
        private ModbusClient _modbusClient;
        private IExceptionHandler _exceptionHandler;
        private ModbusVariableRepo _repo;
        public IReadOnlyDictionary<ushort, int> RegisterValues
        {
            get
            {
                if (_repo == null)
                    return new Dictionary<ushort, int>();
                else
                    return _repo.RegisterValues;
            }
        }

        private object _locker = new object();
        public bool Connected { get => _modbusClient.Connected; }
        public void Connect(string ip, int timeout)
        {
            if (Connected)
            {
                Disconnect();
            }
            if (!Connected)
            {
                _modbusClient.ConnectionTimeout = timeout;
                _modbusClient.IPAddress = ip;
                _modbusClient.Connect(ip, 502);
            }
        }
        public void Disconnect()
        {
            if (Connected)
                _modbusClient.Disconnect();
        }
        public EasyModbusCommunicator(IExceptionHandler exceptionHandler)
        {
            _modbusClient = new ModbusClient();
            _modbusDriver = new EasyModbusDriver(_modbusClient);
            _modbusCommunicator = new ModbusCommunicator(1, _modbusDriver);
            _exceptionHandler = exceptionHandler;
        }
        public void SetVariables(VariablesBase variables)
        {
            _repo = new ModbusVariableRepo(variables, _modbusClient);
        }
        public void ReadScope()
        {
            _exceptionHandler.Handle(() =>
            {
                try
                {
                    foreach (var item in _repo.HoldingRegisters)
                    {
                        lock (_locker)
                        {
                            _repo.SetHoldingRegisters(item.StartingIndex, _modbusClient.ReadHoldingRegisters(item.StartingIndex, item.Count));
                        }
                    }
                    foreach (var item in _repo.HoldingCoils)
                    {
                        lock (_locker)
                        {
                            _repo.SetHoldingCoils(item.StartingIndex, _modbusClient.ReadCoils(item.StartingIndex, item.Count));
                        }
                    }
                }
                catch (Exception exception)
                {

                    throw new RemoteVariableException(() => {; }, null, "Error while reading scope", "Read", exception);
                }
            });
        }
        #region IVariableCommunicator
        public void Read(IRemoteVariable variable)
        {
            _exceptionHandler.Handle(() =>
            {
                try
                {
                    variable.DoIfElse(x => !(x is IRemoteVariable<short> || x is IRemoteVariable<bool> || x is IRemoteVariable<float> || x is IRemoteVariable<int>), x =>
                    {
                        lock (_locker)
                        {
                            ((IVariableCommunicator)_modbusCommunicator).Read(variable);
                        }
                    },
                        x =>
                        {
                            if (x is IRemoteVariable<short>)
                            {
                                x.ReadValue = Convert.ToInt16(_repo.GetRegisterValue((x.Definition as IRegisterNumberedVariableDefinition).Register));
                            }
                            else if (x is IRemoteVariable<bool> && x.Definition is BitOfShortVariableDefinition)
                            {
                                BitOfShortVariableDefinition definition = x.Definition as BitOfShortVariableDefinition;
                                var value = _repo.GetRegisterValue(definition.Register);
                                BitArray bitArray = new BitArray(new int[] { value });
                                bool[] bits = new bool[bitArray.Count];
                                bitArray.CopyTo(bits, 0);
                                x.ReadValue = bits[definition.BitNumber];
                            }
                            else if (x is IRemoteVariable<float>)
                            {
                                int[] array = _repo.GetRegisterValues((x.Definition as IRegisterNumberedVariableDefinition).Register, 2);
                                x.ReadValue = ModbusClient.ConvertRegistersToFloat(array);
                            }
                            else if (x is IRemoteVariable<int>)
                            {
                                int[] array = _repo.GetRegisterValues((x.Definition as IRegisterNumberedVariableDefinition).Register, 2);
                                x.ReadValue = ModbusClient.ConvertRegistersToInt(array);
                            }
                        });
                }
                catch (Exception exception)
                {
                    throw new RemoteVariableException(() => variable.Read(this), variable, "Error while reading, variable name: " + variable.VariableName, "Read", exception);
                }
            });
        }

        public Task ReadAsync(IRemoteVariable variable)
        {
            Task result = null;
            _exceptionHandler.Handle(() =>
            {
                try
                {
                    variable.DoIfElse(x => !(x is IRemoteVariable<short> || x is IRemoteVariable<bool>), x =>
                    {
                        lock (_locker)
                        {
                            result = ((IVariableCommunicator)_modbusCommunicator).ReadAsync(variable);
                        }
                    },
                        x =>
                        {
                            if (x is IRemoteVariable<short>)
                            {
                                x.ReadValue = Convert.ToInt16(_repo.GetRegisterValue((x.Definition as IRegisterNumberedVariableDefinition).Register));
                            }
                            else if (x is IRemoteVariable<bool> && x.Definition is BitOfShortVariableDefinition)
                            {
                                BitOfShortVariableDefinition definition = x.Definition as BitOfShortVariableDefinition;
                                var value = _repo.GetRegisterValue(definition.Register);
                                BitArray bitArray = new BitArray(new int[] { value });
                                bool[] bits = new bool[bitArray.Count];
                                bitArray.CopyTo(bits, 0);
                                x.ReadValue = bits[definition.BitNumber];
                            }
                            else if (x is IRemoteVariable<float>)
                            {
                                int[] array = _repo.GetRegisterValues((x.Definition as IRegisterNumberedVariableDefinition).Register, 2);
                                x.ReadValue = ModbusClient.ConvertRegistersToFloat(array);
                            }
                        });
                }
                catch (Exception exception)
                {
                    throw new RemoteVariableException(() => variable.Read(this), variable, "Error while async reading, variable name: " + variable.VariableName, "Read", exception);
                }
            });
            return result;
        }

        public void Write(IRemoteVariable variable)
        {
            _exceptionHandler.Handle(() =>
            {
                try
                {
                    lock (_locker)
                    {
                        ((IVariableCommunicator)_modbusCommunicator).Write(variable);
                    }
                }
                catch (Exception exception)
                {
                    throw new RemoteVariableException(() => variable.Write(this), variable, "Error while writing, variable name: " + variable.VariableName, "Write", exception);
                }
            });
        }

        public Task WriteAsync(IRemoteVariable variable)
        {
            Task result = null;
            _exceptionHandler.Handle(() =>
            {
                try
                {
                    lock (_locker)
                    {
                        result = ((IVariableCommunicator)_modbusCommunicator).WriteAsync(variable);
                    }
                }
                catch (Exception exception)
                {
                    throw new RemoteVariableException(() => variable.Write(this), variable, "Error while async writing, variable name: " + variable.VariableName, "Write", exception);
                }
            });
            return result;
        }
        #endregion
    }
}
