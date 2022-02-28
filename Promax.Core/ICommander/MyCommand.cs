using System;

namespace Promax.Core
{
    public class MyCommand
    {
        public Action<object[]> Action { get; set; }
        public void Invoke(params object[] Parameters)
        {
            Action?.Invoke(Parameters);
        }
    }
}
