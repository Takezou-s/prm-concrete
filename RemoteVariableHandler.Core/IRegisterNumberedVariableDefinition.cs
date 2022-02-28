using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Core
{
    /// <summary>
    /// Ushort tipinde bir registerda bulunan değişkenler için erişim bilgisini barındıran arayüzdür.
    /// </summary>
    public interface IRegisterNumberedVariableDefinition : IVariableDefinition
    {
        ushort Register { get; set; }
    }
}
