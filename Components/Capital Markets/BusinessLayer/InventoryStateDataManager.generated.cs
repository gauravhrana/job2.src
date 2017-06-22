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
	public partial class InventoryStateDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static InventoryStateDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("InventoryState");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(InventoryStateDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case InventoryStateDataModel.DataColumns.InventoryStateId:
					if (data.InventoryStateId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, InventoryStateDataModel.DataColumns.InventoryStateId, data.InventoryStateId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InventoryStateDataModel.DataColumns.InventoryStateId);
					}
					break;

				case InventoryStateDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, InventoryStateDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InventoryStateDataModel.DataColumns.Name);
					}
					break;

				case InventoryStateDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, InventoryStateDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InventoryStateDataModel.DataColumns.Description);
					}
					break;

				case InventoryStateDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, InventoryStateDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, InventoryStateDataModel.DataColumns.SortOrder);
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

		public static List<InventoryStateDataModel> GetEntityDetails(InventoryStateDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.InventoryStateSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	InventoryStateId           = dataQuery.InventoryStateId
				 ,	Name           = dataQuery.Name
			};

			List<InventoryStateDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<InventoryStateDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<InventoryStateDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(InventoryStateDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(InventoryStateDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(InventoryStateDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.InventoryStateInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.InventoryStateUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, InventoryStateDataModel.DataColumns.InventoryStateId); 
			sql = sql + ", " + ToSQLParameter(data, InventoryStateDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, InventoryStateDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, InventoryStateDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(InventoryStateDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("InventoryState.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(InventoryStateDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("InventoryState.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(InventoryStateDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.InventoryStateDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   InventoryStateId  = data.InventoryStateId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(InventoryStateDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new InventoryStateDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
