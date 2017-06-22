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
	public partial class FieldConfigurationModeAccessModeDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static FieldConfigurationModeAccessModeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationModeAccessMode");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(FieldConfigurationModeAccessModeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FieldConfigurationModeAccessModeDataModel.DataColumns.FieldConfigurationModeAccessModeId:
					if (data.FieldConfigurationModeAccessModeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeAccessModeDataModel.DataColumns.FieldConfigurationModeAccessModeId, data.FieldConfigurationModeAccessModeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeAccessModeDataModel.DataColumns.FieldConfigurationModeAccessModeId);
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

		public static List<FieldConfigurationModeAccessModeDataModel> GetEntityDetails(FieldConfigurationModeAccessModeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FieldConfigurationModeAccessModeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	FieldConfigurationModeAccessModeId                  = dataQuery.FieldConfigurationModeAccessModeId
				 ,	Name                    = dataQuery.Name
			};

			List<FieldConfigurationModeAccessModeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FieldConfigurationModeAccessModeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<FieldConfigurationModeAccessModeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(FieldConfigurationModeAccessModeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(FieldConfigurationModeAccessModeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static FieldConfigurationModeAccessModeDataModel GetDetails(FieldConfigurationModeAccessModeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(FieldConfigurationModeAccessModeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FieldConfigurationModeAccessModeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FieldConfigurationModeAccessModeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, FieldConfigurationModeAccessModeDataModel.DataColumns.FieldConfigurationModeAccessModeId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(FieldConfigurationModeAccessModeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("FieldConfigurationModeAccessMode.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(FieldConfigurationModeAccessModeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("FieldConfigurationModeAccessMode.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(FieldConfigurationModeAccessModeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.FieldConfigurationModeAccessModeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   FieldConfigurationModeAccessModeId  = data.FieldConfigurationModeAccessModeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(FieldConfigurationModeAccessModeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FieldConfigurationModeAccessModeDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FieldConfigurationModeAccessModeRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("FieldConfigurationModeAccessMode.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(FieldConfigurationModeAccessModeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FieldConfigurationModeAccessModeChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, FieldConfigurationModeAccessModeDataModel.DataColumns.FieldConfigurationModeAccessModeId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FieldConfigurationModeAccessModeDataModel data, RequestProfile requestProfile)
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
