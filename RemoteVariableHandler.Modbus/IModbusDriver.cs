using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus
{
    public interface IModbusDriver
    {
        bool ReadBit(ushort slave, ushort register);
        Task<bool> ReadBitAsync(ushort slave, ushort register);
        bool[] ReadBits(ushort slave, ushort register, ushort quantityOfRegisters);
        Task<bool[]> ReadBitsAsync(ushort slave, ushort register, ushort quantityOfRegisters);
        ushort ReadRegister(ushort slave, ushort register);
        Task<ushort> ReadRegisterAsync(ushort slave, ushort register);
        ushort[] ReadRegisters(ushort slave, ushort register, ushort quantityOfRegisters);
        Task<ushort[]> ReadRegistersAsync(ushort slave, ushort register, ushort quantityOfRegisters);

        void WriteBit(ushort slave, ushort register, bool bit);
        Task WriteBitAsync(ushort slave, ushort register, bool bit);
        void WriteBits(ushort slave, ushort register, bool[] bits);
        Task WriteBitsAsync(ushort slave, ushort register, bool[] bits);
        void WriteRegister(ushort slave, ushort register, ushort word);
        Task WriteRegisterAsync(ushort slave, ushort register, ushort word);
        void WriteRegisters(ushort slave, ushort register, ushort[] words);
        Task WriteRegistersAsync(ushort slave, ushort register, ushort[] words);
    }
}
