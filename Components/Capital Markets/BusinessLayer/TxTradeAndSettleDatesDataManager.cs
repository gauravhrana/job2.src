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
	public partial class TxTradeAndSettleDatesDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TxTradeAndSettleDatesDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TxTradeAndSettleDates");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TxTradeAndSettleDatesDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TxTradeAndSettleDatesDataModel.DataColumns.TxTradeAndSettleDatesId:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeAndSettleDatesDataModel.DataColumns.TxTradeAndSettleDatesId, data.TxTradeAndSettleDatesId);
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.TransactionEventId:
					if (data.TransactionEventId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeAndSettleDatesDataModel.DataColumns.TransactionEventId, data.TransactionEventId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.TransactionEventId);
					}
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.TransactionEvent:
					if (!string.IsNullOrEmpty(data.TransactionEvent))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeAndSettleDatesDataModel.DataColumns.TransactionEvent, data.TransactionEvent);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.TransactionEvent);
					}
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.TradeDate:
					if (data.TradeDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeAndSettleDatesDataModel.DataColumns.TradeDate, data.TradeDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.TradeDate);
					}
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.FromSearchTradeDate:
					if (data.FromSearchTradeDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeAndSettleDatesDataModel.DataColumns.TradeDate, data.FromSearchTradeDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.FromSearchTradeDate);
					}
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.ToSearchTradeDate:
					if (data.ToSearchTradeDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeAndSettleDatesDataModel.DataColumns.TradeDate, data.ToSearchTradeDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.ToSearchTradeDate);
					}
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.ContractualDate:
					if (data.ContractualDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeAndSettleDatesDataModel.DataColumns.ContractualDate, data.ContractualDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.ContractualDate);
					}
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.FromSearchContractualDate:
					if (data.FromSearchContractualDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeAndSettleDatesDataModel.DataColumns.ContractualDate, data.FromSearchContractualDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.FromSearchContractualDate);
					}
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.ToSearchContractualDate:
					if (data.ToSearchContractualDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeAndSettleDatesDataModel.DataColumns.ContractualDate, data.ToSearchContractualDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.ToSearchContractualDate);
					}
					break;


				case TxTradeAndSettleDatesDataModel.DataColumns.ActualDate:
					if (data.ActualDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeAndSettleDatesDataModel.DataColumns.ActualDate, data.ActualDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.ActualDate);
					}
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.FromSearchActualDate:
					if (data.FromSearchActualDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeAndSettleDatesDataModel.DataColumns.ActualDate, data.FromSearchActualDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.FromSearchContractualDate);
					}
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.ToSearchActualDate:
					if (data.ToSearchActualDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeAndSettleDatesDataModel.DataColumns.ActualDate, data.ToSearchActualDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.ToSearchActualDate);
					}
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.SpotDate:
					if (data.SpotDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeAndSettleDatesDataModel.DataColumns.SpotDate, data.SpotDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.SpotDate);
					}
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.SettlementDate:
					if (data.SettlementDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxTradeAndSettleDatesDataModel.DataColumns.SettlementDate, data.SettlementDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxTradeAndSettleDatesDataModel.DataColumns.SettlementDate);
					}
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.CreatedDate:
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.CreatedByAuditId:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeAndSettleDatesDataModel.DataColumns.CreatedByAuditId, data.CreatedByAuditId);
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.UpdatedDate:
					break;

				case TxTradeAndSettleDatesDataModel.DataColumns.ModifiedByAuditId:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxTradeAndSettleDatesDataModel.DataColumns.ModifiedByAuditId, data.ModifiedByAuditId);
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<TxTradeAndSettleDatesDataModel> GetEntityDetails(TxTradeAndSettleDatesDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TxTradeAndSettleDatesSearch ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				 ,	ApplicationId = requestProfile.ApplicationId
				 ,	ReturnAuditInfo = returnAuditInfo
				 ,	TxTradeAndSettleDatesId = dataQuery.TxTradeAndSettleDatesId
				 ,	TransactionEventId = dataQuery.TransactionEventId
				 ,	FromSearchTradeDate = dataQuery.FromSearchTradeDate
				 ,	ToSearchTradeDate = dataQuery.ToSearchTradeDate
				 ,  FromSearchActualDate = dataQuery.FromSearchActualDate
				 ,  ToSearchActualDate = dataQuery.ToSearchActualDate
				 ,FromSearchContractualDate = dataQuery.FromSearchContractualDate
				 , ToSearchContractualDate = dataQuery.ToSearchContractualDate
			};

			List<TxTradeAndSettleDatesDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TxTradeAndSettleDatesDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TxTradeAndSettleDatesDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TxTradeAndSettleDatesDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TxTradeAndSettleDatesDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TxTradeAndSettleDatesDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TxTradeAndSettleDatesInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TxTradeAndSettleDatesUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TxTradeAndSettleDatesDataModel.DataColumns.TxTradeAndSettleDatesId);
			sql = sql + ", " + ToSQLParameter(data, TxTradeAndSettleDatesDataModel.DataColumns.TransactionEventId);
			sql = sql + ", " + ToSQLParameter(data, TxTradeAndSettleDatesDataModel.DataColumns.TradeDate);
			sql = sql + ", " + ToSQLParameter(data, TxTradeAndSettleDatesDataModel.DataColumns.ContractualDate);
			sql = sql + ", " + ToSQLParameter(data, TxTradeAndSettleDatesDataModel.DataColumns.ActualDate);
			sql = sql + ", " + ToSQLParameter(data, TxTradeAndSettleDatesDataModel.DataColumns.SpotDate);
			sql = sql + ", " + ToSQLParameter(data, TxTradeAndSettleDatesDataModel.DataColumns.SettlementDate);
			sql = sql + ", " + ToSQLParameter(data, TxTradeAndSettleDatesDataModel.DataColumns.CreatedDate);
			sql = sql + ", " + ToSQLParameter(data, TxTradeAndSettleDatesDataModel.DataColumns.CreatedByAuditId);
			sql = sql + ", " + ToSQLParameter(data, TxTradeAndSettleDatesDataModel.DataColumns.UpdatedDate);
			sql = sql + ", " + ToSQLParameter(data, TxTradeAndSettleDatesDataModel.DataColumns.ModifiedByAuditId);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TxTradeAndSettleDatesDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TxTradeAndSettleDates.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TxTradeAndSettleDatesDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TxTradeAndSettleDates.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TxTradeAndSettleDatesDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TxTradeAndSettleDatesDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				TxTradeAndSettleDatesId = data.TxTradeAndSettleDatesId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TxTradeAndSettleDatesDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TxTradeAndSettleDatesDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.TransactionEventId = data.TransactionEventId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
