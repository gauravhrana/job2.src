using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.UserPreference
{
	public partial class UserPreferenceKeyDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static UserPreferenceKeyDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("UserPreferenceKey");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(UserPreferenceKeyDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId:
					if (data.UserPreferenceKeyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId, data.UserPreferenceKeyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId);
					}
					break;

				case UserPreferenceKeyDataModel.DataColumns.Value:
					if (!string.IsNullOrEmpty(data.Value))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserPreferenceKeyDataModel.DataColumns.Value, data.Value);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceKeyDataModel.DataColumns.Value);
					}
					break;

				case UserPreferenceKeyDataModel.DataColumns.DataTypeId:
					if (data.DataTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserPreferenceKeyDataModel.DataColumns.DataTypeId, data.DataTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceKeyDataModel.DataColumns.DataTypeId);
					}
					break;

				case UserPreferenceKeyDataModel.DataColumns.DataType:
					if (!string.IsNullOrEmpty(data.DataType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserPreferenceKeyDataModel.DataColumns.DataType, data.DataType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceKeyDataModel.DataColumns.DataType);
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

        public static List<UserPreferenceKeyDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(UserPreferenceKeyDataModel.Empty, requestProfile, 0);
		}

		public static List<UserPreferenceKeyDataModel> GetUserPreferenceKeyList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.UserPreferenceKeySearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var result = new List<UserPreferenceKeyDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new UserPreferenceKeyDataModel();

					dataItem.UserPreferenceKeyId = (int)dbReader[UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId];
					dataItem.Name = dbReader[StandardDataModel.StandardDataColumns.Name].ToString();
					dataItem.Description = dbReader[StandardDataModel.StandardDataColumns.Description].ToString();
					dataItem.SortOrder = (int)dbReader[StandardDataModel.StandardDataColumns.SortOrder];

					result.Add(dataItem);
				}
			}

			return result;
		}

		#endregion

		#region GetDetails

        public static UserPreferenceKeyDataModel GetDetails(UserPreferenceKeyDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static DataTable GetDetailsByApplication(UserPreferenceKeyDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.UserPreferenceKeySearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ApplicationId = dataQuery.ApplicationId
				,
				UserPreferenceKeyId = dataQuery.UserPreferenceKeyId
				,
				Name = dataQuery.Name
				,
				ReturnAuditInfo = returnAuditInfo
				,
				DataTypeId = dataQuery.DataTypeId
			};

			List<UserPreferenceKeyDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<UserPreferenceKeyDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			var table = result.ToDataTable();

			return table;
		}

		public static List<UserPreferenceKeyDataModel> GetEntityDetails(UserPreferenceKeyDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.UserPreferenceKeySearch ";

			var parameters =
			new
			{
				    AuditId             = requestProfile.AuditId
				,   ApplicationId       = requestProfile.ApplicationId
				,   UserPreferenceKeyId = dataQuery.UserPreferenceKeyId
				,   Name                = dataQuery.Name
				,   ReturnAuditInfo     = returnAuditInfo
				,   DataTypeId          = dataQuery.DataTypeId
			};

			List<UserPreferenceKeyDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<UserPreferenceKeyDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(UserPreferenceKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("UserPreferenceKey.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(UserPreferenceKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("UserPreferenceKey.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(UserPreferenceKeyDataModel data, RequestProfile requestProfile)
		{
			const string sql = @"dbo.UserPreferenceKeyDelete ";

			var parameters = new
			{
				AuditId = requestProfile.AuditId
				,
				UserPreferenceKeyId = data.UserPreferenceKeyId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(UserPreferenceKeyDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(UserPreferenceKeyDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.UserPreferenceKeyInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.UserPreferenceKeyUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, UserPreferenceKeyDataModel.DataColumns.Value) +
						", " + ToSQLParameter(data, UserPreferenceKeyDataModel.DataColumns.DataTypeId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(UserPreferenceKeyDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new UserPreferenceKeyDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(UserPreferenceKeyDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.UserPreferenceKeyChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(UserPreferenceKeyDataModel data, RequestProfile requestProfile)
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
			var sql = "EXEC dbo.UserPreferenceKeySearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, oldApplicationId);            

			var oDT = new DBDataTable("UserPreferenceKey.Search", sql, DataStoreKey);
            

			// formaulate a new request Profile which will have new Applicationid
			var newRequestProfile = new RequestProfile();
			newRequestProfile.ApplicationId = newApplicationId;
			newRequestProfile.AuditId = requestProfile.AuditId;

            var dataTypedata = new UserPreferenceDataTypeDataModel();
            var dataTypeList = UserPreferenceDataTypeDataManager.GetEntityDetails(dataTypedata, newRequestProfile);

			foreach (DataRow dr in oDT.DBTable.Rows)
			{
				var data = new UserPreferenceKeyDataModel();
				data.ApplicationId = newApplicationId;
				data.Name = dr[StandardDataModel.StandardDataColumns.Name].ToString();

				// check for existing record in new Application Id
				if (!DoesExist(data, newRequestProfile))
				{

                    var dataType = dr[UserPreferenceKeyDataModel.DataColumns.DataType].ToString();
                    
                    //get new fc mode id based on fc mode name
                    var newDataTypeId = dataTypeList.Find(x => x.Name == dataType).UserPreferenceDataTypeId;

					data.Description  = dr[StandardDataModel.StandardDataColumns.Description].ToString();
					data.Value        = dr[UserPreferenceKeyDataModel.DataColumns.Value].ToString();
					if (string.IsNullOrEmpty(data.Value))
					{
						data.Value    = " ";
					}
					data.DataTypeId   = newDataTypeId;
					data.SortOrder    = Convert.ToInt32(dr[StandardDataModel.StandardDataColumns.SortOrder]);

					//create in new application id
					Create(data, newRequestProfile);

				}

			}
		}
	}
}

