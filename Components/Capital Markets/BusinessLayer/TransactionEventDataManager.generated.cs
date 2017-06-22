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
	public partial class TransactionEventDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TransactionEventDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TransactionEvent");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TransactionEventDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TransactionEventDataModel.DataColumns.TransactionEventId:
					if (data.TransactionEventId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventDataModel.DataColumns.TransactionEventId, data.TransactionEventId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.TransactionEventId);
					}
					break;

				case TransactionEventDataModel.DataColumns.TransactionEventDate:
					if (data.TransactionEventDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventDataModel.DataColumns.TransactionEventDate, data.TransactionEventDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.TransactionEventDate);
					}
					break;

				case TransactionEventDataModel.DataColumns.TransactionSettleDate:
					if (data.TransactionSettleDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventDataModel.DataColumns.TransactionSettleDate, data.TransactionSettleDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.TransactionSettleDate);
					}
					break;

				case TransactionEventDataModel.DataColumns.TransactionTypeId:
					if (data.TransactionTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventDataModel.DataColumns.TransactionTypeId, data.TransactionTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.TransactionTypeId);
					}
					break;

				case TransactionEventDataModel.DataColumns.TransactionType:
					if (!string.IsNullOrEmpty(data.TransactionType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventDataModel.DataColumns.TransactionType, data.TransactionType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.TransactionType);
					}
					break;

				case TransactionEventDataModel.DataColumns.CustodianId:
					if (data.CustodianId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventDataModel.DataColumns.CustodianId, data.CustodianId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.CustodianId);
					}
					break;

				case TransactionEventDataModel.DataColumns.Custodian:
					if (!string.IsNullOrEmpty(data.Custodian))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventDataModel.DataColumns.Custodian, data.Custodian);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.Custodian);
					}
					break;

				case TransactionEventDataModel.DataColumns.StrategyId:
					if (data.StrategyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventDataModel.DataColumns.StrategyId, data.StrategyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.StrategyId);
					}
					break;

				case TransactionEventDataModel.DataColumns.Strategy:
					if (!string.IsNullOrEmpty(data.Strategy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventDataModel.DataColumns.Strategy, data.Strategy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.Strategy);
					}
					break;

				case TransactionEventDataModel.DataColumns.AccountSpecificTypeId:
					if (data.AccountSpecificTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventDataModel.DataColumns.AccountSpecificTypeId, data.AccountSpecificTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.AccountSpecificTypeId);
					}
					break;

				case TransactionEventDataModel.DataColumns.AccountSpecificType:
					if (!string.IsNullOrEmpty(data.AccountSpecificType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventDataModel.DataColumns.AccountSpecificType, data.AccountSpecificType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.AccountSpecificType);
					}
					break;

				case TransactionEventDataModel.DataColumns.InvestmentTypeId:
					if (data.InvestmentTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventDataModel.DataColumns.InvestmentTypeId, data.InvestmentTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.InvestmentTypeId);
					}
					break;

				case TransactionEventDataModel.DataColumns.InvestmentType:
					if (!string.IsNullOrEmpty(data.InvestmentType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TransactionEventDataModel.DataColumns.InvestmentType, data.InvestmentType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TransactionEventDataModel.DataColumns.InvestmentType);
					}
					break;

				case TransactionEventDataModel.DataColumns.Quantity:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventDataModel.DataColumns.Quantity, data.Quantity);
					break;

				case TransactionEventDataModel.DataColumns.Price:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventDataModel.DataColumns.Price, data.Price);
					break;

				case TransactionEventDataModel.DataColumns.Fees:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TransactionEventDataModel.DataColumns.Fees, data.Fees);
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<TransactionEventDataModel> GetEntityDetails(TransactionEventDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TransactionEventSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TransactionEventId           = dataQuery.TransactionEventId
				 ,	TransactionTypeId           = dataQuery.TransactionTypeId
				 ,	CustodianId           = dataQuery.CustodianId
				 ,	StrategyId           = dataQuery.StrategyId
				 ,	AccountSpecificTypeId           = dataQuery.AccountSpecificTypeId
				 ,	InvestmentTypeId           = dataQuery.InvestmentTypeId
			};

			List<TransactionEventDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TransactionEventDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TransactionEventDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TransactionEventDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TransactionEventDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TransactionEventDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TransactionEventInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TransactionEventUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TransactionEventDataModel.DataColumns.TransactionEventId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventDataModel.DataColumns.TransactionEventDate); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventDataModel.DataColumns.TransactionSettleDate); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventDataModel.DataColumns.TransactionTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventDataModel.DataColumns.CustodianId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventDataModel.DataColumns.StrategyId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventDataModel.DataColumns.AccountSpecificTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventDataModel.DataColumns.InvestmentTypeId); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventDataModel.DataColumns.Quantity); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventDataModel.DataColumns.Price); 
			sql = sql + ", " + ToSQLParameter(data, TransactionEventDataModel.DataColumns.Fees); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TransactionEventDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TransactionEvent.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TransactionEventDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TransactionEvent.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TransactionEventDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TransactionEventDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TransactionEventId  = data.TransactionEventId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TransactionEventDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TransactionEventDataModel();
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
