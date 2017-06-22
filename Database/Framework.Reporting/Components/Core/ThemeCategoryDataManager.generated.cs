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
	public partial class ThemeCategoryDataManager : StandardDataManager
	{

		private static string DataStoreKey = string.Empty;

		static ThemeCategoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ThemeCategory");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ThemeCategoryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ThemeCategoryDataModel.DataColumns.ThemeCategoryId:
					if (data.ThemeCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ThemeCategoryDataModel.DataColumns.ThemeCategoryId, data.ThemeCategoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ThemeCategoryDataModel.DataColumns.ThemeCategoryId);
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

		public static List<ThemeCategoryDataModel> GetEntityDetails(ThemeCategoryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ThemeCategorySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ThemeCategoryId                  = dataQuery.ThemeCategoryId
				 ,	Name                    = dataQuery.Name
			};

			List<ThemeCategoryDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ThemeCategoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var list = GetEntityDetails(ThemeCategoryDataModel.Empty, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Search

		public static DataTable Search(ThemeCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(ThemeCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static string Save(ThemeCategoryDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ThemeCategoryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ThemeCategoryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ThemeCategoryDataModel.DataColumns.ThemeCategoryId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ThemeCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("ThemeCategory.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ThemeCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("ThemeCategory.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ThemeCategoryDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ThemeCategoryDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ThemeCategoryId  = data.ThemeCategoryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static DataTable DoesExist(ThemeCategoryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ThemeCategoryDataModel();
			doesExistRequest.Name = data.Name;
			return Search(doesExistRequest, requestProfile);
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ThemeCategoryRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("ThemeCategory.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(ThemeCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ThemeCategoryChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, ThemeCategoryDataModel.DataColumns.ThemeCategoryId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ThemeCategoryDataModel data, RequestProfile requestProfile)
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
