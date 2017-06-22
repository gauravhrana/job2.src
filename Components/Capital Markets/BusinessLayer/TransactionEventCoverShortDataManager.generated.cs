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
	public partial class TransactionEventCoverShortDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TransactionEventCoverShortDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TransactionEventCoverShort");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TransactionEventCoverShortDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TransactionEventCoverShortDataModel.DataColumns.TransactionEventCoverShortId:
					if (data.TransactionEventCoverShortId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventCoverShortDataModel.DataColumns.TransactionEventCoverShortId, data.TransactionEventCoverShortId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.TransactionEventCoverShortId);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.TransactionEventDate:
					if (data.TransactionEventDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventCoverShortDataModel.DataColumns.TransactionEventDate, data.TransactionEventDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.TransactionEventDate);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.TransactionSettleDate:
					if (data.TransactionSettleDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventCoverShortDataModel.DataColumns.TransactionSettleDate, data.TransactionSettleDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.TransactionSettleDate);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.TransactionTypeId:
					if (data.TransactionTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventCoverShortDataModel.DataColumns.TransactionTypeId, data.TransactionTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.TransactionTypeId);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.TransactionType:
					if (!string.IsNullOrEmpty(data.TransactionType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventCoverShortDataModel.DataColumns.TransactionType, data.TransactionType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.TransactionType);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.CustodianId:
					if (data.CustodianId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventCoverShortDataModel.DataColumns.CustodianId, data.CustodianId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.CustodianId);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.Custodian:
					if (!string.IsNullOrEmpty(data.Custodian))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventCoverShortDataModel.DataColumns.Custodian, data.Custodian);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.Custodian);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.StrategyId:
					if (data.StrategyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventCoverShortDataModel.DataColumns.StrategyId, data.StrategyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.StrategyId);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.Strategy:
					if (!string.IsNullOrEmpty(data.Strategy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventCoverShortDataModel.DataColumns.Strategy, data.Strategy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.Strategy);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.AccountSpecificTypeId:
					if (data.AccountSpecificTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventCoverShortDataModel.DataColumns.AccountSpecificTypeId, data.AccountSpecificTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.AccountSpecificTypeId);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.AccountSpecificType:
					if (!string.IsNullOrEmpty(data.AccountSpecificType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventCoverShortDataModel.DataColumns.AccountSpecificType, data.AccountSpecificType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.AccountSpecificType);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.InvestmentTypeId:
					if (data.InvestmentTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventCoverShortDataModel.DataColumns.InvestmentTypeId, data.InvestmentTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.InvestmentTypeId);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.InvestmentType:
					if (!string.IsNullOrEmpty(data.InvestmentType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventCoverShortDataModel.DataColumns.InvestmentType, data.InvestmentType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventCoverShortDataModel.DataColumns.InvestmentType);
					}
					break;

				case TransactionEventCoverShortDataModel.DataColumns.Quantity:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventCoverShortDataModel.DataColumns.Quantity, data.Quantity);
					break;

				case TransactionEventCoverShortDataModel.DataColumns.Price:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventCoverShortDataModel.DataColumns.Price, data.Price);
					break;

				case TransactionEventCoverShortDataModel.DataColumns.Fees:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventCoverShortDataModel.DataColumns.Fees, data.Fees);
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<TransactionEventCoverShortDataModel> GetEntityDetails(TransactionEventCoverShortDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TransactionEventCoverShortSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TransactionEventCoverShortId           = dataQuery.TransactionEventCoverShortId
				 ,	TransactionTypeId           = dataQuery.TransactionTypeId
				 ,	CustodianId           = dataQuery.CustodianId
				 ,	StrategyId           = dataQuery.StrategyId
				 ,	AccountSpecificTypeId           = dataQuery.AccountSpecificTypeId
				 ,	InvestmentTypeId           = dataQuery.InvestmentTypeId
			};

			List<TransactionEventCoverShortDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TransactionEventCoverShortDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TransactionEventCoverShortDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TransactionEventCoverShortDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TransactionEventCoverShortDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TransactionEventCoverShortDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TransactionEventCoverShortInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TransactionEventCoverShortUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TransactionEventCoverShortDataModel.DataColumns.TransactionEventCoverShortId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventCoverShortDataModel.DataColumns.TransactionEventDate); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventCoverShortDataModel.DataColumns.TransactionSettleDate); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventCoverShortDataModel.DataColumns.TransactionTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventCoverShortDataModel.DataColumns.CustodianId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventCoverShortDataModel.DataColumns.StrategyId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventCoverShortDataModel.DataColumns.AccountSpecificTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventCoverShortDataModel.DataColumns.InvestmentTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventCoverShortDataModel.DataColumns.Quantity); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventCoverShortDataModel.DataColumns.Price); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventCoverShortDataModel.DataColumns.Fees); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TransactionEventCoverShortDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TransactionEventCoverShort.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TransactionEventCoverShortDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TransactionEventCoverShort.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TransactionEventCoverShortDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TransactionEventCoverShortDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TransactionEventCoverShortId  = data.TransactionEventCoverShortId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TransactionEventCoverShortDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TransactionEventCoverShortDataModel();
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
