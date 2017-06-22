using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Library.CommonServices.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.CapitalMarkets;

namespace CapitalMarkets.Components.BusinessLayer
{
	public partial class AccountExcecutiveDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static AccountExcecutiveDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("AccountExcecutive");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(AccountExcecutiveDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AccountExcecutiveDataModel.DataColumns.AccountExcecutiveId:
					if (data.AccountExcecutiveId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccountExcecutiveDataModel.DataColumns.AccountExcecutiveId, data.AccountExcecutiveId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountExcecutiveDataModel.DataColumns.AccountExcecutiveId);
					}
					break;

				case AccountExcecutiveDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccountExcecutiveDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountExcecutiveDataModel.DataColumns.Name);
					}
					break;

				case AccountExcecutiveDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccountExcecutiveDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountExcecutiveDataModel.DataColumns.Description);
					}
					break;

				case AccountExcecutiveDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccountExcecutiveDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountExcecutiveDataModel.DataColumns.SortOrder);
					}
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<AccountExcecutiveDataModel> GetEntityDetails(AccountExcecutiveDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AccountExcecutiveSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	AccountExcecutiveId           = dataQuery.AccountExcecutiveId
				 ,	Name           = dataQuery.Name
			};

			List<AccountExcecutiveDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AccountExcecutiveDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<AccountExcecutiveDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(AccountExcecutiveDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(AccountExcecutiveDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(AccountExcecutiveDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.AccountExcecutiveInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AccountExcecutiveUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, AccountExcecutiveDataModel.DataColumns.AccountExcecutiveId); 
			sql = sql + ", " + ToSQLParameter(data, AccountExcecutiveDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, AccountExcecutiveDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, AccountExcecutiveDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(AccountExcecutiveDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("AccountExcecutive.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(AccountExcecutiveDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("AccountExcecutive.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AccountExcecutiveDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.AccountExcecutiveDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   AccountExcecutiveId  = data.AccountExcecutiveId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(AccountExcecutiveDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AccountExcecutiveDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
