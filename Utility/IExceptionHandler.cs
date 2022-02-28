using System;

namespace Utility
{
    public interface IExceptionHandler
    {
        void Handle(Action action);
    }
}
