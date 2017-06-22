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
	public partial class SystemEntityCategoryDataManager : StandardDataManager
	{

		private static string DataStoreKey = string.Empty;

		static SystemEntityCategoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SystemEntityCategory");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SystemEntityCategoryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId:
					if (data.SystemEntityCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId, data.SystemEntityCategoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId);
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

		public static List<SystemEntityCategoryDataModel> GetEntityDetails(SystemEntityCategoryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SystemEntityCategorySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SystemEntityCategoryId                  = dataQuery.SystemEntityCategoryId
				 ,	Name                    = dataQuery.Name
			};

			List<SystemEntityCategoryDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SystemEntityCategoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var list = GetEntityDetails(SystemEntityCategoryDataModel.Empty, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Search

		public static DataTable Search(SystemEntityCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(SystemEntityCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static string Save(SystemEntityCategoryDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SystemEntityCategoryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SystemEntityCategoryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SystemEntityCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SystemEntityCategory.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SystemEntityCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SystemEntityCategory.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SystemEntityCategoryDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SystemEntityCategoryDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SystemEntityCategoryId  = data.SystemEntityCategoryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static DataTable DoesExist(SystemEntityCategoryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SystemEntityCategoryDataModel();
			doesExistRequest.Name = data.Name;
			return Search(doesExistRequest, requestProfile);
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SystemEntityCategoryRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("SystemEntityCategory.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(SystemEntityCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SystemEntityCategoryChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, SystemEntityCategoryDataModel.DataColumns.SystemEntityCategoryId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(SystemEntityCategoryDataModel data, RequestProfile requestProfile)
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
