using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.CapitalMarkets;

namespace CapitalMarkets.Components.BusinessLayer
{
	public partial class AccountingCalenderDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static AccountingCalenderDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("AccountingCalender");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(AccountingCalenderDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AccountingCalenderDataModel.DataColumns.AccountingCalenderId:
					if (data.AccountingCalenderId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccountingCalenderDataModel.DataColumns.AccountingCalenderId, data.AccountingCalenderId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountingCalenderDataModel.DataColumns.AccountingCalenderId);
					}
					break;

				case AccountingCalenderDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccountingCalenderDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountingCalenderDataModel.DataColumns.Name);
					}
					break;

				case AccountingCalenderDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccountingCalenderDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountingCalenderDataModel.DataColumns.Description);
					}
					break;

				case AccountingCalenderDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccountingCalenderDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountingCalenderDataModel.DataColumns.SortOrder);
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

		public static List<AccountingCalenderDataModel> GetEntityDetails(AccountingCalenderDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AccountingCalenderSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	AccountingCalenderId           = dataQuery.AccountingCalenderId
				 ,	Name           = dataQuery.Name
			};

			List<AccountingCalenderDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AccountingCalenderDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<AccountingCalenderDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(AccountingCalenderDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(AccountingCalenderDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(AccountingCalenderDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.AccountingCalenderInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AccountingCalenderUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, AccountingCalenderDataModel.DataColumns.AccountingCalenderId); 
			sql = sql + ", " + ToSQLParameter(data, AccountingCalenderDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, AccountingCalenderDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, AccountingCalenderDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(AccountingCalenderDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("AccountingCalender.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(AccountingCalenderDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("AccountingCalender.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AccountingCalenderDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.AccountingCalenderDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   AccountingCalenderId  = data.AccountingCalenderId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(AccountingCalenderDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AccountingCalenderDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
