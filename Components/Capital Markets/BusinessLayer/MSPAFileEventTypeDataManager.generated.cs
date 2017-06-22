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
	public partial class MSPAFileEventTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static MSPAFileEventTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("MSPAFileEventType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(MSPAFileEventTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case MSPAFileEventTypeDataModel.DataColumns.MSPAFileEventTypeId:
					if (data.MSPAFileEventTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MSPAFileEventTypeDataModel.DataColumns.MSPAFileEventTypeId, data.MSPAFileEventTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileEventTypeDataModel.DataColumns.MSPAFileEventTypeId);
					}
					break;

				case MSPAFileEventTypeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MSPAFileEventTypeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileEventTypeDataModel.DataColumns.Name);
					}
					break;

				case MSPAFileEventTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MSPAFileEventTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileEventTypeDataModel.DataColumns.Description);
					}
					break;

				case MSPAFileEventTypeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MSPAFileEventTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MSPAFileEventTypeDataModel.DataColumns.SortOrder);
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

		public static List<MSPAFileEventTypeDataModel> GetEntityDetails(MSPAFileEventTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.MSPAFileEventTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	MSPAFileEventTypeId           = dataQuery.MSPAFileEventTypeId
				 ,	Name           = dataQuery.Name
			};

			List<MSPAFileEventTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<MSPAFileEventTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<MSPAFileEventTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(MSPAFileEventTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(MSPAFileEventTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(MSPAFileEventTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.MSPAFileEventTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.MSPAFileEventTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, MSPAFileEventTypeDataModel.DataColumns.MSPAFileEventTypeId); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileEventTypeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileEventTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, MSPAFileEventTypeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(MSPAFileEventTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("MSPAFileEventType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(MSPAFileEventTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("MSPAFileEventType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(MSPAFileEventTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.MSPAFileEventTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   MSPAFileEventTypeId  = data.MSPAFileEventTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(MSPAFileEventTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new MSPAFileEventTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}