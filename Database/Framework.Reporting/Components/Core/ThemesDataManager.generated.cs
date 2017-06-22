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
	public partial class ThemesDataManager : StandardDataManager
	{

		private static string DataStoreKey = string.Empty;

		static ThemesDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Themes");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ThemesDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ThemesDataModel.DataColumns.ThemesId:
					if (data.ThemesId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ThemesDataModel.DataColumns.ThemesId, data.ThemesId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ThemesDataModel.DataColumns.ThemesId);
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

		public static List<ThemesDataModel> GetEntityDetails(ThemesDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ThemesSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ThemesId                  = dataQuery.ThemesId
				 ,	Name                    = dataQuery.Name
			};

			List<ThemesDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ThemesDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var list = GetEntityDetails(ThemesDataModel.Empty, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Search

		public static DataTable Search(ThemesDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(ThemesDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static string Save(ThemesDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ThemesInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ThemesUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ThemesDataModel.DataColumns.ThemesId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ThemesDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("Themes.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ThemesDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("Themes.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ThemesDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ThemesDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ThemesId  = data.ThemesId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static DataTable DoesExist(ThemesDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ThemesDataModel();
			doesExistRequest.Name = data.Name;
			return Search(doesExistRequest, requestProfile);
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ThemesRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("Themes.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(ThemesDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ThemesChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, ThemesDataModel.DataColumns.ThemesId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ThemesDataModel data, RequestProfile requestProfile)
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
