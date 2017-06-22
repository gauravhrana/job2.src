using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Library.CommonServices.Utils;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.DayCare;

namespace DayCare.Components.BusinessLayer
{
	public partial class ActivitySubTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static ActivitySubTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ActivitySubType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ActivitySubTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ActivitySubTypeDataModel.DataColumns.ActivitySubTypeId:
					if (data.ActivitySubTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ActivitySubTypeDataModel.DataColumns.ActivitySubTypeId, data.ActivitySubTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivitySubTypeDataModel.DataColumns.ActivitySubTypeId);
					}
					break;

				case ActivitySubTypeDataModel.DataColumns.Name:
					if (!string.IsNullOrEmpty(data.Name))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ActivitySubTypeDataModel.DataColumns.Name, data.Name);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivitySubTypeDataModel.DataColumns.Name);
					}
					break;

				case ActivitySubTypeDataModel.DataColumns.Description:
					if (!string.IsNullOrEmpty(data.Description))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ActivitySubTypeDataModel.DataColumns.Description, data.Description);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivitySubTypeDataModel.DataColumns.Description);
					}
					break;

				case ActivitySubTypeDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ActivitySubTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivitySubTypeDataModel.DataColumns.SortOrder);
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

		public static List<ActivitySubTypeDataModel> GetEntityDetails(ActivitySubTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ActivitySubTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ActivitySubTypeId           = dataQuery.ActivitySubTypeId
				 ,	Name           = dataQuery.Name
			};

			List<ActivitySubTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ActivitySubTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<ActivitySubTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ActivitySubTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(ActivitySubTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(ActivitySubTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.ActivitySubTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ActivitySubTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ActivitySubTypeDataModel.DataColumns.ActivitySubTypeId); 
			sql = sql + ", " + ToSQLParameter(data, ActivitySubTypeDataModel.DataColumns.Name); 
			sql = sql + ", " + ToSQLParameter(data, ActivitySubTypeDataModel.DataColumns.Description); 
			sql = sql + ", " + ToSQLParameter(data, ActivitySubTypeDataModel.DataColumns.SortOrder); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ActivitySubTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("ActivitySubType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ActivitySubTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("ActivitySubType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ActivitySubTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ActivitySubTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ActivitySubTypeId  = data.ActivitySubTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(ActivitySubTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ActivitySubTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name  = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
