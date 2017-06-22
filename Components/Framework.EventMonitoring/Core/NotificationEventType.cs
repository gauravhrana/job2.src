using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.EventMonitoring;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.EventMonitoring
{
	public partial class NotificationEventTypeDataManager : StandardDataManager
	{
		private static string DataStoreKey = "";		

		static NotificationEventTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("NotificationEventType");			
		}

		#region GetList		

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationEventTypeSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId)
				+ ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);

			var oDT = new DBDataTable("NotificationEventType.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		public static List<NotificationEventTypeDataModel> GetNotificationEventTypeList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationEventTypeSearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);

			var result = new List<NotificationEventTypeDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new NotificationEventTypeDataModel();

					dataItem.NotificationEventTypeId = (int)dbReader[NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId];
					dataItem.ApplicationId = (int)dbReader[BaseDataModel.BaseDataColumns.ApplicationId];

					SetStandardInfo(dataItem, dbReader);

					SetStandardInfo(dataItem, dbReader);

					result.Add(dataItem);
				}

			}

			return result;
		}

		#endregion GetList

		#region Search

		public static string ToSQLParameter(NotificationEventTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId:
					if (data.NotificationEventTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId, data.NotificationEventTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId);
					}
					break;

				case NotificationEventTypeDataModel.DataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationEventTypeDataModel.DataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationEventTypeDataModel.DataColumns.ApplicationId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

		public static DataTable GetDetails(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region GetEntitySearch

		public static List<NotificationEventTypeDataModel> GetEntityDetails(NotificationEventTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.NotificationEventTypeSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ApplicationId				= requestProfile.ApplicationId
				,	NotificationEventTypeId		= dataQuery.NotificationEventTypeId
				,	Name						= dataQuery.Name
				,	ReturnAuditInfo				= returnAuditInfo
				,	ApplicationMode				= requestProfile.ApplicationModeId 
			};

			List<NotificationEventTypeDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				result = dataAccess.Connection.Query<NotificationEventTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();

				
			}

			return result;
		}

		#endregion

		#region CreateOrUpdate
		private static string CreateOrUpdate(NotificationEventTypeDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.NotificationEventTypeInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);						
					break;

				case "Update":
					sql += "dbo.NotificationEventTypeUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
						", " + ToSQLParameter(data, NotificationEventTypeDataModel.DataColumns.ApplicationId);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			Framework.Components.DataAccess.DBDML.RunSQL("NotificationEventType.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("Clent.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, int auditId)
		{
			var sql = "EXEC dbo.NotificationEventTypeRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("NotificationEventType.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(NotificationEventTypeDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.NotificationEventTypeDelete ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	NotificationEventTypeId		= dataQuery.NotificationEventTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion Delete

		#region DoesExist

		public static DataTable DoesExist(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new NotificationEventTypeDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationEventTypeChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId);

			var oDT = new DBDataSet("NotificationEventType.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationEventTypeChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId);

			var oDT = new DBDataSet("NotificationEventType.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var isDeletable = true;

			var ds = GetChildren(data, requestProfile);

			if (ds != null && ds.Tables.Count > 0)
			{
				if (ds.Tables.Cast<DataTable>().Any(dt => dt.Rows.Count > 0))
				{
					isDeletable = false;
				}
			}

			return isDeletable;
		}

		#endregion

		#region EventTypeList

		public static DataTable EventTypeList(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.NotificationEventTypeSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, NotificationEventTypeDataModel.DataColumns.ApplicationId);

			var oDT = new DataAccess.DBDataTable("EventType.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion
	}
}
