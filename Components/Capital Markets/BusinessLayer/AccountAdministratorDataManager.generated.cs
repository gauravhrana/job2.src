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
	public partial class AccountAdministratorDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static AccountAdministratorDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("AccountAdministrator");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(AccountAdministratorDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AccountAdministratorDataModel.DataColumns.AccountAdministratorId:
					if (data.AccountAdministratorId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccountAdministratorDataModel.DataColumns.AccountAdministratorId, data.AccountAdministratorId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountAdministratorDataModel.DataColumns.AccountAdministratorId);
					}
					break;

				case AccountAdministratorDataModel.DataColumns.Url:
					if (!string.IsNullOrEmpty(data.Url))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccountAdministratorDataModel.DataColumns.Url, data.Url);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountAdministratorDataModel.DataColumns.Url);
					}
					break;

				case AccountAdministratorDataModel.DataColumns.Code:
					if (!string.IsNullOrEmpty(data.Code))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccountAdministratorDataModel.DataColumns.Code, data.Code);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountAdministratorDataModel.DataColumns.Code);
					}
					break;

				case AccountAdministratorDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccountAdministratorDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountAdministratorDataModel.DataColumns.Name);
					}
					break;

				case AccountAdministratorDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccountAdministratorDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountAdministratorDataModel.DataColumns.Description);
					}
					break;

				case AccountAdministratorDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccountAdministratorDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountAdministratorDataModel.DataColumns.SortOrder);
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

		public static List<AccountAdministratorDataModel> GetEntityDetails(AccountAdministratorDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AccountAdministratorSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	AccountAdministratorId           = dataQuery.AccountAdministratorId
				 ,	Name           = dataQuery.Name
			};

			List<AccountAdministratorDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AccountAdministratorDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<AccountAdministratorDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(AccountAdministratorDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(AccountAdministratorDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static void FormatData(AccountAdministratorDataModel data)
		{
			data.Code = Formatter.FormatCode(data.Code);
		}

		public static string Save(AccountAdministratorDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			FormatData(data);

			switch (action)
			{
				case "Create":
					sql += "dbo.AccountAdministratorInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AccountAdministratorUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, AccountAdministratorDataModel.DataColumns.AccountAdministratorId); 
			sql = sql + ", " + ToSQLParameter(data, AccountAdministratorDataModel.DataColumns.Url); 
			sql = sql + ", " + ToSQLParameter(data, AccountAdministratorDataModel.DataColumns.Code); 
			sql = sql + ", " + ToSQLParameter(data, AccountAdministratorDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, AccountAdministratorDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, AccountAdministratorDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(AccountAdministratorDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("AccountAdministrator.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(AccountAdministratorDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("AccountAdministrator.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AccountAdministratorDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.AccountAdministratorDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   AccountAdministratorId  = data.AccountAdministratorId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(AccountAdministratorDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AccountAdministratorDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
