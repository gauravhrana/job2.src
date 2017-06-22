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
	public partial class OrderRequestDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static OrderRequestDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("OrderRequest");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(OrderRequestDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case OrderRequestDataModel.DataColumns.OrderRequestId:
					if (data.OrderRequestId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderRequestDataModel.DataColumns.OrderRequestId, data.OrderRequestId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderRequestDataModel.DataColumns.OrderRequestId);
					}
					break;

				case OrderRequestDataModel.DataColumns.EventDate:
					if (data.EventDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderRequestDataModel.DataColumns.EventDate, data.EventDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderRequestDataModel.DataColumns.EventDate);
					}
					break;

				case OrderRequestDataModel.DataColumns.Notes:
					if (!string.IsNullOrEmpty(data.Notes))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderRequestDataModel.DataColumns.Notes, data.Notes);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderRequestDataModel.DataColumns.Notes);
					}
					break;

				case OrderRequestDataModel.DataColumns.LastModifiedBy:
					if (!string.IsNullOrEmpty(data.LastModifiedBy))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderRequestDataModel.DataColumns.LastModifiedBy, data.LastModifiedBy);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderRequestDataModel.DataColumns.LastModifiedBy);
					}
					break;

				case OrderRequestDataModel.DataColumns.LastModifiedOn:
					if (data.LastModifiedOn != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderRequestDataModel.DataColumns.LastModifiedOn, data.LastModifiedOn);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderRequestDataModel.DataColumns.LastModifiedOn);
					}
					break;

				case OrderRequestDataModel.DataColumns.ParentOrderRequestId:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderRequestDataModel.DataColumns.ParentOrderRequestId, data.ParentOrderRequestId);
					break;

				case OrderRequestDataModel.DataColumns.PortfolioId:
					if (data.PortfolioId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderRequestDataModel.DataColumns.PortfolioId, data.PortfolioId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderRequestDataModel.DataColumns.PortfolioId);
					}
					break;

				case OrderRequestDataModel.DataColumns.Portfolio:
					if (!string.IsNullOrEmpty(data.Portfolio))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderRequestDataModel.DataColumns.Portfolio, data.Portfolio);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderRequestDataModel.DataColumns.Portfolio);
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

		public static List<OrderRequestDataModel> GetEntityDetails(OrderRequestDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.OrderRequestSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	OrderRequestId           = dataQuery.OrderRequestId
				 ,	Notes           = dataQuery.Notes
				 ,	PortfolioId           = dataQuery.PortfolioId
			};

			List<OrderRequestDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<OrderRequestDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<OrderRequestDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(OrderRequestDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(OrderRequestDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(OrderRequestDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.OrderRequestInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.OrderRequestUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, OrderRequestDataModel.DataColumns.OrderRequestId); 
			sql = sql + ", " + ToSQLParameter(data, OrderRequestDataModel.DataColumns.EventDate); 
			sql = sql + ", " + ToSQLParameter(data, OrderRequestDataModel.DataColumns.Notes); 
			sql = sql + ", " + ToSQLParameter(data, OrderRequestDataModel.DataColumns.LastModifiedBy); 
			sql = sql + ", " + ToSQLParameter(data, OrderRequestDataModel.DataColumns.LastModifiedOn); 
			sql = sql + ", " + ToSQLParameter(data, OrderRequestDataModel.DataColumns.ParentOrderRequestId); 
			sql = sql + ", " + ToSQLParameter(data, OrderRequestDataModel.DataColumns.PortfolioId); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(OrderRequestDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("OrderRequest.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(OrderRequestDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("OrderRequest.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(OrderRequestDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.OrderRequestDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   OrderRequestId  = data.OrderRequestId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(OrderRequestDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new OrderRequestDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.PortfolioId  = data.PortfolioId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
