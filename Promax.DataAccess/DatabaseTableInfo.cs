using System.Collections.Generic;

namespace Promax.DataAccess
{
    internal class DatabaseTableInfo
    {
        private Dictionary<string, string> _columns = new Dictionary<string, string>();

        internal DatabaseTableInfo(string name, params KeyValuePair<string, string>[] columnInfos)
        {
            foreach (var columnInfo in columnInfos)
            {
                AddColumn(columnInfo.Key, columnInfo.Value);
            }
            Name = name;
        }

        internal DatabaseTableInfo()
        {

        }

        public string Name { get; set; }
        public void AddColumn(string propertyName, string columnName)
        {
            if (_columns.ContainsKey(propertyName))
                return;
            _columns.Add(propertyName, columnName);
        }
        public void RemoveColumn(string propertyName, string columnName)
        {
            if (!_columns.ContainsKey(propertyName))
                return;
            _columns.Remove(propertyName);
        }
        public string GetColumnName(string propertyName)
        {
            if (_columns.ContainsKey(propertyName))
                return _columns[propertyName];
            else
                return null;
        }
    }
}

