using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Configuration;

namespace Framework.Components.UserPreference
{
	public partial class FieldConfigurationModeCategoryDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FieldConfigurationModeCategoryDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationModeCategory");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FieldConfigurationModeCategoryDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId:
					if (data.FieldConfigurationModeCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId, data.FieldConfigurationModeCategoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId);
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

		public static List<FieldConfigurationModeCategoryDataModel> GetEntityDetails(FieldConfigurationModeCategoryDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FieldConfigurationModeCategorySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FieldConfigurationModeCategoryId                  = dataQuery.FieldConfigurationModeCategoryId
				 ,	Name                    = dataQuery.Name
			};

			List<FieldConfigurationModeCategoryDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FieldConfigurationModeCategoryDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FieldConfigurationModeCategoryDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FieldConfigurationModeCategoryDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FieldConfigurationModeCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static FieldConfigurationModeCategoryDataModel GetDetails(FieldConfigurationModeCategoryDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(FieldConfigurationModeCategoryDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FieldConfigurationModeCategoryInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FieldConfigurationModeCategoryUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FieldConfigurationModeCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FieldConfigurationModeCategory.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FieldConfigurationModeCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FieldConfigurationModeCategory.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FieldConfigurationModeCategoryDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FieldConfigurationModeCategoryDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   FieldConfigurationModeCategoryId  = data.FieldConfigurationModeCategoryId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FieldConfigurationModeCategoryDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FieldConfigurationModeCategoryDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FieldConfigurationModeCategoryRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("FieldConfigurationModeCategory.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(FieldConfigurationModeCategoryDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FieldConfigurationModeCategoryChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FieldConfigurationModeCategoryDataModel data, RequestProfile requestProfile)
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
