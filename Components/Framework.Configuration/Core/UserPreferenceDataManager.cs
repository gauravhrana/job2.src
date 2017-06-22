using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using System.Reflection;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.UserPreference
{
	public partial class UserPreferenceDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static UserPreferenceDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("UserPreference");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(UserPreferenceDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case UserPreferenceDataModel.DataColumns.UserPreferenceId:
					if (data.UserPreferenceId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserPreferenceDataModel.DataColumns.UserPreferenceId, data.UserPreferenceId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceDataModel.DataColumns.UserPreferenceId);
					}
					break;


				case UserPreferenceDataModel.DataColumns.UserPreferenceKeyId:
					if (data.UserPreferenceKeyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserPreferenceDataModel.DataColumns.UserPreferenceKeyId, data.UserPreferenceKeyId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceDataModel.DataColumns.UserPreferenceKeyId);

					}
					break;

				case UserPreferenceDataModel.DataColumns.Value:
					if (data.Value != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserPreferenceDataModel.DataColumns.Value, data.Value.Replace("'", "''"));

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceDataModel.DataColumns.Value);

					}
					break;

				case UserPreferenceDataModel.DataColumns.DataTypeId:
					if (data.DataTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserPreferenceDataModel.DataColumns.DataTypeId, data.DataTypeId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceDataModel.DataColumns.DataTypeId);

					}
					break;

				case UserPreferenceDataModel.DataColumns.UserPreferenceCategoryId:
					if (data.UserPreferenceCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserPreferenceDataModel.DataColumns.UserPreferenceCategoryId, data.UserPreferenceCategoryId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceDataModel.DataColumns.UserPreferenceCategoryId);

					}
					break;

				case UserPreferenceDataModel.DataColumns.ApplicationUserId:
					if (data.ApplicationUserId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserPreferenceDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceDataModel.DataColumns.ApplicationUserId);

					}
					break;

				case UserPreferenceDataModel.DataColumns.Application:
					if (!string.IsNullOrEmpty(data.Application))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserPreferenceDataModel.DataColumns.Application, data.Application);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceDataModel.DataColumns.Application);

					}
					break;

				case UserPreferenceDataModel.DataColumns.UserPreferenceCategory:
					if (!string.IsNullOrEmpty(data.UserPreferenceCategory))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserPreferenceDataModel.DataColumns.UserPreferenceCategory, data.UserPreferenceCategory);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceDataModel.DataColumns.UserPreferenceCategory);

					}
					break;

				case UserPreferenceDataModel.DataColumns.UserPreferenceDataType:
					if (!string.IsNullOrEmpty(data.UserPreferenceDataType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserPreferenceDataModel.DataColumns.UserPreferenceDataType, data.UserPreferenceDataType);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceDataModel.DataColumns.UserPreferenceDataType);

					}
					break;

				case UserPreferenceDataModel.DataColumns.ApplicationUser:
					if (!string.IsNullOrEmpty(data.ApplicationUser))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserPreferenceDataModel.DataColumns.ApplicationUser, data.ApplicationUser);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceDataModel.DataColumns.ApplicationUser);

					}
					break;

				case UserPreferenceDataModel.DataColumns.UserPreferenceKey:
					if (!string.IsNullOrEmpty(data.UserPreferenceKey))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserPreferenceDataModel.DataColumns.UserPreferenceKey, data.UserPreferenceKey);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceDataModel.DataColumns.UserPreferenceKey);

					}
					break;

				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}
			return returnValue;
		}

		#endregion

		#region GetList

        public static List<UserPreferenceDataModel> GetList(RequestProfile requestProfile)
		{
			try
			{
                return GetEntityDetails(UserPreferenceDataModel.Empty, requestProfile, 0);
			}
			catch (Exception ex)
			{
				Log4Net.LogError(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.ToString(), requestProfile.ApplicationId, ex);
				throw ex;
			}
		}

		#endregion

		#region GetDetails

        public static UserPreferenceDataModel GetDetails(UserPreferenceDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<UserPreferenceDataModel> GetEntityDetails(UserPreferenceDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.UserPreferenceSearch ";

			var parameters =
			new
			{
				    AuditId                     = requestProfile.AuditId
				,   ApplicationId               = dataQuery.ApplicationId
				,   UserPreferenceId            = dataQuery.UserPreferenceId
				,   UserPreferenceKeyId         = dataQuery.UserPreferenceKeyId
				,   UserPreferenceKey           = dataQuery.UserPreferenceKey
				,   UserPreferenceCategoryId    = dataQuery.UserPreferenceCategoryId
				,   DataTypeId                  = dataQuery.DataTypeId
				,   Value                       = dataQuery.Value
				,   ApplicationUserId           = dataQuery.ApplicationUserId
				,   ReturnAuditInfo             = returnAuditInfo
			};

			List<UserPreferenceDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<UserPreferenceDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static int Create(UserPreferenceDataModel data, RequestProfile requestProfile)
		{
			var sql = String.Empty;
			try
			{
				sql = Save(data, requestProfile, "Create");
				var id = DBDML.RunScalarSQL("UserPreference.Insert", sql, DataStoreKey);
				return Convert.ToInt32(id);
			}
			catch (Exception ex)
			{
				Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), requestProfile.ApplicationId, ex);
				throw ex;
			}
		}

		#endregion

		#region Update

		public static void Update(UserPreferenceDataModel data, RequestProfile requestProfile)
		{
			var sql = String.Empty;
			try
			{
				sql = Save(data, requestProfile, "Update");
				DBDML.RunSQL("UserPreference.Update", sql, DataStoreKey);
			}
			catch (Exception ex)
			{
				Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), requestProfile.ApplicationId, ex);
				throw ex;
			}
		}

		public static void UpdateValueOnly(UserPreferenceDataModel data, RequestProfile requestProfile)
		{
			var sql = String.Empty;
			try
			{
				sql = "EXEC ";
				sql += "dbo.UserPreferenceUpdateValueOnly  " +
					" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					", " + ToSQLParameter(data, UserPreferenceDataModel.DataColumns.UserPreferenceId) +
					", " + ToSQLParameter(data, UserPreferenceDataModel.DataColumns.Value);
				DBDML.RunSQL("UserPreference.Update", sql, DataStoreKey);
			}
			catch (Exception ex)
			{
				Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), requestProfile.ApplicationId, ex);
				throw ex;
			}
		}

		#endregion

		#region Delete

		public static void Delete(UserPreferenceDataModel data, RequestProfile requestProfile)
		{
			var sql = String.Empty;
			try
			{
				sql = @"dbo.UserPreferenceDelete ";

				var parameters = new
				{
					AuditId = requestProfile.AuditId
					,
					UserPreferenceId = data.UserPreferenceId
				};

				using (var dataAccess = new DataAccessBase(DataStoreKey))
				{
					dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
				}
			}
			catch (Exception ex)
			{
				Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), requestProfile.ApplicationId, ex);
				throw ex;
			}
		}

		#endregion

		#region Search

		public static DataTable Search(UserPreferenceDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			var table = list.ToDataTable();
			return table;
		}

        public static List<UserPreferenceDataModel> GetEntityList(UserPreferenceDataModel data, RequestProfile requestProfile)
        {
            return GetEntityDetails(data, requestProfile, 0);
        }

		#endregion

		#region Save

		private static string Save(UserPreferenceDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.UserPreferenceInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.UserPreferenceUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, UserPreferenceDataModel.DataColumns.UserPreferenceId) +
						", " + ToSQLParameter(data, UserPreferenceDataModel.DataColumns.UserPreferenceKeyId) +
						", " + ToSQLParameter(data, UserPreferenceDataModel.DataColumns.Value) +
						", " + ToSQLParameter(data, UserPreferenceDataModel.DataColumns.DataTypeId) +
						", " + ToSQLParameter(data, UserPreferenceDataModel.DataColumns.UserPreferenceCategoryId) +
						", " + ToSQLParameter(data, UserPreferenceDataModel.DataColumns.ApplicationUserId);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(UserPreferenceDataModel data, RequestProfile requestProfile)
		{
			var sql = String.Empty;
			try
			{
                var newData = new UserPreferenceDataModel();
                newData.UserPreferenceId = data.UserPreferenceId;
                newData.UserPreferenceKeyId = data.UserPreferenceKeyId;

                var list = GetEntityDetails(newData, requestProfile, 0);
				return list.Count > 0;
			}
			catch (Exception ex)
			{
				Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), requestProfile.ApplicationId, ex);
				throw ex;
			}
		}

		#endregion

		#region Get User Preferences

		public static int? GetUserTimeZone(int applicationUserId, RequestProfile requestProfile)
		{
			var dataKey = new UserPreferenceKeyDataModel();
			dataKey.Name = "UserTimeZone";

			var dtKeys = UserPreferenceKeyDataManager.Search(dataKey, requestProfile);

			if (dtKeys != null && dtKeys.Rows.Count > 0)
			{
				var keyId = Convert.ToInt32(dtKeys.Rows[0][UserPreferenceDataModel.DataColumns.UserPreferenceKeyId]);

				var data = new UserPreferenceDataModel();
				data.ApplicationUserId = applicationUserId;
				data.UserPreferenceKeyId = keyId;

				var dt = Search(data, requestProfile);

				if (dt.Rows.Count > 0)
				{
					return Convert.ToInt32(dt.Rows[0][UserPreferenceDataModel.DataColumns.Value]);
				}
			}

			return null;
		}

		public static Dictionary<string, string> GetUserPreferences(int applicationUserId, RequestProfile requestProfile)
		{
			var objDictionary = new Dictionary<string, string>();
			var sql = String.Empty;
			try
			{
				// formulate SQL
				sql = string.Format("EXEC dbo.UserPreferenceSearch @UserPreferenceId={0}," +
																   "@ApplicationUserId={1}," +
																   "@DataTypeId={2}," +
																   "@UserPreferenceKeyId={3}," +
																   "@ApplicationId={4}," +
																   "@AuditId={5}," +
																   "@UserPreferenceCategoryId={6}",
																   "NULL", applicationUserId, "NULL", "NULL", "NULL", requestProfile.AuditId, "NULL");

				var oDT = new DBDataTable("EXEC dbo.UserPreferenceSearch", sql, DataStoreKey);

				if (oDT.DBTable != null && oDT.DBTable.Rows.Count > 0)
				{
					foreach (DataRow dr in oDT.DBTable.Rows)
					{
						if (!objDictionary.Keys.Contains(Convert.ToString(dr["UserPreferenceKey"])))
						{
							objDictionary.Add(Convert.ToString(dr["UserPreferenceKey"]), Convert.ToString(dr["Value"]));
						}
					}
				}

				return objDictionary;
			}
			catch (Exception ex)
			{
				Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), requestProfile.ApplicationId, ex);
				throw ex;
			}
		}

		public static Dictionary<string, string> GetUserPreferences(int applicationUserId, int userPreferenceCategoryId, RequestProfile requestProfile)
		{
			var objDictionary = new Dictionary<string, string>();
			var sql = String.Empty;
			try
			{
				// formulate SQL
				sql = string.Format("EXEC dbo.UserPreferenceSearch @UserPreferenceId={0}, " +
																"@ApplicationUserId={1}, " +
																"@DataTypeId={2}," +
																"@UserPreferenceKeyId={3}," +
																"@ApplicationId={4}," +
																"@AuditId={5}," +
																"@UserPreferenceCategoryId={6}",
																"NULL", applicationUserId, "NULL", "NULL", "NULL", requestProfile.AuditId, userPreferenceCategoryId);

				var oDT = new DBDataTable("EXEC dbo.UserPreferenceSearch", sql, DataStoreKey);

				if (oDT.DBTable != null && oDT.DBTable.Rows.Count > 0)
				{
					foreach (DataRow dr in oDT.DBTable.Rows)
					{
						if (!objDictionary.Keys.Contains(Convert.ToString(dr["UserPreferenceKey"])))
						{
							objDictionary.Add(Convert.ToString(dr["UserPreferenceKey"]), Convert.ToString(dr["Value"]));
						}
					}
				}

				return objDictionary;
			}
			catch (Exception ex)
			{
				Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), requestProfile.ApplicationId, ex);
				throw ex;
			}
		}

		public static DataTable GetTopNUserPreferences(UserPreferenceDataModel data, int topN, RequestProfile requestProfile)
		{

			var sql = String.Empty;
			try
			{
				// formulate SQL
				sql = "EXEC dbo.UserPreferenceTopNPreference " +
			   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
			   ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
			   ", " + ToSQLParameter(data, UserPreferenceDataModel.DataColumns.Value) +
			   ", " + ToSQLParameter(data, UserPreferenceDataModel.DataColumns.UserPreferenceKeyId) +
			   ", @TopN = " + Convert.ToString(topN);

				var oDT = new DBDataTable("EXEC dbo.UserPreferenceTopNPreference", sql, DataStoreKey);

				if (oDT.DBTable != null && oDT.DBTable.Rows.Count > topN) // check if the total no. of records are greater then TopN
				{
					if (Convert.ToString(oDT.DBTable.Rows[topN + 1]["Value"]) == data.Value) // check the last row's Value. IF it matches the current value passed then remove the 2nd last row.
					{
						oDT.DBTable.Rows.RemoveAt(topN);
					}
					else
					{
						oDT.DBTable.Rows.RemoveAt(topN + 1); //if not then remove the last row.
					}
				}
				return oDT.DBTable;
			}
			catch (Exception ex)
			{
				Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), requestProfile.ApplicationId, ex);
				throw ex;
			}
		}

		#endregion

	}

}
