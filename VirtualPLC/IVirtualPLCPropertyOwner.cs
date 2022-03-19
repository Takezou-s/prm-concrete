using System.Collections.Generic;
using System.ComponentModel;

namespace VirtualPLC
{
    public interface IVirtualPLCPropertyOwner : INotifyPropertyChanged
    {
        List<VirtualPLCProperty> VirtualPLCProperties { get; }
        void OnPropertyChanged(string propertyName);
    }
}
