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
	public partial class OrderStatusGroupDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static OrderStatusGroupDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("OrderStatusGroup");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(OrderStatusGroupDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case OrderStatusGroupDataModel.DataColumns.OrderStatusGroupId:
					if (data.OrderStatusGroupId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderStatusGroupDataModel.DataColumns.OrderStatusGroupId, data.OrderStatusGroupId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusGroupDataModel.DataColumns.OrderStatusGroupId);
					}
					break;

				case OrderStatusGroupDataModel.DataColumns.OrderStatusGroupCode:
					if (!string.IsNullOrEmpty(data.OrderStatusGroupCode))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderStatusGroupDataModel.DataColumns.OrderStatusGroupCode, data.OrderStatusGroupCode);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusGroupDataModel.DataColumns.OrderStatusGroupCode);
					}
					break;

				case OrderStatusGroupDataModel.DataColumns.OrderStatusGroupDescription:
					if (!string.IsNullOrEmpty(data.OrderStatusGroupDescription))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, OrderStatusGroupDataModel.DataColumns.OrderStatusGroupDescription, data.OrderStatusGroupDescription);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, OrderStatusGroupDataModel.DataColumns.OrderStatusGroupDescription);
					}
					break;

				case OrderStatusGroupDataModel.DataColumns.OrderProcessFlag:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, OrderStatusGroupDataModel.DataColumns.OrderProcessFlag, data.OrderProcessFlag);
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<OrderStatusGroupDataModel> GetEntityDetails(OrderStatusGroupDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.OrderStatusGroupSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	OrderStatusGroupId           = dataQuery.OrderStatusGroupId
				 ,	OrderStatusGroupCode           = dataQuery.OrderStatusGroupCode
			};

			List<OrderStatusGroupDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<OrderStatusGroupDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<OrderStatusGroupDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(OrderStatusGroupDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(OrderStatusGroupDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(OrderStatusGroupDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.OrderStatusGroupInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.OrderStatusGroupUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, OrderStatusGroupDataModel.DataColumns.OrderStatusGroupId); 
			sql = sql + ", " + ToSQLParameter(data, OrderStatusGroupDataModel.DataColumns.OrderStatusGroupCode); 
			sql = sql + ", " + ToSQLParameter(data, OrderStatusGroupDataModel.DataColumns.OrderStatusGroupDescription); 
			sql = sql + ", " + ToSQLParameter(data, OrderStatusGroupDataModel.DataColumns.OrderProcessFlag); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(OrderStatusGroupDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("OrderStatusGroup.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(OrderStatusGroupDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("OrderStatusGroup.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(OrderStatusGroupDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.OrderStatusGroupDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   OrderStatusGroupId  = data.OrderStatusGroupId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(OrderStatusGroupDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new OrderStatusGroupDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
