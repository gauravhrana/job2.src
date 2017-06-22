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
	public partial class TransactionEventSellDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TransactionEventSellDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TransactionEventSell");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TransactionEventSellDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TransactionEventSellDataModel.DataColumns.TransactionEventSellId:
					if (data.TransactionEventSellId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellDataModel.DataColumns.TransactionEventSellId, data.TransactionEventSellId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.TransactionEventSellId);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.TransactionEventDate:
					if (data.TransactionEventDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellDataModel.DataColumns.TransactionEventDate, data.TransactionEventDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.TransactionEventDate);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.TransactionSettleDate:
					if (data.TransactionSettleDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellDataModel.DataColumns.TransactionSettleDate, data.TransactionSettleDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.TransactionSettleDate);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.TransactionTypeId:
					if (data.TransactionTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellDataModel.DataColumns.TransactionTypeId, data.TransactionTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.TransactionTypeId);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.TransactionType:
					if (!string.IsNullOrEmpty(data.TransactionType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellDataModel.DataColumns.TransactionType, data.TransactionType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.TransactionType);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.CustodianId:
					if (data.CustodianId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellDataModel.DataColumns.CustodianId, data.CustodianId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.CustodianId);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.Custodian:
					if (!string.IsNullOrEmpty(data.Custodian))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellDataModel.DataColumns.Custodian, data.Custodian);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.Custodian);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.StrategyId:
					if (data.StrategyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellDataModel.DataColumns.StrategyId, data.StrategyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.StrategyId);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.Strategy:
					if (!string.IsNullOrEmpty(data.Strategy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellDataModel.DataColumns.Strategy, data.Strategy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.Strategy);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.AccountSpecificTypeId:
					if (data.AccountSpecificTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellDataModel.DataColumns.AccountSpecificTypeId, data.AccountSpecificTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.AccountSpecificTypeId);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.AccountSpecificType:
					if (!string.IsNullOrEmpty(data.AccountSpecificType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellDataModel.DataColumns.AccountSpecificType, data.AccountSpecificType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.AccountSpecificType);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.InvestmentTypeId:
					if (data.InvestmentTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellDataModel.DataColumns.InvestmentTypeId, data.InvestmentTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.InvestmentTypeId);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.InvestmentType:
					if (!string.IsNullOrEmpty(data.InvestmentType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellDataModel.DataColumns.InvestmentType, data.InvestmentType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellDataModel.DataColumns.InvestmentType);
					}
					break;

				case TransactionEventSellDataModel.DataColumns.Quantity:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellDataModel.DataColumns.Quantity, data.Quantity);
					break;

				case TransactionEventSellDataModel.DataColumns.Price:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellDataModel.DataColumns.Price, data.Price);
					break;

				case TransactionEventSellDataModel.DataColumns.Fees:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellDataModel.DataColumns.Fees, data.Fees);
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<TransactionEventSellDataModel> GetEntityDetails(TransactionEventSellDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TransactionEventSellSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TransactionEventSellId           = dataQuery.TransactionEventSellId
				 ,	TransactionTypeId           = dataQuery.TransactionTypeId
				 ,	CustodianId           = dataQuery.CustodianId
				 ,	StrategyId           = dataQuery.StrategyId
				 ,	AccountSpecificTypeId           = dataQuery.AccountSpecificTypeId
				 ,	InvestmentTypeId           = dataQuery.InvestmentTypeId
			};

			List<TransactionEventSellDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TransactionEventSellDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TransactionEventSellDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TransactionEventSellDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TransactionEventSellDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TransactionEventSellDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TransactionEventSellInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TransactionEventSellUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellDataModel.DataColumns.TransactionEventSellId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellDataModel.DataColumns.TransactionEventDate); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellDataModel.DataColumns.TransactionSettleDate); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellDataModel.DataColumns.TransactionTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellDataModel.DataColumns.CustodianId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellDataModel.DataColumns.StrategyId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellDataModel.DataColumns.AccountSpecificTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellDataModel.DataColumns.InvestmentTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellDataModel.DataColumns.Quantity); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellDataModel.DataColumns.Price); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellDataModel.DataColumns.Fees); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TransactionEventSellDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TransactionEventSell.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TransactionEventSellDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TransactionEventSell.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TransactionEventSellDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TransactionEventSellDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TransactionEventSellId  = data.TransactionEventSellId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TransactionEventSellDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TransactionEventSellDataModel();
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
