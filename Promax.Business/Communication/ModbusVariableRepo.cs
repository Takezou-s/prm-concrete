using EasyModbus;
using System;
using System.Collections.Generic;

namespace Promax.Business
{
    public class ModbusVariableRepo
    {
        private ModbusClient _modbusClient;
        private VariableStackCreater _stackCreater = new VariableStackCreater();
        private Dictionary<ushort, int> _registerValues = new Dictionary<ushort, int>();
        private Dictionary<ushort, bool> _coilValues = new Dictionary<ushort, bool>();
        public IReadOnlyDictionary<ushort, int> RegisterValues => _registerValues;
        public IEnumerable<VariableStack> HoldingRegisters => _stackCreater.HoldingRegisters;
        public IEnumerable<VariableStack> HoldingCoils => _stackCreater.HoldingCoils;
        public ModbusVariableRepo(VariablesBase variableScope, ModbusClient modbusClient)
        {
            _modbusClient = modbusClient;
            _stackCreater.CreateStack(variableScope);
        }
        public void SetHoldingRegisters(ushort startingRegister, int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                SetRegister(startingRegister, values[i]);
                startingRegister++;
            }
        }
        public void SetHoldingCoils(ushort startingRegister, bool[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                SetCoil(startingRegister, values[i]);
                startingRegister++;
            }
        }
        public int GetRegisterValue(ushort Register)
        {
            int result = 0;
            if (_registerValues.ContainsKey(Register))
                result = _registerValues[Register];
            return result;
        }
        public bool GetCoilValue(ushort Register)
        {
            bool result = false;
            if (_coilValues.ContainsKey(Register))
                result = _coilValues[Register];
            return result;
        }
        public int[] GetRegisterValues(ushort Register, int Number)
        {
            int[] result = new int[Number];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = GetRegisterValue(Convert.ToUInt16(Register + i));
            }
            return result;
        }
        public bool[] GetCoilValues(ushort Register, int Number)
        {
            bool[] result = new bool[Number];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = GetCoilValue(Register);
            }
            return result;
        }
        private void SetRegister(ushort startingRegister, int value)
        {
            if (!_registerValues.ContainsKey(startingRegister))
            {
                _registerValues.Add(startingRegister, value);
            }
            _registerValues[startingRegister] = value;
        }
        private void SetCoil(ushort startingRegister, bool value)
        {
            if (!_coilValues.ContainsKey(startingRegister))
            {
                _coilValues.Add(startingRegister, value);
            }
            _coilValues[startingRegister] = value;
        }
    }
}
