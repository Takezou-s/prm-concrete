using Promax.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Entities
{
    public class StockEntry : ICloneable
    {
        private Stock _malzeme;
        private Silo _silo;
        private UserDTO _user;
        #region DTO
        public int EntryId { get; set; } = -1;
        public int StockId { get; set; } = -1;
        public int SiloId { get; set; } = -1;
        public int UserId { get; set; } = -1;
        public DateTime EntryDate { get; set; }
        public DateTime EntryTime { get; set; }
        public string DocumentNo { get; set; }
        public string Description { get; set; }
        public double Entry { get; set; }
        public double Minus { get; set; }
        #endregion
        public Stock Stock
        {
            get => _malzeme;
            set
            {
                _malzeme = value;
                value.Do(m => StockId = m.StockId, () => StockId = -1);
            }
        }
        public Silo Silo
        {
            get => _silo;
            set
            {
                _silo = value;
                value.Do(m => SiloId = m.SiloId, () => SiloId = -1);
            }
        }
        public UserDTO User
        {
            get => _user;
            set
            {
                _user = value;
                value.Do(m => UserId = m.UserId, () => UserId = -1);
            }
        }

        public object Clone()
        {
            return new StockEntry()
            {
                EntryId = this.EntryId,
                StockId = this.StockId,
                SiloId = this.SiloId,
                UserId = this.UserId,
                EntryDate = this.EntryDate,
                EntryTime = this.EntryTime,
                DocumentNo = this.DocumentNo,
                Description = this.Description,
                Entry = this.Entry,
                Minus = this.Minus,
                Stock = this.Stock,
                Silo = this.Silo,
                User = this.User
            };
        }
    }
}
