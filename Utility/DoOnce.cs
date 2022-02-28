using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class DoOnce
    {
        private volatile object _locker = new object();

        public DoOnce(bool lockPerform=false)
        {
            LockPerform = lockPerform;
        }

        public bool LockPerform { get; set; }
        public bool Done { get; private set; }
        public void Perform(Action action)
        {
            if (LockPerform)
            {
                lock (_locker)
                {
                    Done.DoIf(x => !x, x => { action?.Invoke(); Done = true; });  
                }
            }
            else
            {
                Done.DoIf(x => !x, x => { action?.Invoke(); Done = true; });
            }
        }
        public void Reset()
        {
            Done = false;
        }
    }
}
