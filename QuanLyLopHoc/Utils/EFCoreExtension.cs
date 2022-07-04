using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace QuanLyLopHoc.Utils
{
    public static class EFCoreExtension
    {
        public static DataTable GetDataTable(this DbContext context, string sqlQuery,
                                     params object[] prms)
        {

            sqlQuery += " ";
            sqlQuery += string.Join(", ", prms.Select(x => $"'{x?.ToString()}'"));
            //Console.WriteLine($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: Executing query: {sqlQuery}");

            DataTable dataTable = new DataTable();
            DbConnection connection = context.Database.GetDbConnection();
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connection);
            using (var cmd = dbFactory.CreateCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }
            }
            //Console.WriteLine($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: Executed query: {sqlQuery}");

            return dataTable;
        }

        public static List<T> FromProcedure<T>(this DbContext context, string procName, params object[] param) where T: class
        {
            DataTable data = context.GetDataTable(procName, param);
            return ConvertDataTable<T>(data);
        }

        public static List<T> FromExcel<T>(this DbContext context, string filePath)
        {
            DataTable data = ExcelHelper.ReadData(filePath);
            return ConvertDataTable<T>(data);
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    string collumnName = column.ColumnName?.RemoveUnicode()?.ToTitleCase()?.RemoveSpace();
                    Console.WriteLine(collumnName);

                    if (pro.Name == collumnName)
                        pro.SetValue(obj, dr[column.ColumnName] is DBNull? null : dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static int ExecuteNonQuery(this DbContext context, string sqlQuery, params object[] prms)
        {
            int rowEffected = 0;
            sqlQuery += " ";
            sqlQuery += string.Join(", ", prms.Select(x => $"'{x}'"));

            DbConnection connection = context.Database.GetDbConnection();
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connection);
            using (DbCommand cmd = dbFactory.CreateCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                rowEffected = cmd.ExecuteNonQuery();
            }

            //var stats = connection.RetrieveStatistics();
            //var firstCommandExecutionTimeInMs = (long)stats["ExecutionTime"];

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return rowEffected;
        }

        public static object ExecuteScalar(this DbContext context, string sqlQuery, params object[] prms)
        {
            object result = 0;
            sqlQuery += " ";
            sqlQuery += string.Join(", ", prms.Select(x => $"'{x}'"));

            DbConnection connection = context.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connection);
            using (DbCommand cmd = dbFactory.CreateCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                result = cmd.ExecuteScalar();
            }

            //var stats = connection.RetrieveStatistics();
            //var firstCommandExecutionTimeInMs = (long)stats["ExecutionTime"];

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public static string ToJsonString(this DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return JsonConvert.SerializeObject(rows);
        }
    }
}
