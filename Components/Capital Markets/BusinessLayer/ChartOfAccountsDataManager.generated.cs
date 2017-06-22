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
	public partial class ChartOfAccountsDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static ChartOfAccountsDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ChartOfAccounts");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ChartOfAccountsDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ChartOfAccountsDataModel.DataColumns.ChartOfAccountsId:
					if (data.ChartOfAccountsId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ChartOfAccountsDataModel.DataColumns.ChartOfAccountsId, data.ChartOfAccountsId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ChartOfAccountsDataModel.DataColumns.ChartOfAccountsId);
					}
					break;

				case ChartOfAccountsDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ChartOfAccountsDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ChartOfAccountsDataModel.DataColumns.Name);
					}
					break;

				case ChartOfAccountsDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ChartOfAccountsDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ChartOfAccountsDataModel.DataColumns.Description);
					}
					break;

				case ChartOfAccountsDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ChartOfAccountsDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ChartOfAccountsDataModel.DataColumns.SortOrder);
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

		public static List<ChartOfAccountsDataModel> GetEntityDetails(ChartOfAccountsDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ChartOfAccountsSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ChartOfAccountsId           = dataQuery.ChartOfAccountsId
				 ,	Name           = dataQuery.Name
			};

			List<ChartOfAccountsDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ChartOfAccountsDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<ChartOfAccountsDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ChartOfAccountsDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(ChartOfAccountsDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(ChartOfAccountsDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.ChartOfAccountsInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ChartOfAccountsUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ChartOfAccountsDataModel.DataColumns.ChartOfAccountsId); 
			sql = sql + ", " + ToSQLParameter(data, ChartOfAccountsDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, ChartOfAccountsDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, ChartOfAccountsDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ChartOfAccountsDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("ChartOfAccounts.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ChartOfAccountsDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("ChartOfAccounts.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ChartOfAccountsDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ChartOfAccountsDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ChartOfAccountsId  = data.ChartOfAccountsId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ChartOfAccountsDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ChartOfAccountsDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
