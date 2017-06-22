using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Dapper;
using System.Data;
using Framework.CommonServices.BusinessDomain.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Configuration;

namespace Framework.Components.Configuration
{
	public partial class DBProjectNameDataManager : StandardDataManager
	{		
		static readonly string DataStoreKey = "";
		static readonly int ApplicationId;

		private static DataTable DBProjectNameList;

		static DBProjectNameDataManager()
		{
			ApplicationId = SetupConfiguration.ApplicationId;
			DataStoreKey = SetupConfiguration.GetDataStoreKey("DBProjectName");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(DBProjectNameDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DBProjectNameDataModel.DataColumns.DBProjectNameId:
					if (data.DBProjectNameId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DBProjectNameDataModel.DataColumns.DBProjectNameId, data.DBProjectNameId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DBProjectNameDataModel.DataColumns.DBProjectNameId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		#endregion

		#region GetList

		public static DataTable GetList(int auditId, bool forceRefresh = false)
		{
			if (DBProjectNameList == null || forceRefresh)
			{
				var sql = "EXEC dbo.DBProjectNameSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId);

				var oDT = new DBDataTable("DBProjectName.List", sql, DataStoreKey);

				DBProjectNameList = oDT.DBTable;
			}

			// return clone copy in case databound ... but still should not be required
			return DBProjectNameList.Clone();
		}

		#endregion

		#region GetEntitySearch

		static public List<DBProjectNameDataModel> GetEntityDetails(DBProjectNameDataModel dataQuery, int auditId, int applicationModeId = 0)
		{
			const string sql = @"dbo.DBProjectNameSearch ";

			var parameters =
			new
			{							
					AuditId				= auditId	
				,	ApplicationId		= ApplicationId		
				,	DBProjectNameId		= dataQuery.DBProjectNameId
				,	Name				= dataQuery.Name																			
			};
						
			List<DBProjectNameDataModel> result;
			
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				var sp = Log4Net.LogSQLInfoStart(sql, DataStoreKey, parameters);

				result = dataAccess.Connection.Query<DBProjectNameDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();

				Log4Net.LogSQLInfoStop(sp, result.Count);
			}
			
			return result;		
		}

		#endregion

		#region ToList

		//static private List<DBProjectNameDataModel> ToList(DataTable dt)
		//{
		//	var list = new List<DBProjectNameDataModel>();

		//	if (dt != null && dt.Rows.Count > 0)
		//	{
		//		foreach (DataRow dr in dt.Rows)
		//		{
		//			var dataItem = new DBProjectNameDataModel();

		//			dataItem.DBProjectNameId = (int?)dr[DBProjectNameDataModel.DataColumns.DBProjectNameId];

		//			SetStandardInfo(dataItem, dr);

		//			list.Add(dataItem);
		//		}
		//	}
		//	return list;
		//}

		#endregion


		#region GetDetails

		public static DataTable GetDetails(DBProjectNameDataModel data, int auditId)
		{
			var sql = "EXEC dbo.DBProjectNameSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				//"," + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo ,BaseDataManager .ReturnAduitInfoOnDetails ) +
				", " + ToSQLParameter(data, DBProjectNameDataModel.DataColumns.DBProjectNameId);

			var oDt = new DBDataTable("DBProjectName.Details", sql, DataStoreKey);
			return oDt.DBTable;
		}

		//public static List<DBProjectNameDataModel> GetEntityDetails(DBProjectNameDataModel data, int auditId, int applicationModeId = 0)
		//{
		//	var sql = "EXEC dbo.DBProjectNameSearch " +
		//			  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
		//			  //", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, applicationModeId) +
		//			  ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
		//			  ", " + ToSQLParameter(data, DBProjectNameDataModel.DataColumns.DBProjectNameId);

		//	var result = new List<DBProjectNameDataModel>();

		//	using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
		//	{
		//		var dbReader = reader.DBReader;

		//		while (dbReader.Read())
		//		{
		//			var dataItem = new DBProjectNameDataModel();

		//			dataItem.DBProjectNameId = (int)dbReader[DBProjectNameDataModel.DataColumns.DBProjectNameId];

		//			SetStandardInfo(dataItem, dbReader);

		//			/*BaseData.SetBaseInfo(dataItem, dbReader);
		//			*/
		//			result.Add(dataItem);
		//		}
		//	}

		//	return result;
		//}

		#endregion

		#region Create

		public static void Create(DBProjectNameDataModel data, int auditId)
		{
			var sql = Save(data, auditId, "Create");
			DBDML.RunSQL("DBProjectName.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(DBProjectNameDataModel data, int auditId)
		{
			var sql = Save(data, auditId, "Update");
			DBDML.RunSQL("DBProjectName.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(DBProjectNameDataModel dataQuery, int auditId)
		{			
			const string sql = @"dbo.DBProjectNameDelete ";

			var parameters =
			new
			{							
					AuditId				= auditId					
				,	DBProjectNameId		= dataQuery.DBProjectNameId				
			};
												
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				var sp = Log4Net.LogSQLInfoStart(sql, DataStoreKey, parameters);

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				Log4Net.LogSQLInfoStop(sp);
			}						
		}

		#endregion

		#region Search

		public static DataTable Search(DBProjectNameDataModel data, int auditId, int applicationModeId = 0)
		{
			// formulate SQL
			var sql = "EXEC dbo.DBProjectNameSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, applicationModeId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data, DBProjectNameDataModel.DataColumns.DBProjectNameId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

			var oDT = new DBDataTable("DBProjectName.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Save

		private static string Save(DBProjectNameDataModel data, int auditId, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.DBProjectNameInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId);
					break;

				case "Update":
					sql += "dbo.DBProjectNameUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, DBProjectNameDataModel.DataColumns.DBProjectNameId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(DBProjectNameDataModel data, int auditId)
		{
			var sql = "EXEC dbo.DBProjectNameSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, DBProjectNameDataModel.DataColumns.DBProjectNameId);

			var oDT = new DBDataTable("DBProjectName.DoesExist", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion
	}
}

//LogMe();
//using (var dataAccess = new DataAccessBase(DataStoreKey))
//{
//	const string sql = @"dbo.DBProjectNameSearch "				+
//	" @" + BaseDataModel.BaseDataColumns.AuditId				+ " =  @" + BaseDataModel.BaseDataColumns.AuditId +
//	", @" + BaseDataModel.BaseDataColumns.ApplicationId			+ " =  @" + BaseDataModel.BaseDataColumns.ApplicationId +
//	", @" + DBProjectNameDataModel.DataColumns.DBProjectNameId	+ " =  @" + DBProjectNameDataModel.DataColumns.DBProjectNameId +				
//	", @" + StandardDataModel.StandardDataColumns.Name			+ " =  @" + StandardDataModel.StandardDataColumns.Name;

//	var result = dataAccess.Connection.Query<DBProjectNameDataModel>(sql,
//		new
//		{
//				AuditId			= auditId
//			,	ApplicationId	= ApplicationId
//			,	DBProjectNameId = dataQuery.DBProjectNameId
//			,	Name			= dataQuery.Name
//		}
//		).ToList();
		
//	return result;
//}

//private static void LogMe(
//		[CallerFilePath] string sourceFile = "",
//		[CallerLineNumber] int sourceLineNo = 0,
//		[CallerMemberName] string memberName = "")
//		{
//			Console.WriteLine("Member Name: {0}", memberName);
//			Console.WriteLine("Line No: {0}", sourceLineNo);
//			Console.WriteLine("File name: {0}", Path.GetFileName(sourceFile));
//			Console.WriteLine();
//		}