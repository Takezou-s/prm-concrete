using Extensions;
using FirebirdSql.Data.FirebirdClient;
using Promax.Entities;
using System;

namespace Promax.DataAccess
{
    public class SentViewReader : FirebirdViewReaderBase<SentViewDTO>, ISentViewReader
    {
        public SentViewReader(string tableName) : base(tableName, Infrastructure.Firebird)
        {
        }

        protected override SentViewDTO ConvertRowToEntity(FbDataReader reader)
        {
            SentViewDTO a = new SentViewDTO();
            reader["CLIENT_ID"].DoIf(x => x != DBNull.Value, x => a.ClientId = int.Parse(x.ToString()));
            reader["SITE_NAME"].DoIf(x => x != DBNull.Value, x => a.SiteName = x.ToString());
            reader["PRODUCT_DATE"].DoIf(x => x != DBNull.Value, x => a.ProductDate = DateTime.Parse(x.ToString()));
            reader["PRODUCT_TIME"].DoIf(x => x != DBNull.Value, x => a.ProductTime = DateTime.Parse(x.ToString()));
            reader["RECIPE_NAME"].DoIf(x => x != DBNull.Value, x => a.RecipeName = x.ToString());
            reader["MIXER_SERVICE_NAME"].DoIf(x => x != DBNull.Value, x => a.MixerServiceName = x.ToString());
            reader["TOUR"].DoIf(x => x != DBNull.Value, x => a.Tour = int.Parse(x.ToString()));
            reader["SHIPPED"].DoIf(x => x != DBNull.Value, x => a.Shipped = double.Parse(x.ToString()));
            reader["RETURNED"].DoIf(x => x != DBNull.Value, x => a.Returned = double.Parse(x.ToString()));
            reader["DELIVERED"].DoIf(x => x != DBNull.Value, x => a.Delivered = double.Parse(x.ToString()));
            return a;
        }
    }
}
