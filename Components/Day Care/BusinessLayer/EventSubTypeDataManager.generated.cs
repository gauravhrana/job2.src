using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.DayCare;

namespace DayCare.Components.BusinessLayer
{
	public partial class EventSubTypeDataManager : BaseDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static EventSubTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("EventSubType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(EventSubTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case EventSubTypeDataModel.DataColumns.EventSubTypeId:
					if (data.EventSubTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EventSubTypeDataModel.DataColumns.EventSubTypeId, data.EventSubTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EventSubTypeDataModel.DataColumns.EventSubTypeId);
					}
					break;

				case EventSubTypeDataModel.DataColumns.EventTypeId:
					if (data.EventTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EventSubTypeDataModel.DataColumns.EventTypeId, data.EventTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EventSubTypeDataModel.DataColumns.EventTypeId);
					}
					break;

				case EventSubTypeDataModel.DataColumns.EventType:
					if (!string.IsNullOrEmpty(data.EventType))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, EventSubTypeDataModel.DataColumns.EventType, data.EventType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EventSubTypeDataModel.DataColumns.EventType);
					}
					break;

				case EventSubTypeDataModel.DataColumns.PersonId:
					if (data.PersonId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EventSubTypeDataModel.DataColumns.PersonId, data.PersonId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EventSubTypeDataModel.DataColumns.PersonId);
					}
					break;

				case EventSubTypeDataModel.DataColumns.Person:
					if (!string.IsNullOrEmpty(data.Person))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, EventSubTypeDataModel.DataColumns.Person, data.Person);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EventSubTypeDataModel.DataColumns.Person);
					}
					break;

				case EventSubTypeDataModel.DataColumns.EventKey:
					if (!string.IsNullOrEmpty(data.EventKey))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, EventSubTypeDataModel.DataColumns.EventKey, data.EventKey);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EventSubTypeDataModel.DataColumns.EventKey);
					}
					break;

				case EventSubTypeDataModel.DataColumns.SortOrder:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EventSubTypeDataModel.DataColumns.SortOrder, data.SortOrder);
					break;

				case EventSubTypeDataModel.DataColumns.Value:
					returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EventSubTypeDataModel.DataColumns.Value, data.Value);
					break;


				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<EventSubTypeDataModel> GetEntityDetails(EventSubTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.EventSubTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	EventSubTypeId           = dataQuery.EventSubTypeId
				 ,	EventTypeId           = dataQuery.EventTypeId
				 ,	PersonId           = dataQuery.PersonId
				 ,	EventKey           = dataQuery.EventKey
			};

			List<EventSubTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<EventSubTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<EventSubTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(EventSubTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(EventSubTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save


		public static string Save(EventSubTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";


			switch (action)
			{
				case "Create":
					sql += "dbo.EventSubTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.EventSubTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, EventSubTypeDataModel.DataColumns.EventSubTypeId); 
			sql = sql + ", " + ToSQLParameter(data, EventSubTypeDataModel.DataColumns.EventTypeId); 
			sql = sql + ", " + ToSQLParameter(data, EventSubTypeDataModel.DataColumns.PersonId); 
			sql = sql + ", " + ToSQLParameter(data, EventSubTypeDataModel.DataColumns.EventKey); 
			sql = sql + ", " + ToSQLParameter(data, EventSubTypeDataModel.DataColumns.SortOrder); 
			sql = sql + ", " + ToSQLParameter(data, EventSubTypeDataModel.DataColumns.Value); 

			return sql;
		}

		#endregion

		#region Create

		public static int Create(EventSubTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("EventSubType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(EventSubTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("EventSubType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(EventSubTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.EventSubTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   EventSubTypeId  = data.EventSubTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(EventSubTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new EventSubTypeDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

	}
}
