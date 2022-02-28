using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Promax.DataAccess
{
    internal static class ExtensionMethods
    {
        internal static string Columnify(this string str, string columnName)
        {
            return str + "," + columnName;
        }
        internal static KeyValuePair<string, string> ToColumnInfo(this string str)
        {
            return new KeyValuePair<string, string>(str.Split(',')[0], str.Split(',')[1]);
        }
        internal static KeyValuePair<string, string> ToColumnInfo(this string str, string columnName)
        {
            return new KeyValuePair<string, string>(str, columnName);
        }

        internal static void HasColumnNameExt<T>(this StringPropertyConfiguration config, string propertyName)
        {
            config.HasColumnName(Infrastructure.GetTableInfo<T>().GetColumnName(propertyName));
        }
        internal static void HasColumnNameExt<T>(this PrimitivePropertyConfiguration config, string propertyName)
        {
            config.HasColumnName(Infrastructure.GetTableInfo<T>().GetColumnName(propertyName));
        }
        internal static void ToTableExt<T>(this EntityTypeConfiguration<T> modelBuilder) where T : class
        {
            modelBuilder.ToTable(Infrastructure.GetTableInfo<T>().Name);
        }
    }
}