using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.Framework.LogAndTrace;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.LogAndTrace
{
	public partial class UserLoginStatusDataManager : StandardDataManager
	{

		static readonly string DataStoreKey = "";

		static UserLoginStatusDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("UserLoginStatus");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(UserLoginStatusDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case UserLoginStatusDataModel.DataColumns.UserLoginStatusId:
					if (data.UserLoginStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserLoginStatusDataModel.DataColumns.UserLoginStatusId, data.UserLoginStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserLoginStatusDataModel.DataColumns.UserLoginStatusId);
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

        public static List<UserLoginStatusDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(UserLoginStatusDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetDetails

        public static UserLoginStatusDataModel GetDetails(UserLoginStatusDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<UserLoginStatusDataModel> GetUserLoginStatusList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.UserLoginStatusSearch " +
			   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
			   ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, null);

			var result = new List<UserLoginStatusDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new UserLoginStatusDataModel();

					dataItem.UserLoginStatusId = (int)dbReader[UserLoginStatusDataModel.DataColumns.UserLoginStatusId];
					dataItem.UserLoginStatusCode = dbReader[UserLoginStatusDataModel.DataColumns.UserLoginStatusCode].ToString();
					
					result.Add(dataItem);
				}
			}

			return result;
		}

		public static List<UserLoginStatusDataModel> GetEntityDetails(UserLoginStatusDataModel dataQuery, RequestProfile requestProfile, int applicationModeId = 0)
		{
			const string sql = @"dbo.UserLoginStatusSearch ";

			var parameters =
			new
			{
				    AuditId             = requestProfile.AuditId
				,   UserLoginStatusId   = dataQuery.UserLoginStatusId
				,	Name				= dataQuery.Name
				,   ReturnAuditInfo     = ReturnAuditInfoOnDetails
			};

			List<UserLoginStatusDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<UserLoginStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}
		
		#endregion

		#region Create

		public static void Create(UserLoginStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("UserLoginStatus.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(UserLoginStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("UserLoginStatus.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(UserLoginStatusDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.UserLoginStatusDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				UserLoginStatusId = data.UserLoginStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(UserLoginStatusDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;

		}

		#endregion

		#region Save

		private static string Save(UserLoginStatusDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.UserLoginStatusInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.UserLoginStatusUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, UserLoginStatusDataModel.DataColumns.UserLoginStatusId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(UserLoginStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.UserLoginStatusSearch " +
			" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
			", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
			", " + ToSQLParameter(data, UserLoginStatusDataModel.DataColumns.Name);

			var oDT = new DBDataTable("UserLoginStatus.DoesExist", sql, DataStoreKey);
			return oDT.DBTable.Rows.Count > 0;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(UserLoginStatusDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.UserLoginStatusChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, UserLoginStatusDataModel.DataColumns.UserLoginStatusId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(UserLoginStatusDataModel data, RequestProfile requestProfile)
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

		public static void Sync(int newApplicationId, int oldApplicationId, RequestProfile requestProfile)
		{
			// get all records for old Application Id
			var sql = "EXEC dbo.UserLoginStatusSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, oldApplicationId);

			var oDT = new DBDataTable("UserLoginStatus.Search", sql, DataStoreKey);

			// formaulate a new request Profile which will have new Applicationid
			var newRequestProfile = new RequestProfile();
			newRequestProfile.ApplicationId = newApplicationId;
			newRequestProfile.AuditId = requestProfile.AuditId;

			foreach (DataRow dr in oDT.DBTable.Rows)
			{
				var data = new UserLoginStatusDataModel();
				data.ApplicationId = newApplicationId;
				data.Name = dr[UserLoginStatusDataModel.DataColumns.Name].ToString();

				// check for existing record in new Application Id
				if(!DoesExist(data, newRequestProfile))
				{
					data.Description = dr[UserLoginStatusDataModel.DataColumns.Description].ToString();
					data.SortOrder = Convert.ToInt32(dr[UserLoginStatusDataModel.DataColumns.SortOrder]);

					//create in new application id
					Create(data, newRequestProfile);

				}

			}
		}

	}
}
