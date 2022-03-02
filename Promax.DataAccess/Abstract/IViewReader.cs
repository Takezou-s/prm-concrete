using System.Collections.Generic;

namespace Promax.DataAccess
{
    public interface IViewReader<T>
    {
        List<T> GetList(string filter = null);
        T Get(string filter);
    }
}