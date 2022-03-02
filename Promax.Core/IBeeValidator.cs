using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Core
{
    public interface IBeeValidator<T> : IValidator<T>
    {
    }
}
