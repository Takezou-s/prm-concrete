using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.UI
{
    public class BatchedStockGridAdapter
    {
        private Dictionary<int, double> _batchedQuantities = new Dictionary<int, double>();
        public int BatchNo { get; set; }
        public DateTime BatchedTime { get; set; }
        public IReadOnlyDictionary<int, double> BatchedQuantities => _batchedQuantities;
        public double Sum
        {
            get
            {
                double result = 0;
                _batchedQuantities.Values.ToList().ForEach(x => result += x);
                return result;
            }
        }
        public void EnterBatchedQuantity(int id, double quantity)
        {
            _batchedQuantities.DoIfElse(x => x.ContainsKey(id), x => x[id] = quantity, x => x.Add(id, quantity));
        }
        public double GetBatchedQuantity(int id)
        {
            double result = 0;
            BatchedQuantities.DoIf(x => x.ContainsKey(id), x => result = x[id]);
            return result;
        }
    }
}