using System.ComponentModel;

namespace Promax.Core
{
    public interface IParameter : INotifyPropertyChanged
    {
        string Name { get; set; }
        string Code { get; set; }
        string Unit { get; set; }
        object Value { get; set; }
    }
}
