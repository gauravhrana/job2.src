using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using System.Reflection;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.UserPreference
{
	public partial class UserPreferenceSelectedItemDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static UserPreferenceSelectedItemDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("UserPreferenceSelectedItem");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(UserPreferenceSelectedItemDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceSelectedItemId:
					if (data.UserPreferenceSelectedItemId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceSelectedItemId, data.UserPreferenceSelectedItemId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceSelectedItemId);
					}
					break;


				case UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceKeyId:
					if (data.UserPreferenceKeyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceKeyId, data.UserPreferenceKeyId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceKeyId);

					}
					break;

				case UserPreferenceSelectedItemDataModel.DataColumns.Value:
					if (data.Value != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserPreferenceSelectedItemDataModel.DataColumns.Value, data.Value);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceSelectedItemDataModel.DataColumns.Value);

					}
					break;

				case UserPreferenceSelectedItemDataModel.DataColumns.ParentKey:
					if (data.ParentKey != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserPreferenceSelectedItemDataModel.DataColumns.ParentKey, data.ParentKey);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceSelectedItemDataModel.DataColumns.ParentKey);

					}
					break;

				case UserPreferenceSelectedItemDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserPreferenceSelectedItemDataModel.DataColumns.SortOrder, data.SortOrder);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceSelectedItemDataModel.DataColumns.SortOrder);

					}
					break;

				case UserPreferenceSelectedItemDataModel.DataColumns.ApplicationUserId:
					if (data.ApplicationUserId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UserPreferenceSelectedItemDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceSelectedItemDataModel.DataColumns.ApplicationUserId);

					}
					break;

				case UserPreferenceSelectedItemDataModel.DataColumns.ApplicationUser:
					if (!string.IsNullOrEmpty(data.Value))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserPreferenceSelectedItemDataModel.DataColumns.ApplicationUser, data.ApplicationUser);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceSelectedItemDataModel.DataColumns.ApplicationUser);

					}
					break;

				case UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceKey:
					if (!string.IsNullOrEmpty(data.UserPreferenceKey))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceKey, data.UserPreferenceKey);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceKey);

					}
					break;

			}
			return returnValue;
		}

		#endregion

		#region GetList

        public static List<UserPreferenceSelectedItemDataModel> GetList(RequestProfile requestProfile)
		{
			try
			{
                return GetEntityDetails(UserPreferenceSelectedItemDataModel.Empty, requestProfile, 0);
			}
			catch (Exception ex)
			{
				Log4Net.LogError(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.ToString(), requestProfile.ApplicationId, ex);
				throw ex;
			}
		}

		#endregion

		#region GetDetails

        public static UserPreferenceSelectedItemDataModel GetDetails(UserPreferenceSelectedItemDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static List<UserPreferenceSelectedItemDataModel> GetEntityDetails(UserPreferenceSelectedItemDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.UserPreferenceSelectedItemSearch ";

			var parameters =
			new
			{
				    AuditId                         = requestProfile.AuditId
				,   ApplicationId                   = requestProfile.ApplicationId
				,   UserPreferenceSelectedItemId    = dataQuery.UserPreferenceSelectedItemId				
				,   ReturnAuditInfo                 = returnAuditInfo
				,   UserPreferenceKeyId             = dataQuery.UserPreferenceKeyId
				,   ParentKey                       = dataQuery.ParentKey
				,   ApplicationUserId               = dataQuery.ApplicationUserId
			};

			List<UserPreferenceSelectedItemDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<UserPreferenceSelectedItemDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}


		#endregion

		#region Create

		public static void Create(UserPreferenceSelectedItemDataModel data, RequestProfile requestProfile)
		{
			var sql = String.Empty;
			try
			{
				sql = Save(data, requestProfile, "Create");
				DBDML.RunSQL("UserPreferenceSelectedItem.Insert", sql, DataStoreKey);
			}
			catch (Exception ex)
			{
				Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), requestProfile.ApplicationId, ex);
				throw ex;
			}
		}

		#endregion

		#region Update

		public static void Update(UserPreferenceSelectedItemDataModel data, RequestProfile requestProfile)
		{
			var sql = String.Empty;
			try
			{
				sql = Save(data, requestProfile, "Update");
				DBDML.RunSQL("UserPreferenceSelectedItem.Update", sql, DataStoreKey);
			}
			catch (Exception ex)
			{
				Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), requestProfile.ApplicationId, ex);
				throw ex;
			}
		}

		#endregion

		#region Delete

		public static void Delete(UserPreferenceSelectedItemDataModel data, RequestProfile requestProfile)
		{
			var sql = String.Empty;
			try
			{
				sql = @"dbo.UserPreferenceSelectedItemDelete ";

				var parameters = new
				{
					AuditId = requestProfile.AuditId
					,
					UserPreferenceSelectedItemId = data.UserPreferenceSelectedItemId
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

		public static DataTable Search(UserPreferenceSelectedItemDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(UserPreferenceSelectedItemDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.UserPreferenceSelectedItemInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.UserPreferenceSelectedItemUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceSelectedItemId) +
						", " + ToSQLParameter(data, UserPreferenceSelectedItemDataModel.DataColumns.UserPreferenceKeyId) +
						", " + ToSQLParameter(data, UserPreferenceSelectedItemDataModel.DataColumns.Value) +
						", " + ToSQLParameter(data, UserPreferenceSelectedItemDataModel.DataColumns.ParentKey) +
						", " + ToSQLParameter(data, UserPreferenceSelectedItemDataModel.DataColumns.SortOrder) +
						", " + ToSQLParameter(data, UserPreferenceSelectedItemDataModel.DataColumns.ApplicationUserId);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(UserPreferenceSelectedItemDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new UserPreferenceSelectedItemDataModel();
			doesExistRequest.UserPreferenceKeyId = data.UserPreferenceKeyId;
			doesExistRequest.ParentKey = data.ParentKey;
			doesExistRequest.Value = data.Value;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

	}

}


