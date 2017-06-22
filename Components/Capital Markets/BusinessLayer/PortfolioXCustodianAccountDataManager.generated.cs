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
	public partial class PortfolioXCustodianAccountDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static PortfolioXCustodianAccountDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("PortfolioXCustodianAccount");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(PortfolioXCustodianAccountDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case PortfolioXCustodianAccountDataModel.DataColumns.PortfolioXCustodianAccountId:
					if (data.PortfolioXCustodianAccountId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PortfolioXCustodianAccountDataModel.DataColumns.PortfolioXCustodianAccountId, data.PortfolioXCustodianAccountId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PortfolioXCustodianAccountDataModel.DataColumns.PortfolioXCustodianAccountId);
					}
					break;

				case PortfolioXCustodianAccountDataModel.DataColumns.CustodianAccountId:
					if (data.CustodianAccountId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PortfolioXCustodianAccountDataModel.DataColumns.CustodianAccountId, data.CustodianAccountId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PortfolioXCustodianAccountDataModel.DataColumns.CustodianAccountId);
					}
					break;

				case PortfolioXCustodianAccountDataModel.DataColumns.PortfolioId:
					if (data.PortfolioId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, PortfolioXCustodianAccountDataModel.DataColumns.PortfolioId, data.PortfolioId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PortfolioXCustodianAccountDataModel.DataColumns.PortfolioId);
					}
					break;

				case PortfolioXCustodianAccountDataModel.DataColumns.CustodianAccount:
					if (!string.IsNullOrEmpty(data.CustodianAccount))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PortfolioXCustodianAccountDataModel.DataColumns.CustodianAccount, data.CustodianAccount);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PortfolioXCustodianAccountDataModel.DataColumns.CustodianAccount);
					}
					break;

				case PortfolioXCustodianAccountDataModel.DataColumns.Portfolio:
					if (!string.IsNullOrEmpty(data.Portfolio))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, PortfolioXCustodianAccountDataModel.DataColumns.Portfolio, data.Portfolio);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, PortfolioXCustodianAccountDataModel.DataColumns.Portfolio);
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

		public static List<PortfolioXCustodianAccountDataModel> GetEntityDetails(PortfolioXCustodianAccountDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.PortfolioXCustodianAccountSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	PortfolioXCustodianAccountId           = dataQuery.PortfolioXCustodianAccountId
				 ,	CustodianAccountId           = dataQuery.CustodianAccountId
				 ,	PortfolioId           = dataQuery.PortfolioId
			};

			List<PortfolioXCustodianAccountDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<PortfolioXCustodianAccountDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<PortfolioXCustodianAccountDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(PortfolioXCustodianAccountDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(PortfolioXCustodianAccountDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(PortfolioXCustodianAccountDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.PortfolioXCustodianAccountInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.PortfolioXCustodianAccountUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, PortfolioXCustodianAccountDataModel.DataColumns.PortfolioXCustodianAccountId); 
			sql = sql + ", " + ToSQLParameter(data, PortfolioXCustodianAccountDataModel.DataColumns.CustodianAccountId); 
			sql = sql + ", " + ToSQLParameter(data, PortfolioXCustodianAccountDataModel.DataColumns.PortfolioId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(PortfolioXCustodianAccountDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("PortfolioXCustodianAccount.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(PortfolioXCustodianAccountDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("PortfolioXCustodianAccount.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(PortfolioXCustodianAccountDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.PortfolioXCustodianAccountDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				 ,	PortfolioXCustodianAccountId           = data.PortfolioXCustodianAccountId
				 ,	CustodianAccountId           = data.CustodianAccountId
				 ,	PortfolioId           = data.PortfolioId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(PortfolioXCustodianAccountDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new PortfolioXCustodianAccountDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
