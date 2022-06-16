using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;

namespace Promax.Entities
{
    public class Settings : BindableObject
    {
        #region FieldsByAutoPropCreator
        private string _ip;
        private int _timeout;
        private SistemAyarları _sistemAyarları;
        #endregion
        #region PropertiesByAutoPropCreator
        public string Ip
        {
            get
            {
                return _ip;
            }
            set
            {
                SetValue(nameof(Ip), value, nameof(_ip));
            }
        }
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                SetValue(nameof(Timeout), value, nameof(_timeout));
            }
        }
        public SistemAyarları SistemAyarları
        {
            get
            {
                return _sistemAyarları;
            }
            set
            {
                SetValue(nameof(SistemAyarları), value, nameof(_sistemAyarları));
            }
        }
        #endregion
    }
}
