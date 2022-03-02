using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.UI
{
    static class Infrastructure
    {
        private static Main _main;
        private static volatile object _locker = new object();
        static Infrastructure()
        {
            //_main = new Main();
            //_main.Init();
        }
        public static Main Main
        {
            get
            {
                lock (_locker)
                {
                    if (_main == null)
                    {
                        _main = new Main();
                        _main.Init();
                    }
                    return _main;
                }
            }
        }
    }
}
