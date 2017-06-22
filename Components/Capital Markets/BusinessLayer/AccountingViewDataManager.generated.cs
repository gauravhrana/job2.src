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
	public partial class AccountingViewDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static AccountingViewDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("AccountingView");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(AccountingViewDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AccountingViewDataModel.DataColumns.AccountingViewId:
					if (data.AccountingViewId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccountingViewDataModel.DataColumns.AccountingViewId, data.AccountingViewId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountingViewDataModel.DataColumns.AccountingViewId);
					}
					break;

				case AccountingViewDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccountingViewDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountingViewDataModel.DataColumns.Name);
					}
					break;

				case AccountingViewDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccountingViewDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountingViewDataModel.DataColumns.Description);
					}
					break;

				case AccountingViewDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccountingViewDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountingViewDataModel.DataColumns.SortOrder);
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

		public static List<AccountingViewDataModel> GetEntityDetails(AccountingViewDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AccountingViewSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	AccountingViewId           = dataQuery.AccountingViewId
				 ,	Name           = dataQuery.Name
			};

			List<AccountingViewDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AccountingViewDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<AccountingViewDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(AccountingViewDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(AccountingViewDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(AccountingViewDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.AccountingViewInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AccountingViewUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, AccountingViewDataModel.DataColumns.AccountingViewId); 
			sql = sql + ", " + ToSQLParameter(data, AccountingViewDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, AccountingViewDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, AccountingViewDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(AccountingViewDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("AccountingView.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(AccountingViewDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("AccountingView.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AccountingViewDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.AccountingViewDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   AccountingViewId  = data.AccountingViewId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(AccountingViewDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AccountingViewDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
