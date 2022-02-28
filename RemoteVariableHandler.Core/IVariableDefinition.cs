using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Core
{
    /// <summary>
    /// Değişkenlerin erişim bilgisini barındıran arayüzdür.
    /// </summary>
    public interface IVariableDefinition
    {
        object Definition { get; set; }
    }
}
