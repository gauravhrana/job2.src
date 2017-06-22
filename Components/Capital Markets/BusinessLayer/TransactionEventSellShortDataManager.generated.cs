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
	public partial class TransactionEventSellShortDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TransactionEventSellShortDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TransactionEventSellShort");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TransactionEventSellShortDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TransactionEventSellShortDataModel.DataColumns.TransactionEventSellShortId:
					if (data.TransactionEventSellShortId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellShortDataModel.DataColumns.TransactionEventSellShortId, data.TransactionEventSellShortId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.TransactionEventSellShortId);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.TransactionEventDate:
					if (data.TransactionEventDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellShortDataModel.DataColumns.TransactionEventDate, data.TransactionEventDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.TransactionEventDate);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.TransactionSettleDate:
					if (data.TransactionSettleDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellShortDataModel.DataColumns.TransactionSettleDate, data.TransactionSettleDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.TransactionSettleDate);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.TransactionTypeId:
					if (data.TransactionTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellShortDataModel.DataColumns.TransactionTypeId, data.TransactionTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.TransactionTypeId);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.TransactionType:
					if (!string.IsNullOrEmpty(data.TransactionType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellShortDataModel.DataColumns.TransactionType, data.TransactionType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.TransactionType);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.CustodianId:
					if (data.CustodianId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellShortDataModel.DataColumns.CustodianId, data.CustodianId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.CustodianId);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.Custodian:
					if (!string.IsNullOrEmpty(data.Custodian))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellShortDataModel.DataColumns.Custodian, data.Custodian);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.Custodian);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.StrategyId:
					if (data.StrategyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellShortDataModel.DataColumns.StrategyId, data.StrategyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.StrategyId);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.Strategy:
					if (!string.IsNullOrEmpty(data.Strategy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellShortDataModel.DataColumns.Strategy, data.Strategy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.Strategy);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.AccountSpecificTypeId:
					if (data.AccountSpecificTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellShortDataModel.DataColumns.AccountSpecificTypeId, data.AccountSpecificTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.AccountSpecificTypeId);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.AccountSpecificType:
					if (!string.IsNullOrEmpty(data.AccountSpecificType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellShortDataModel.DataColumns.AccountSpecificType, data.AccountSpecificType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.AccountSpecificType);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.InvestmentTypeId:
					if (data.InvestmentTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellShortDataModel.DataColumns.InvestmentTypeId, data.InvestmentTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.InvestmentTypeId);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.InvestmentType:
					if (!string.IsNullOrEmpty(data.InvestmentType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventSellShortDataModel.DataColumns.InvestmentType, data.InvestmentType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventSellShortDataModel.DataColumns.InvestmentType);
					}
					break;

				case TransactionEventSellShortDataModel.DataColumns.Quantity:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellShortDataModel.DataColumns.Quantity, data.Quantity);
					break;

				case TransactionEventSellShortDataModel.DataColumns.Price:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellShortDataModel.DataColumns.Price, data.Price);
					break;

				case TransactionEventSellShortDataModel.DataColumns.Fees:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventSellShortDataModel.DataColumns.Fees, data.Fees);
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<TransactionEventSellShortDataModel> GetEntityDetails(TransactionEventSellShortDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TransactionEventSellShortSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TransactionEventSellShortId           = dataQuery.TransactionEventSellShortId
				 ,	TransactionTypeId           = dataQuery.TransactionTypeId
				 ,	CustodianId           = dataQuery.CustodianId
				 ,	StrategyId           = dataQuery.StrategyId
				 ,	AccountSpecificTypeId           = dataQuery.AccountSpecificTypeId
				 ,	InvestmentTypeId           = dataQuery.InvestmentTypeId
			};

			List<TransactionEventSellShortDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TransactionEventSellShortDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TransactionEventSellShortDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TransactionEventSellShortDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TransactionEventSellShortDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TransactionEventSellShortDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TransactionEventSellShortInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TransactionEventSellShortUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellShortDataModel.DataColumns.TransactionEventSellShortId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellShortDataModel.DataColumns.TransactionEventDate); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellShortDataModel.DataColumns.TransactionSettleDate); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellShortDataModel.DataColumns.TransactionTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellShortDataModel.DataColumns.CustodianId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellShortDataModel.DataColumns.StrategyId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellShortDataModel.DataColumns.AccountSpecificTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellShortDataModel.DataColumns.InvestmentTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellShortDataModel.DataColumns.Quantity); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellShortDataModel.DataColumns.Price); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventSellShortDataModel.DataColumns.Fees); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TransactionEventSellShortDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TransactionEventSellShort.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TransactionEventSellShortDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TransactionEventSellShort.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TransactionEventSellShortDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TransactionEventSellShortDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TransactionEventSellShortId  = data.TransactionEventSellShortId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TransactionEventSellShortDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TransactionEventSellShortDataModel();
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
