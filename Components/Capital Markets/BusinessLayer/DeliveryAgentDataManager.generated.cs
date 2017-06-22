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
	public partial class DeliveryAgentDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static DeliveryAgentDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("DeliveryAgent");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(DeliveryAgentDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DeliveryAgentDataModel.DataColumns.DeliveryAgentId:
					if (data.DeliveryAgentId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DeliveryAgentDataModel.DataColumns.DeliveryAgentId, data.DeliveryAgentId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DeliveryAgentDataModel.DataColumns.DeliveryAgentId);
					}
					break;

				case DeliveryAgentDataModel.DataColumns.Url:
					if (!string.IsNullOrEmpty(data.Url))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DeliveryAgentDataModel.DataColumns.Url, data.Url);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DeliveryAgentDataModel.DataColumns.Url);
					}
					break;

				case DeliveryAgentDataModel.DataColumns.Code:
					if (!string.IsNullOrEmpty(data.Code))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DeliveryAgentDataModel.DataColumns.Code, data.Code);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DeliveryAgentDataModel.DataColumns.Code);
					}
					break;

				case DeliveryAgentDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DeliveryAgentDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DeliveryAgentDataModel.DataColumns.Name);
					}
					break;

				case DeliveryAgentDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DeliveryAgentDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DeliveryAgentDataModel.DataColumns.Description);
					}
					break;

				case DeliveryAgentDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DeliveryAgentDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DeliveryAgentDataModel.DataColumns.SortOrder);
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

		public static List<DeliveryAgentDataModel> GetEntityDetails(DeliveryAgentDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.DeliveryAgentSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	DeliveryAgentId           = dataQuery.DeliveryAgentId
				 ,	Name           = dataQuery.Name
			};

			List<DeliveryAgentDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<DeliveryAgentDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<DeliveryAgentDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(DeliveryAgentDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(DeliveryAgentDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static void FormatData(DeliveryAgentDataModel data)
		{
			data.Code = Formatter.FormatCode(data.Code);
		}

		public static string Save(DeliveryAgentDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			FormatData(data);

			switch (action)
			{
				case "Create":
					sql += "dbo.DeliveryAgentInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.DeliveryAgentUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, DeliveryAgentDataModel.DataColumns.DeliveryAgentId); 
			sql = sql + ", " + ToSQLParameter(data, DeliveryAgentDataModel.DataColumns.Url); 
			sql = sql + ", " + ToSQLParameter(data, DeliveryAgentDataModel.DataColumns.Code); 
			sql = sql + ", " + ToSQLParameter(data, DeliveryAgentDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, DeliveryAgentDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, DeliveryAgentDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(DeliveryAgentDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("DeliveryAgent.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(DeliveryAgentDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("DeliveryAgent.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(DeliveryAgentDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.DeliveryAgentDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   DeliveryAgentId  = data.DeliveryAgentId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(DeliveryAgentDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new DeliveryAgentDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
