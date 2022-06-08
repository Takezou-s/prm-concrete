using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPLC
{
    /// <summary>
    /// VirtualControllerda çalışacak, VirtualPLCProperty sahibi objeleri tanımlamak için kullanılır.
    /// </summary>
    public class VirtualPLCObject : IVirtualPLCPropertyOwner
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        /// <summary>
        /// VirtualPLCProperty objelerinin bulunduğu liste.
        /// </summary>
        public List<VirtualPLCProperty> VirtualPLCProperties { get; } = new List<VirtualPLCProperty>();
        /// <summary>
        /// Sahibi olan VirtualController.
        /// </summary>
        public VirtualController Controller { get; private set; }

        public VirtualPLCObject(VirtualController controller)
        {
            Controller = controller;
            Controller.VirtualPLCObjects.Add(this);
        }
        /// <summary>
        /// VirtualPLCProperty objelerinin geçici değerini gerçek değere yazar.
        /// </summary>
        public void RestoreInputs()
        {
            VirtualPLCProperties.ForEach(x => x.RestoreValue());
        }
        /// <summary>
        /// VirtualPLCProperty objelerinin değer değişimlerini dışarı bildirir.
        /// </summary>
        public void SetOutputs()
        {
            VirtualPLCProperties.ForEach(x => x.SetOutputValue());
        }
        /// <summary>
        /// Belirtilen VirtualPLCProperty'sinin değerini döndürür.
        /// </summary>
        /// <param name="property">Değeri istenen VirtualPLCProperty</param>
        /// <returns>VirtualPLCProperty değeri</returns>
        protected object GetValue(VirtualPLCProperty property)
        {
            return property.GetValue();
        }
        /// <summary>
        /// Belirtilen VirtualPLCProperty'sinin değerini setler.
        /// </summary>
        /// <param name="property">Değeri setlenmek istenen VirtualPLCProperty</param>
        /// <param name="value">Değer</param>
        protected void SetValue(VirtualPLCProperty property, object value)
        {
            property.SetValue(value);
        }
    }
}
