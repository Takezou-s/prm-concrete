using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualPLC;

namespace Promax.Process
{
    public class KantarController : VirtualPLCObject, IMalzemeAl
    {
        public VirtualPLCProperty MalzemeAlındıProperty { get; private set; }

        public List<IMalzemeBoşalt> Silolar { get; set; } = new List<IMalzemeBoşalt>();

        public KantarController(VirtualController controller) : base(controller)
        {
            MalzemeAlındıProperty = VirtualPLCProperty.Register(nameof(MalzemeAlındı), typeof(bool), this, false, true, false);
        }

        public bool MalzemeAlındı { get => (bool)GetValue(MalzemeAlındıProperty); private set => SetValue(MalzemeAlındıProperty, value); }

        public void MalzemeAl()
        {
            if (MalzemeAlındı)
                return;
            bool _alındı = true;
            foreach (var silo in Silolar)
            {
                if(!silo.MalzemeBoşaltıldı)
                {
                    _alındı = false;
                    silo.MalzemeBoşalt();
                    break;
                }
            }
            if (_alındı)
                MalzemeAlındı = true;
        }

        public void ResetMalzemeAl()
        {
            throw new NotImplementedException();
        }
    }
}
