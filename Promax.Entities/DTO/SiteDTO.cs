using Extensions;
using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class SiteDTO
    {
        private string _isHidden = "false";

        public int SiteId { get; set; } = -1;
        public int ClientId { get; set; }
        public string SiteCode { get; set; }
        public string SiteName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public int Distance { get; set; }
        public string IsHidden { get => _isHidden; set => _isHidden = value.Boolify(); }
    }
}
