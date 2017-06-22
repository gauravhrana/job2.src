using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Core;

namespace Framework.Components.Core
{
	public partial class SystemForeignRelationshipDatabaseDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SystemForeignRelationshipDatabaseDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SystemForeignRelationshipDatabase");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SystemForeignRelationshipDatabaseDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SystemForeignRelationshipDatabaseDataModel.DataColumns.SystemForeignRelationshipDatabaseId:
					if (data.SystemForeignRelationshipDatabaseId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemForeignRelationshipDatabaseDataModel.DataColumns.SystemForeignRelationshipDatabaseId, data.SystemForeignRelationshipDatabaseId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipDatabaseDataModel.DataColumns.SystemForeignRelationshipDatabaseId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<SystemForeignRelationshipDatabaseDataModel> GetEntityDetails(SystemForeignRelationshipDatabaseDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SystemForeignRelationshipDatabaseSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SystemForeignRelationshipDatabaseId                  = dataQuery.SystemForeignRelationshipDatabaseId
				 ,	Name                    = dataQuery.Name
			};

			List<SystemForeignRelationshipDatabaseDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SystemForeignRelationshipDatabaseDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SystemForeignRelationshipDatabaseDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SystemForeignRelationshipDatabaseDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SystemForeignRelationshipDatabaseDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static SystemForeignRelationshipDatabaseDataModel GetDetails(SystemForeignRelationshipDatabaseDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(SystemForeignRelationshipDatabaseDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SystemForeignRelationshipDatabaseInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SystemForeignRelationshipDatabaseUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SystemForeignRelationshipDatabaseDataModel.DataColumns.SystemForeignRelationshipDatabaseId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SystemForeignRelationshipDatabaseDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SystemForeignRelationshipDatabase.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SystemForeignRelationshipDatabaseDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SystemForeignRelationshipDatabase.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SystemForeignRelationshipDatabaseDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SystemForeignRelationshipDatabaseDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SystemForeignRelationshipDatabaseId  = data.SystemForeignRelationshipDatabaseId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SystemForeignRelationshipDatabaseDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SystemForeignRelationshipDatabaseDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SystemForeignRelationshipDatabaseRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("SystemForeignRelationshipDatabase.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(SystemForeignRelationshipDatabaseDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SystemForeignRelationshipDatabaseChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, SystemForeignRelationshipDatabaseDataModel.DataColumns.SystemForeignRelationshipDatabaseId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(SystemForeignRelationshipDatabaseDataModel data, RequestProfile requestProfile)
		{
			var isDeletable = true;
			var ds = GetChildren(data, requestProfile);
			if (ds != null && ds.Tables.Count > 0)
			{
				foreach (DataTable dt in ds.Tables)
				{
					if (dt.Rows.Count > 0)
					{
						isDeletable = false;
						break;
					}
				}
			}
			return isDeletable;
		}

		#endregion

	}
}
