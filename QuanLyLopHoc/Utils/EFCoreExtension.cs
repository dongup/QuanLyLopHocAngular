using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace QuanLyLopHoc.Utils
{
    public static class EFCoreExtension
    {
        public static DataTable ExecStoreProcedure(this DbContext context, string procName,
                                     params object[] prms)
        {

            procName += " ";
            procName += string.Join(", ", prms.Select(x => $"'{x?.ToString()}'"));
            //Console.WriteLine($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: Executing query: {sqlQuery}");

            DataTable dataTable = new DataTable();
            DbConnection connection = context.Database.GetDbConnection();
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connection);
            using (var cmd = dbFactory.CreateCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = procName;
                using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }
            }
            //Console.WriteLine($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]: Executed query: {sqlQuery}");

            return dataTable;
        }

        public static DataTable GetDateTable(this DbContext context, string sqlQuery,
                                     params object[] prms)
        {
            sqlQuery = String.Format(sqlQuery, prms.Select(x => x.ToString()).ToArray());

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


        public static List<T> FromProcedure<T>(this DbContext context, string procName, params object[] param) where T : class
        {
            DataTable data = context.ExecStoreProcedure(procName, param);
            return data.ToList<T>();
        }

        public static List<T> FromExcel<T>(this DbContext context, string filePath)
        {
            ExcelService excel = new ExcelService();
            DataTable data = excel.ReadData(filePath);
            return data.ToList<T>();
        }
        public static DataTable GetDataTableFromExcel(this DbContext context, string filePath)
        {
            ExcelService excel = new ExcelService();
            DataTable data = excel.ReadData(filePath);
            return data;
        }

        /// <summary>
        /// Đọc dữ liệu trong file excel và trả về list động
        /// </summary>
        /// <param name="context">dbcontext</param>
        /// <param name="filePath">Đường dẫn file</param>
        /// <returns></returns>
        public static IEnumerable<dynamic> EnumsFromExcel(this DbContext context, string filePath)
        {
            ExcelService excel = new ExcelService();
            DataTable data = excel.ReadData(filePath);
            return data.AsDynamicEnumerable();
        }
     
        public static IEnumerable<dynamic> AsDynamicEnumerable(this DataTable table)
        {
            // Validate argument here..

            return table.AsEnumerable().Select(row => new DynamicRow(row));
        }

        private sealed class DynamicRow : DynamicObject
        {
            private readonly DataRow _row;

            internal DynamicRow(DataRow row) { _row = row; }

            // Interprets a member-access as an indexer-access on the 
            // contained DataRow.
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                string[] columnNames = _row.Table.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();

                //Console.WriteLine("prop name: " + binder.Name);
                string slctedCol = columnNames
                    .FirstOrDefault(x => x?.RemoveUnicode()?.ToLower()?.ToTitleCase()?.RemoveSpace() == binder.Name);

                //Console.WriteLine("cols: " + string.Join(",", columnNames));
                Console.WriteLine("cols parsed: " + string.Join(",", columnNames.Select(x => x?.RemoveUnicode()?.ToLower()?.ToTitleCase()?.RemoveSpace()).ToList()));

                bool retVal = !string.IsNullOrEmpty(slctedCol);
                //Console.WriteLine("slect collumn: "  + slctedCol);

                result = retVal ? _row[slctedCol].ToString() : null;
                return true;
            }
        }


        private static List<T> ToList<T>(this DataTable dt)
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

                    if (pro.Name == collumnName)
                        pro.SetValue(obj, dr[column.ColumnName] is DBNull ? null : dr[column.ColumnName], null);
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
