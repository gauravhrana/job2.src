using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace Framework.Components.EventMonitoring
{
	public partial class NotificationPublisherXNotificationEventType : DataAccess.BaseDataManager
	{
        static readonly string DataStoreKey = "";		

		static NotificationPublisherXNotificationEventType()
		{			
			DataStoreKey = SetupConfiguration.GetDataStoreKey("NotificationPublisherXNotificationEventType");
		}

		#region GetDetails

		public static DataTable GetDetails(Data data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationPublisherXNotificationEventTypeSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo,BaseDataManager.ReturnAuditInfoOnDetails) +
				", " + data.ToSQLParameter(DataColumns.NotificationPublisherXNotificationEventTypeId);

			var oDT = new Framework.Components.DataAccess.DBDataTable("NotificationPublisherXNotificationEventType.Details", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Create

		public static void Create(Data data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			Framework.Components.DataAccess.DBDML.RunSQL("NotificationPublisherXNotificationEventType.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(Data data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			Framework.Components.DataAccess.DBDML.RunSQL("NotificationPublisherXNotificationEventType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(Data dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.NotificationPublisherXNotificationEventTypeDelete ";

			var parameters =
			new
			{
					AuditId											= requestProfile.AuditId
				,	NotificationPublisherXNotificationEventTypeId	= dataQuery.NotificationPublisherXNotificationEventTypeId
				,	NotificationPublisherId							= dataQuery.NotificationPublisherId 
				,	NotificationEventTypeId							= dataQuery.NotificationEventTypeId 
				,	CreatedDateId									= dataQuery.CreatedDateId 
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion

		#region Search

		public static DataTable Search(Data data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.NotificationPublisherXNotificationEventTypeSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode,requestProfile.ApplicationModeId) +
				", " + data.ToSQLParameter(DataColumns.NotificationPublisherXNotificationEventTypeId) +
				", " + data.ToSQLParameter(DataColumns.NotificationPublisherId) +				
				", " + data.ToSQLParameter(DataColumns.NotificationEventTypeId) +
				", " + data.ToSQLParameter(DataColumns.CreatedDateId);

			var oDT = new Framework.Components.DataAccess.DBDataTable("NotificationPublisherXNotificationEventType.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region Save

		private static string Save(Data data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.NotificationPublisherXNotificationEventTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.NotificationPublisherXNotificationEventTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				default:
					break;

			}

			sql = sql + ", " + data.ToSQLParameter(DataColumns.NotificationPublisherXNotificationEventTypeId) +
						", " + data.ToSQLParameter(DataColumns.NotificationPublisherId) +
						", " + data.ToSQLParameter(DataColumns.NotificationEventTypeId) +
						", " + data.ToSQLParameter(DataColumns.CreatedDateId);	
					

			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(Data data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationPublisherXNotificationEventTypeSearch " +
			" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
			", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId) +
				", " + data.ToSQLParameter(DataColumns.NotificationPublisherXNotificationEventTypeId);

			var oDT = new Framework.Components.DataAccess.DBDataTable("NotificationPublisherXNotificationEventType.DoesExist", sql, DataStoreKey);
			return oDT.DBTable.Rows.Count > 0;
		}

		#endregion
	}
}