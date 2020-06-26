using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace SpiderCommon
{
    public class SqlBulkHelper
    {
        public static bool BuliInsertTran<T>(List<T> list, string sql, string tableName, string connStr, params SqlParameter[] ps)
        {
            var dt = ConvertToDataTable(list);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.BatchSize = 10;
                        bulkCopy.DestinationTableName = tableName;
                        try
                        {
                            if (!string.IsNullOrEmpty(sql))
                            {
                                using (SqlCommand command = conn.CreateCommand())
                                {
                                    command.Transaction = transaction;
                                    command.Parameters.AddRange(ps);
                                    command.CommandText = sql;
                                    command.ExecuteNonQuery();
                                }
                            }
                            foreach (DataColumn dc in dt.Columns)
                            {
                                bulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                            }
                            bulkCopy.WriteToServer(dt);
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message+sql);

                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }
        }

        public static bool BuliInsertTran(DataTable dt, string sql, string tableName, string connStr, params SqlParameter[] ps)
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                    {

                        bulkCopy.BatchSize = 10;
                        bulkCopy.DestinationTableName = tableName;
                        try
                        {
                            if (!string.IsNullOrEmpty(sql))
                            {
                                using (SqlCommand command = conn.CreateCommand())
                                {
                                    command.Transaction = transaction;
                                    command.Parameters.AddRange(ps);
                                    command.CommandText = sql;
                                    command.ExecuteNonQuery();
                                }
                            }
                            foreach (DataColumn dc in dt.Columns)
                            {
                                bulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                            }
                            bulkCopy.WriteToServer(dt);


                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// SqlBulkCopy 批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="tableName"></param>
        public static void BulkInsertData<T>(List<T> list, string tableName, string connect = "Data Source=192.168.0.18;Initial Catalog=onlinedata_historical;User Id=xhlsdata;Password=xhlsdata987654321;")
        {
            var dataTable = ConvertToDataTable(list);
            using (var bulkCopy = new SqlBulkCopy(connect))
            {
                foreach (DataColumn dcPrepped in dataTable.Columns)
                {
                    bulkCopy.ColumnMappings.Add(dcPrepped.ColumnName, dcPrepped.ColumnName);
                }
                bulkCopy.BulkCopyTimeout = 660;
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.WriteToServer(dataTable);
            }
        }
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            var properties = typeof(T).GetProperties().Where(p => !p.IsDefined(typeof(JsonIgnoreAttribute), true));// TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();

            foreach (PropertyInfo prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                var row = table.NewRow();
                foreach (PropertyInfo info in properties)
                {
                    row[info.Name] = info.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
