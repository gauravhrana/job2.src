using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.AuthenticationAndAuthorization;

namespace Framework.Components.ApplicationUser
{
	public partial class ApplicationUserTitleDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static ApplicationUserTitleDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationUserTitle");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ApplicationUserTitleDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId:
					if (data.ApplicationUserTitleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId, data.ApplicationUserTitleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId);
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

		public static List<ApplicationUserTitleDataModel> GetEntityDetails(ApplicationUserTitleDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ApplicationUserTitleSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ApplicationUserTitleId                  = dataQuery.ApplicationUserTitleId
				 ,	Name                    = dataQuery.Name
			};

			List<ApplicationUserTitleDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ApplicationUserTitleDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<ApplicationUserTitleDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ApplicationUserTitleDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static ApplicationUserTitleDataModel GetDetails(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(ApplicationUserTitleDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ApplicationUserTitleInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ApplicationUserTitleUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("ApplicationUserTitle.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("ApplicationUserTitle.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ApplicationUserTitleDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ApplicationUserTitleId  = data.ApplicationUserTitleId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ApplicationUserTitleDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationUserTitleRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("ApplicationUserTitle.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationUserTitleChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
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
