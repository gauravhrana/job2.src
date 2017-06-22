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
	public partial class OrderActionDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static OrderActionDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("OrderAction");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(OrderActionDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case OrderActionDataModel.DataColumns.OrderActionId:
					if (data.OrderActionId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderActionDataModel.DataColumns.OrderActionId, data.OrderActionId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderActionDataModel.DataColumns.OrderActionId);
					}
					break;

				case OrderActionDataModel.DataColumns.OrderActionCode:
					if (!string.IsNullOrEmpty(data.OrderActionCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderActionDataModel.DataColumns.OrderActionCode, data.OrderActionCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderActionDataModel.DataColumns.OrderActionCode);
					}
					break;

				case OrderActionDataModel.DataColumns.OrderActionDescription:
					if (!string.IsNullOrEmpty(data.OrderActionDescription))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderActionDataModel.DataColumns.OrderActionDescription, data.OrderActionDescription);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderActionDataModel.DataColumns.OrderActionDescription);
					}
					break;

				case OrderActionDataModel.DataColumns.PositionDirection:
					if (!string.IsNullOrEmpty(data.PositionDirection))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderActionDataModel.DataColumns.PositionDirection, data.PositionDirection);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderActionDataModel.DataColumns.PositionDirection);
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

		public static List<OrderActionDataModel> GetEntityDetails(OrderActionDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.OrderActionSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	OrderActionId           = dataQuery.OrderActionId
				 ,	OrderActionCode           = dataQuery.OrderActionCode
			};

			List<OrderActionDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<OrderActionDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<OrderActionDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(OrderActionDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(OrderActionDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(OrderActionDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.OrderActionInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.OrderActionUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, OrderActionDataModel.DataColumns.OrderActionId); 
			sql = sql + ", " + ToSQLParameter(data, OrderActionDataModel.DataColumns.OrderActionCode); 
			sql = sql + ", " + ToSQLParameter(data, OrderActionDataModel.DataColumns.OrderActionDescription); 
			sql = sql + ", " + ToSQLParameter(data, OrderActionDataModel.DataColumns.PositionDirection); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(OrderActionDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("OrderAction.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(OrderActionDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("OrderAction.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(OrderActionDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.OrderActionDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   OrderActionId  = data.OrderActionId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(OrderActionDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new OrderActionDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
