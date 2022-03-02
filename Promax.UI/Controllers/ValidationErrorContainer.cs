using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.UI
{
    public class ValidationErrorContainer
    {
        List<ValidError> _errorProperties = new List<ValidError>();
        public event EventHandler ErrorOccured;
        public IEnumerable<ValidError> ErrorProperties => _errorProperties;
        public void SetErrors(params ValidError[] properties)
        {
            _errorProperties.Clear();
            _errorProperties.AddRange(properties);
            ErrorOccured?.Invoke(this, null);
        }
    }
    public class ValidError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
    }
}
