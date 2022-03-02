using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.DataAccess
{
    public abstract class FirebirdViewReaderBase<T> : IViewReader<T>
    {
        protected FirebirdViewReaderBase(string tableName, string nameOrConnectionString)
        {
            TableName = tableName;
            try
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[nameOrConnectionString].ConnectionString;
            }
            catch
            {
                ;
            }
            if (string.IsNullOrEmpty(ConnectionString))
                ConnectionString = nameOrConnectionString;
        }

        public string TableName { get; private set; }
        public string ConnectionString { get; private set; }
        public virtual T Get(string filter)
        {
            T result = default(T);
            using (FbConnection connection = new FbConnection(ConnectionString))
            {
                connection.Open();
                string query = GetQueryLimited(filter);
                using (FbCommand command = new FbCommand(query, connection))
                {
                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = ConvertRowToEntity(reader);
                            break;
                        }
                    }
                }
            }
            return result;
        }
        public virtual List<T> GetList(string filter = null)
        {
            List<T> list = new List<T>();
            using (FbConnection connection = new FbConnection(ConnectionString))
            {
                connection.Open();
                string query = GetQuery(filter);
                using (FbCommand command = new FbCommand(query, connection))
                {
                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T entity = ConvertRowToEntity(reader);
                            list.Add(entity);
                        }
                    }
                }
            }
            return list;
        }
        protected abstract T ConvertRowToEntity(FbDataReader reader);
        protected virtual string GetQuery(string filter)
        {
            string query = "select * from " + TableName + " ";
            if (!string.IsNullOrEmpty(filter))
            {
                query += filter;
            }
            return query;
        }
        protected virtual string GetQueryLimited(string filter)
        {
            string query = "select first 1 * from " + TableName + " ";
            if (!string.IsNullOrEmpty(filter))
            {
                query += filter;
            }
            return query;
        }
    }
}
