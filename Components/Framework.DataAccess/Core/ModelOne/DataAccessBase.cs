using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Framework.Components.DataAccess
{
	public class DataAccessBase : IDisposable
	{
		private const int CommandTimeout = 3000;

		private SqlConnection _connection;
		private readonly string _connectionString = string.Empty;

		public DataAccessBase(string key)
		{
			_connectionString = StartUp.GetConnectionString(key);
		}

		public SqlConnection Connection
		{
			get
			{
				if (_connection != null) return _connection;

				var connection = new SqlConnection(_connectionString);
				connection.Open();

				_connection = connection;

				//_connection = new ProfiledDbConnection(connection, MiniProfiler.Current);

				return _connection;
			}
		}

		public void Dispose()
		{
			if (_connection != null)
				_connection.Close();
		}

		//public IEnumerable<dynamic> Query(string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true)
		//{
		//	return SqlMapper.Query(Connection, sql, param, transaction, buffered, CommandTimeout);
		//}
        
		//public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id")
		//{
		//	return SqlMapper.Query<TFirst, TSecond, TReturn>(Connection, sql, map, param, transaction, buffered, splitOn, CommandTimeout);
		//}

		//public IEnumerable<T> Query<T>(string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true)
		//{

		//	return SqlMapper.Query<T>(Connection, sql, param, transaction, buffered, CommandTimeout);
		//}        

		//public int Execute(string sql, dynamic param = null, IDbTransaction transaction = null)
		//{
		//	return SqlMapper.Execute(Connection, sql, param, transaction, CommandTimeout);
		//}    

		protected string[] GetColumnNames(string tableName)
		{
			var sql = string.Format(@"SELECT * FROM {0} WHERE 1 = 2", tableName);

			using (var cmd = new SqlCommand(sql, Connection))
			{
				var dt = new DataTable();

				using (var da = new SqlDataAdapter(cmd))
				{
					da.Fill(dt);
					return dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
				}
			}
		}

		public void BulkCopy(string tableName, DataTable data)
		{
			using (var bc = new SqlBulkCopy(_connectionString, SqlBulkCopyOptions.TableLock))
			{
				bc.BulkCopyTimeout = CommandTimeout;
				bc.DestinationTableName = tableName;
				
				var tableColumnsName = GetColumnNames(tableName);
				
				foreach (var column in data.Columns.Cast<DataColumn>().Where(column => tableColumnsName.Contains(column.ColumnName)))
				{
					bc.ColumnMappings.Add(column.ColumnName, column.ColumnName);
				}

				bc.WriteToServer(data);
			}
		}

		//static public List<T> GetEntitySearch<T>(T dataQuery, string sql, dynamic parameters, string dataStoreKey)
		//{
		//	

		//	List<T> result;

		//	using (var dataAccess = new DataAccessBase(dataStoreKey))
		//	{
		//		result = dataAccess.Connection.Query<T>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
		//	}

		//

		//	return result;
		//}

		static public string ToSQLInfo(object obj)
		{
			var answer = string.Empty;

			if (obj == null) return answer;

			var properties = obj.GetType().GetProperties();

			if (properties.Length == 0) return answer;

			foreach (var prop in properties)
			{
				var propValue = prop.GetValue(obj, null);

				if (prop.PropertyType == typeof(string))
				{
					if (propValue == null || ReferenceEquals(propValue, string.Empty))
					{
						answer += string.Format("\n,\t@{0} = {1}", prop.Name, "NULL");
					}
					else
					{
						answer += string.Format("\n,\t@{0} = '{1}'", prop.Name, propValue);
					}
				}
				else
				{
					if (propValue == null)
					{
						answer += string.Format("\n,\t@{0} = {1}", prop.Name, "NULL");
					}
					else
					{
						answer += string.Format("\n,\t@{0} = {1}", prop.Name, propValue);
					}
				}
			}

			return answer.Remove(0, 2);
		}
	}
}
