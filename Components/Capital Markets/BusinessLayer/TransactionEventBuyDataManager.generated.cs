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
	public partial class TransactionEventBuyDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TransactionEventBuyDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TransactionEventBuy");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TransactionEventBuyDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TransactionEventBuyDataModel.DataColumns.TransactionEventBuyId:
					if (data.TransactionEventBuyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventBuyDataModel.DataColumns.TransactionEventBuyId, data.TransactionEventBuyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.TransactionEventBuyId);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.TransactionEventDate:
					if (data.TransactionEventDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventBuyDataModel.DataColumns.TransactionEventDate, data.TransactionEventDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.TransactionEventDate);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.TransactionSettleDate:
					if (data.TransactionSettleDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventBuyDataModel.DataColumns.TransactionSettleDate, data.TransactionSettleDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.TransactionSettleDate);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.TransactionTypeId:
					if (data.TransactionTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventBuyDataModel.DataColumns.TransactionTypeId, data.TransactionTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.TransactionTypeId);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.TransactionType:
					if (!string.IsNullOrEmpty(data.TransactionType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventBuyDataModel.DataColumns.TransactionType, data.TransactionType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.TransactionType);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.CustodianId:
					if (data.CustodianId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventBuyDataModel.DataColumns.CustodianId, data.CustodianId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.CustodianId);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.Custodian:
					if (!string.IsNullOrEmpty(data.Custodian))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventBuyDataModel.DataColumns.Custodian, data.Custodian);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.Custodian);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.StrategyId:
					if (data.StrategyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventBuyDataModel.DataColumns.StrategyId, data.StrategyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.StrategyId);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.Strategy:
					if (!string.IsNullOrEmpty(data.Strategy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventBuyDataModel.DataColumns.Strategy, data.Strategy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.Strategy);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.AccountSpecificTypeId:
					if (data.AccountSpecificTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventBuyDataModel.DataColumns.AccountSpecificTypeId, data.AccountSpecificTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.AccountSpecificTypeId);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.AccountSpecificType:
					if (!string.IsNullOrEmpty(data.AccountSpecificType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventBuyDataModel.DataColumns.AccountSpecificType, data.AccountSpecificType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.AccountSpecificType);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.InvestmentTypeId:
					if (data.InvestmentTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventBuyDataModel.DataColumns.InvestmentTypeId, data.InvestmentTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.InvestmentTypeId);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.InvestmentType:
					if (!string.IsNullOrEmpty(data.InvestmentType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventBuyDataModel.DataColumns.InvestmentType, data.InvestmentType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventBuyDataModel.DataColumns.InvestmentType);
					}
					break;

				case TransactionEventBuyDataModel.DataColumns.Quantity:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventBuyDataModel.DataColumns.Quantity, data.Quantity);
					break;

				case TransactionEventBuyDataModel.DataColumns.Price:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventBuyDataModel.DataColumns.Price, data.Price);
					break;

				case TransactionEventBuyDataModel.DataColumns.Fees:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventBuyDataModel.DataColumns.Fees, data.Fees);
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<TransactionEventBuyDataModel> GetEntityDetails(TransactionEventBuyDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TransactionEventBuySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TransactionEventBuyId           = dataQuery.TransactionEventBuyId
				 ,	TransactionTypeId           = dataQuery.TransactionTypeId
				 ,	CustodianId           = dataQuery.CustodianId
				 ,	StrategyId           = dataQuery.StrategyId
				 ,	AccountSpecificTypeId           = dataQuery.AccountSpecificTypeId
				 ,	InvestmentTypeId           = dataQuery.InvestmentTypeId
			};

			List<TransactionEventBuyDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TransactionEventBuyDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TransactionEventBuyDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TransactionEventBuyDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TransactionEventBuyDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TransactionEventBuyDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TransactionEventBuyInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TransactionEventBuyUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TransactionEventBuyDataModel.DataColumns.TransactionEventBuyId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventBuyDataModel.DataColumns.TransactionEventDate); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventBuyDataModel.DataColumns.TransactionSettleDate); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventBuyDataModel.DataColumns.TransactionTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventBuyDataModel.DataColumns.CustodianId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventBuyDataModel.DataColumns.StrategyId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventBuyDataModel.DataColumns.AccountSpecificTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventBuyDataModel.DataColumns.InvestmentTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventBuyDataModel.DataColumns.Quantity); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventBuyDataModel.DataColumns.Price); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventBuyDataModel.DataColumns.Fees); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TransactionEventBuyDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TransactionEventBuy.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TransactionEventBuyDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TransactionEventBuy.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TransactionEventBuyDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TransactionEventBuyDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TransactionEventBuyId  = data.TransactionEventBuyId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TransactionEventBuyDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TransactionEventBuyDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.TransactionTypeId  = data.TransactionTypeId;
			doesExistRequest.CustodianId  = data.CustodianId;
			doesExistRequest.StrategyId  = data.StrategyId;
			doesExistRequest.AccountSpecificTypeId  = data.AccountSpecificTypeId;
			doesExistRequest.InvestmentTypeId  = data.InvestmentTypeId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
