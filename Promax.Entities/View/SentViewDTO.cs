using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class SentViewDTO
    {
        public int ClientId { get; set; }
        public string SiteName { get; set; }
        public DateTime ProductDate { get; set; }
        public DateTime ProductTime { get; set; }
        public string RecipeName { get; set; }
        public string MixerServiceName { get; set; }
        public int Tour { get; set; }
        public double Shipped { get; set; }
        public double Returned { get; set; }
        public double Delivered { get; set; }
    }
}