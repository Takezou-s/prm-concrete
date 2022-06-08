using System.Collections.Generic;
using System.ComponentModel;

namespace VirtualPLC
{
    /// <summary>
    /// VirtualPLCProperty sahibi olan objeyi tanımlamak için kullanılan arayüz.
    /// </summary>
    public interface IVirtualPLCPropertyOwner : INotifyPropertyChanged
    {
        /// <summary>
        /// Sahip olunan VirtualPLCProperty objelerinin bulunduğu liste.
        /// </summary>
        List<VirtualPLCProperty> VirtualPLCProperties { get; }
        /// <summary>
        /// Property değişimini bildirmek için kullanılan method.
        /// </summary>
        /// <param name="propertyName">Değişen property adı.</param>
        void OnPropertyChanged(string propertyName);
    }
}
