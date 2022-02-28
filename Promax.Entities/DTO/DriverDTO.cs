using Extensions;
using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class DriverDTO
    {
        private string _isHidden = "false";
        private string _gravity = "false";

        public int DriverId { get; set; } = -1;
        public string DriverCode { get; set; }
        public string DriverName { get; set; }
        public string Identity { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Gravity { get => _gravity; set => _gravity = value.Boolify(); }
        public string IsHidden { get => _isHidden; set => _isHidden = value.Boolify(); }
    }
}
