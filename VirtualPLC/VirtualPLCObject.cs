using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPLC
{
    public class VirtualPLCObject : IVirtualPLCPropertyOwner
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public List<VirtualPLCProperty> VirtualPLCProperties { get; } = new List<VirtualPLCProperty>();
        public VirtualController Controller { get; private set; }

        public VirtualPLCObject(VirtualController controller)
        {
            Controller = controller;
            Controller.VirtualPLCObjects.Add(this);
        }

        public void RestoreInputs()
        {
            VirtualPLCProperties.ForEach(x => x.RestoreValue());
        }

        public void SetOutputs()
        {
            VirtualPLCProperties.ForEach(x => x.SetOutputValue());
        }

        protected object GetValue(VirtualPLCProperty property)
        {
            return property.GetValue();
        }

        protected void SetValue(VirtualPLCProperty property, object value)
        {
            property.SetValue(value);
        }
    }
}
