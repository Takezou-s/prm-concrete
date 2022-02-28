using Extensions;
using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class SiloDTO
    {
        private string _isStock="false";
        private string _isActive="false";
        private string _enabled="false";

        public int SiloId { get; set; }
        public int WegId { get; set; }
        public string SiloName { get; set; }
        public int SiloNo { get; set; }
        public string IsStock { get => _isStock; set => _isStock = value.Boolify(); }
        public string IsActive { get => _isActive; set => _isActive = value.Boolify(); }
        public int StockId { get; set; }
        public double Capacity { get; set; }
        public int Scale { get; set; }
        public double FastVal { get; set; }
        public int VibOn { get; set; }
        public int VibOff { get; set; }
        public double VibFl { get; set; }
        public int SwingOn { get; set; }
        public int SwingOff { get; set; }
        public double SwingVal { get; set; }
        public double TolVal { get; set; }
        public double ShotVal { get; set; }
        public double ManNem { get; set; }
        public int NemId { get; set; }
        public double MinDebi { get; set; }
        public string Enabled { get => _enabled; set => _enabled = value.Boolify(); }
        public double Temp { get; set; }
        public double Balance { get; set; }
    }
}
