using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Business
{
    public partial class CommandScope
    {

        protected List<object> RetentiveObjects { get; set; } = new List<object>();

        protected override void InitVariables()
        {
        }

        public bool IsCommandRetentive(object obj)
        {
            bool result = false;
            obj.Do(delegate (object x)
            {
                result = this.RetentiveObjects.Contains(x);
            });
            return result;
        }
    }
}
