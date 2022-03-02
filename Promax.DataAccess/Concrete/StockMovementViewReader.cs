using Extensions;
using FirebirdSql.Data.FirebirdClient;
using Promax.Entities;
using System;

namespace Promax.DataAccess
{
    public class StockMovementViewReader : FirebirdViewReaderBase<StockMovementDTO>, IStockMovementViewReader
    {
        public StockMovementViewReader(string tableName) : base(tableName, Infrastructure.Firebird)
        {
        }

        protected override StockMovementDTO ConvertRowToEntity(FbDataReader reader)
        {
            StockMovementDTO a = new StockMovementDTO();
            reader["INV_DATE"].DoIf(x => x != DBNull.Value, x => a.InvDate = DateTime.Parse(x.ToString()));
            reader["STOCK_ID"].DoIf(x => x != DBNull.Value, x => a.StockId = int.Parse(x.ToString()));
            reader["SILO_ID"].DoIf(x => x != DBNull.Value, x => a.StockId = int.Parse(x.ToString()));
            reader["STOCK_CAT_NUM"].DoIf(x => x != DBNull.Value, x => a.StockCatNum = int.Parse(x.ToString()));
            reader["STOCK_NAME"].DoIf(x => x != DBNull.Value, x => a.StockName = x.ToString());
            reader["QUANTITY"].DoIf(x => x != DBNull.Value, x => a.Quantity = double.Parse(x.ToString()));
            //a.InvDate = DateTime.Parse(reader["INV_DATE"].ToString());
            //a.StockId = (int)reader["STOCK_ID"];
            //a.SiloId = (int)reader["SILO_ID"];
            //a.StockCatNum = int.Parse(reader["STOCK_CAT_NUM"].ToString());
            //a.StockName = (string)reader["STOCK_NAME"];
            //a.Quantity = double.Parse(reader["QUANTITY"].ToString());
            return a;
        }
    }
}
