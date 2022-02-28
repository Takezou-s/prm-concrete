using Extensions;
using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class ServiceDTO
    {
        private string _isHidden = "false";
        private string _gravity = "false";

        public int ServiceId { get; set; } = -1;
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public int ServiceCatNum { get; set; }
        public string LicencePlate { get; set; }
        public int DriverId { get; set; }
        public double Capacity { get; set; }
        public string Gravity { get => _gravity; set => _gravity = value.Boolify(); }
        public string IsHidden { get => _isHidden; set => _isHidden = value.Boolify(); } 
    }
}
