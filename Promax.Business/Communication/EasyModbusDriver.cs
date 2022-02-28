using EasyModbus;
using RemoteVariableHandler.Modbus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Promax.Business
{
    public class EasyModbusDriver : IModbusDriver
    {
        private ModbusClient _modbusClient;

        public EasyModbusDriver(ModbusClient modbusClient)
        {
            _modbusClient = modbusClient;
        }
        public bool ReadBit(ushort slave, ushort register)
        {
            return _modbusClient.ReadCoils(register, 1)[0];
        }

        public async Task<bool> ReadBitAsync(ushort slave, ushort register)
        {
            bool result = false;
            await Task.Run(() =>
            {
                result = this.ReadBit(slave, register);
            });
            return result;
        }

        public bool[] ReadBits(ushort slave, ushort register, ushort quantityOfRegisters)
        {
            return _modbusClient.ReadCoils(register, quantityOfRegisters);
        }

        public async Task<bool[]> ReadBitsAsync(ushort slave, ushort register, ushort quantityOfRegisters)
        {
            bool[] result = null;
            await Task.Run(() =>
            {
                result = this.ReadBits(slave, register, quantityOfRegisters);
            });
            return result;
        }

        public ushort ReadRegister(ushort slave, ushort register)
        {
            return (ushort)(_modbusClient.ReadHoldingRegisters(register, 1)[0]);
        }

        public async Task<ushort> ReadRegisterAsync(ushort slave, ushort register)
        {
            ushort result = 0;
            await Task.Run(() =>
            {
                result = this.ReadRegister(slave, register);
            });
            return result;
        }

        public ushort[] ReadRegisters(ushort slave, ushort register, ushort quantityOfRegisters)
        {
            var values = _modbusClient.ReadHoldingRegisters(register, quantityOfRegisters);
            List<ushort> list = new List<ushort>();
            foreach (var value in values)
            {
                list.Add((ushort)value);
            }
            return list.ToArray();
        }

        public async Task<ushort[]> ReadRegistersAsync(ushort slave, ushort register, ushort quantityOfRegisters)
        {
            ushort[] result = null;
            await Task.Run(() =>
            {
                result = this.ReadRegisters(slave, register, quantityOfRegisters);
            });
            return result;
        }

        public void WriteBit(ushort slave, ushort register, bool bit)
        {
            _modbusClient.WriteSingleCoil(register, bit);
        }

        public async Task WriteBitAsync(ushort slave, ushort register, bool bit)
        {
            await Task.Run(() =>
            {
                this.WriteBit(slave, register, bit);
            });
        }

        public void WriteBits(ushort slave, ushort register, bool[] bits)
        {
            _modbusClient.WriteMultipleCoils(register, bits);
        }

        public async Task WriteBitsAsync(ushort slave, ushort register, bool[] bits)
        {
            await Task.Run(() =>
            {
                this.WriteBits(slave, register, bits);
            });
        }

        public void WriteRegister(ushort slave, ushort register, ushort word)
        {
            _modbusClient.WriteSingleRegister(register, word);
        }

        public async Task WriteRegisterAsync(ushort slave, ushort register, ushort word)
        {
            await Task.Run(() =>
            {
                this.WriteRegister(slave, register, word);
            });
        }

        public void WriteRegisters(ushort slave, ushort register, ushort[] words)
        {
            List<int> list = new List<int>();
            foreach (var word in words)
            {
                list.Add(word);
            }
            _modbusClient.WriteMultipleRegisters(register, list.ToArray());
        }

        public async Task WriteRegistersAsync(ushort slave, ushort register, ushort[] words)
        {
            await Task.Run(() =>
            {
                this.WriteRegisters(slave, register, words);
            });
        }
    }
}
