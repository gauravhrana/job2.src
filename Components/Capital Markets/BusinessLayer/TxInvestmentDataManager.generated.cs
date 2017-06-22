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
	public partial class TxInvestmentDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static TxInvestmentDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TxInvestment");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(TxInvestmentDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TxInvestmentDataModel.DataColumns.TxInvestmentId:
					if (data.TxInvestmentId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxInvestmentDataModel.DataColumns.TxInvestmentId, data.TxInvestmentId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxInvestmentDataModel.DataColumns.TxInvestmentId);
					}
					break;

				case TxInvestmentDataModel.DataColumns.TransactionEventId:
					if (data.TransactionEventId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxInvestmentDataModel.DataColumns.TransactionEventId, data.TransactionEventId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxInvestmentDataModel.DataColumns.TransactionEventId);
					}
					break;

				case TxInvestmentDataModel.DataColumns.TransactionEvent:
					if (!string.IsNullOrEmpty(data.TransactionEvent))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TxInvestmentDataModel.DataColumns.TransactionEvent, data.TransactionEvent);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxInvestmentDataModel.DataColumns.TransactionEvent);
					}
					break;

				case TxInvestmentDataModel.DataColumns.InvestmentId:
					if (data.InvestmentId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxInvestmentDataModel.DataColumns.InvestmentId, data.InvestmentId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxInvestmentDataModel.DataColumns.InvestmentId);
					}
					break;

				case TxInvestmentDataModel.DataColumns.CustAccountId:
					if (data.CustAccountId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TxInvestmentDataModel.DataColumns.CustAccountId, data.CustAccountId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TxInvestmentDataModel.DataColumns.CustAccountId);
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

		public static List<TxInvestmentDataModel> GetEntityDetails(TxInvestmentDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TxInvestmentSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	TxInvestmentId           = dataQuery.TxInvestmentId
				 ,	TransactionEventId           = dataQuery.TransactionEventId
			};

			List<TxInvestmentDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TxInvestmentDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<TxInvestmentDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(TxInvestmentDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(TxInvestmentDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(TxInvestmentDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.TxInvestmentInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TxInvestmentUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, TxInvestmentDataModel.DataColumns.TxInvestmentId); 
			sql = sql + ", " + ToSQLParameter(data, TxInvestmentDataModel.DataColumns.TransactionEventId); 
			sql = sql + ", " + ToSQLParameter(data, TxInvestmentDataModel.DataColumns.InvestmentId); 
			sql = sql + ", " + ToSQLParameter(data, TxInvestmentDataModel.DataColumns.CustAccountId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(TxInvestmentDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("TxInvestment.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(TxInvestmentDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("TxInvestment.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TxInvestmentDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.TxInvestmentDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   TxInvestmentId  = data.TxInvestmentId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(TxInvestmentDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TxInvestmentDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.TransactionEventId  = data.TransactionEventId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
