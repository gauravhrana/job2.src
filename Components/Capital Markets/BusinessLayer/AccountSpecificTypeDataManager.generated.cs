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
	public partial class AccountSpecificTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static AccountSpecificTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("AccountSpecificType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(AccountSpecificTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case AccountSpecificTypeDataModel.DataColumns.AccountSpecificTypeId:
					if (data.AccountSpecificTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccountSpecificTypeDataModel.DataColumns.AccountSpecificTypeId, data.AccountSpecificTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountSpecificTypeDataModel.DataColumns.AccountSpecificTypeId);
					}
					break;

				case AccountSpecificTypeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccountSpecificTypeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountSpecificTypeDataModel.DataColumns.Name);
					}
					break;

				case AccountSpecificTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, AccountSpecificTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountSpecificTypeDataModel.DataColumns.Description);
					}
					break;

				case AccountSpecificTypeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, AccountSpecificTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, AccountSpecificTypeDataModel.DataColumns.SortOrder);
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

		public static List<AccountSpecificTypeDataModel> GetEntityDetails(AccountSpecificTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.AccountSpecificTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	AccountSpecificTypeId           = dataQuery.AccountSpecificTypeId
				 ,	Name           = dataQuery.Name
			};

			List<AccountSpecificTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<AccountSpecificTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<AccountSpecificTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(AccountSpecificTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(AccountSpecificTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(AccountSpecificTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.AccountSpecificTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.AccountSpecificTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, AccountSpecificTypeDataModel.DataColumns.AccountSpecificTypeId); 
			sql = sql + ", " + ToSQLParameter(data, AccountSpecificTypeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, AccountSpecificTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, AccountSpecificTypeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(AccountSpecificTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("AccountSpecificType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(AccountSpecificTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("AccountSpecificType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(AccountSpecificTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.AccountSpecificTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   AccountSpecificTypeId  = data.AccountSpecificTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(AccountSpecificTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new AccountSpecificTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
