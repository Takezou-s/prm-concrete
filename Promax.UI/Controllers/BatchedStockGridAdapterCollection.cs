using Extensions;
using Promax.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Promax.UI
{
    public class BatchedStockGridAdapterCollection
    {
        private Dictionary<int, BatchedStockGridAdapter> _batchedStocks = new Dictionary<int, BatchedStockGridAdapter>();
        public IEnumerable<int> StockIds
        {
            get
            {
                List<int> list = new List<int>();
                foreach (var item in BatchedStocks)
                {
                    var obj = item.BatchedQuantities.Keys;
                    foreach (var item1 in obj)
                    {
                        list.DoIf(x => !x.Contains(item1), x => x.Add(item1));
                    }
                }
                return list;
            }
        }
        public IReadOnlyDictionary<int, double> StockSums
        {
            get
            {
                Dictionary<int, double> dic = new Dictionary<int, double>();
                foreach (var item in StockIds)
                {
                    double sum = 0;
                    foreach (var item1 in BatchedStocks)
                    {
                        sum += item1.GetBatchedQuantity(item);
                    }
                    dic.Add(item, sum);
                }
                return dic;
            }
        }
        public IEnumerable<BatchedStockGridAdapter> BatchedStocks => _batchedStocks.Values;
        public void AddBatchedStock(BatchedStock batchedStock)
        {
            if (batchedStock == null)
                return;
            if (!_batchedStocks.ContainsKey(batchedStock.BatchNo))
            {
                var a = new BatchedStockGridAdapter();
                a.BatchNo = batchedStock.BatchNo;
                a.BatchedTime = batchedStock.BatchedTime;
                a.EnterBatchedQuantity(batchedStock.StockId, batchedStock.Batched);
                _batchedStocks.Add(batchedStock.BatchNo, a);
            }
            else
            {
                _batchedStocks[batchedStock.BatchNo].EnterBatchedQuantity(batchedStock.StockId, batchedStock.Batched);
            }
        }
        public double GetStockSum(int id)
        {
            double result = 0;
            StockSums.DoIf(x => x.ContainsKey(id), x => result = x[id]);
            return result;
        }
    }
}
