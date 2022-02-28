using Extensions;
using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class ClientDTO
    {
        private string _gravity = "false";
        private string _isHidden = "false";
        private string _enableNotification = "false";

        public int ClientId { get; set; } = -1;
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string TaxLocation { get; set; }
        public string TaxNumber { get; set; }
        public string Gravity { get => _gravity; set => _gravity = value.Boolify(); }
        public string IsHidden { get => _isHidden; set => _isHidden = value.Boolify(); }
        public short ClientType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClientTitle { get; set; }
        public string Email { get; set; }
        public string EnableNotification { get => _enableNotification; set => _enableNotification = value.Boolify(); }
        public short MailAttachInfo { get; set; }
    }
}
