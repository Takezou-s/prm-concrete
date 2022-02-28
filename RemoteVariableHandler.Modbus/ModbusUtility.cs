using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus
{
    internal class ModbusUtility
    {
        /// <summary>
        /// Registerlardan okunan ushort verilerini int tipine dönüştürür.
        /// </summary>
        /// <param name="values">Dönüştürülecek değerler.</param>
        /// <returns></returns>
        internal static int GetInt(ushort[] values)
        {
            ushort highOrderValue = values[0];
            ushort lowOrderValue = 0;
            if (values.Length >= 2)
            {
                lowOrderValue = values[1];
            }
            int result = BitConverter.ToInt32(BitConverter.GetBytes(lowOrderValue).Concat(BitConverter.GetBytes(highOrderValue)).ToArray(), 0);
            return result;
        }

        /// <summary>
        /// Registerlardan okunan ushort verilerini uint tipine dönüştürür.
        /// </summary>
        /// <param name="values">Dönüştürülecek değerler.</param>
        /// <returns></returns>
        internal static uint GetUInt(ushort[] values)
        {
            ushort highOrderValue = values[0];
            ushort lowOrderValue = 0;
            if (values.Length >= 2)
            {
                lowOrderValue = values[1];
            }
            uint result = BitConverter.ToUInt32(BitConverter.GetBytes(lowOrderValue).Concat(BitConverter.GetBytes(highOrderValue)).ToArray<byte>(), 0);
            return result;
        }
        /// <summary>
        /// Registerlardan okunan ushort verilerini float tipine dönüştürür.
        /// </summary>
        /// <param name="values">Dönüştürülecek değerler.</param>
        /// <returns></returns>
        internal static float GetSingle(ushort[] values)
        {
            ushort highOrderValue = values[0];
            ushort lowOrderValue = 0;
            if (values.Length >= 2)
            {
                lowOrderValue = values[1];
            }
            float result = BitConverter.ToSingle(BitConverter.GetBytes(lowOrderValue).Concat(BitConverter.GetBytes(highOrderValue)).ToArray<byte>(), 0);
            return result;
        }
        /// <summary>
        /// Registerlardan okunan ushort verisini short tipine dönüştürür.
        /// </summary>
        /// <param name="value">Dönüştürülecek değer.</param>
        /// <returns></returns>
        internal static short GetShort(ushort value)
        {
            unchecked
            {
                var bytes = BitConverter.GetBytes(value);
                var result = BitConverter.ToInt16(bytes, 0);
                return result;
            }
        }
        /// <summary>
        /// Short to ushort.
        /// </summary>
        /// <param name="value">Dönüştürülecek değer.</param>
        /// <returns></returns>
        internal static ushort GetUShort(short value)
        {
            unchecked
            {
                var bytes = BitConverter.GetBytes(value);
                var result = BitConverter.ToUInt16(bytes, 0);
                return result;
            }
        }
        /// <summary>
        /// int bir değeri modbus ile gönderilecek biçimde wordlere ayırır.
        /// </summary>
        /// <param name="value">Dönüştürülecek değer</param>
        /// <returns></returns>
        internal static ushort[] SeparateToWords(int value)
        {
            byte[] array = BitConverter.GetBytes(value);
            return GetWordsFromByteArray(array);
        }
        /// <summary>
        /// int bir değeri modbus ile gönderilecek biçimde wordlere ayırır.
        /// </summary>
        /// <param name="value">Dönüştürülecek değer</param>
        /// <returns></returns>
        internal static ushort[] SeparateToWords(uint value)
        {
            byte[] array = BitConverter.GetBytes(value);
            return GetWordsFromByteArray(array);
        }
        /// <summary>
        /// int bir değeri modbus ile gönderilecek biçimde wordlere ayırır.
        /// </summary>
        /// <param name="value">Dönüştürülecek değer</param>
        /// <returns></returns>
        internal static ushort[] SeparateToWords(float value)
        {
            byte[] array = BitConverter.GetBytes(value);
            return GetWordsFromByteArray(array);
        }

        private static ushort[] GetWordsFromByteArray(byte[] array)
        {
            if (array.Length < 4)
            {
                ushort v = BitConverter.ToUInt16(array, 0);
                return new ushort[] { v };
            }
            unchecked
            {
                ushort v0 = BitConverter.ToUInt16(array, 2);
                ushort v1 = BitConverter.ToUInt16(array, 0);
                return new ushort[] { v1, v0 };
            }
        }
    }
}
