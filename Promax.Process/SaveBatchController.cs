using Promax.Business;
using Promax.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualPLC;

namespace Promax.Process
{
    public class SaveBatchController : VirtualPLCObject
    {
        private IBatchedStockManager _batchedStockManager;

        public IReadOnlyDictionary<Silo, SiloController> SiloControllers { get; set; }
        public Product Product { get; set; }
        public UserDTO User { get; set; }
        public SaveBatchController(VirtualController controller, IReadOnlyDictionary<Silo, SiloController> siloControllers, IBatchedStockManager batchedStockManager) : base(controller)
        {
            SiloControllers = siloControllers;
            _batchedStockManager = batchedStockManager;
        }
        public void Check()
        {
            foreach (var item in SiloControllers)
            {
                var silo = item.Key;
                var siloController = item.Value;
                if (!siloController.SaveBatched)
                    continue;
                siloController.SaveBatched = false;
                if (siloController.StockController == null || _batchedStockManager == null)
                    continue;
                _batchedStockManager.Add(new BatchedStock()
                {
                    Product = Product,
                    BatchNo = siloController.Periyot,
                    Stock = siloController.StockController.Stock,
                    Silo = silo,
                    User = User,
                    BatchedDate = DateTime.Now,
                    BatchedTime = DateTime.Now,
                    AddVal = siloController.StockController.İlaveMiktar,
                    Design = siloController.StockController.İstenen,
                    Batched = siloController.StockController.Ölçülen
                });

            }
        }
    }
}
