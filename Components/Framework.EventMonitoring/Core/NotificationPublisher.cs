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
	public partial class NotificationPublisherDataManager : StandardDataManager
	{
		private static string DataStoreKey = "";		

		static NotificationPublisherDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("NotificationPublisher");			
		}

		#region GetList		

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationPublisherSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)
				+ ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
			var oDT = new DBDataTable("NotificationPublisher.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		public static List<NotificationPublisherDataModel> GetNotificationPublisherList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationPublisherSearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var result = new List<NotificationPublisherDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new NotificationPublisherDataModel();

					dataItem.NotificationPublisherId = (int)dbReader[NotificationPublisherDataModel.DataColumns.NotificationPublisherId];
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

		public static string ToSQLParameter(NotificationPublisherDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case NotificationPublisherDataModel.DataColumns.NotificationPublisherId:
					if (data.NotificationPublisherId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationPublisherDataModel.DataColumns.NotificationPublisherId, data.NotificationPublisherId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationPublisherDataModel.DataColumns.NotificationPublisherId);
					}
					break;

				case NotificationPublisherDataModel.DataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationPublisherDataModel.DataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationPublisherDataModel.DataColumns.ApplicationId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

		public static DataTable GetDetails(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region GetEntitySearch

		public static List<NotificationPublisherDataModel> GetEntityDetails(NotificationPublisherDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.NotificationPublisherSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ApplicationId				= requestProfile.ApplicationId
				,	NotificationPublisherId		= dataQuery.NotificationPublisherId
				,	Name						= dataQuery.Name				
				,	ApplicationMode				= requestProfile.ApplicationModeId
				,	ReturnAuditInfo				= returnAuditInfo
			};

			List<NotificationPublisherDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<NotificationPublisherDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}


			return result;
		}

		#endregion GetDetails

		#region CreateOrUpdate
		private static string CreateOrUpdate(NotificationPublisherDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.NotificationPublisherInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				case "Update":
					sql += "dbo.NotificationPublisherUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, NotificationPublisherDataModel.DataColumns.NotificationPublisherId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
						", " + ToSQLParameter(data, NotificationPublisherDataModel.DataColumns.ApplicationId);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			Framework.Components.DataAccess.DBDML.RunSQL("NotificationPublisher.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("Clent.Update", sql, DataStoreKey);
		}

		#endregion Update

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationPublisherRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("NotificationPublisher.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(NotificationPublisherDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.NotificationPublisherDelete ";

			var parameters =
			new
			{
					AuditId					= requestProfile.AuditId
				,	NotificationPublisherId = dataQuery.NotificationPublisherId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion Delete

		#region DoesExist

		public static DataTable DoesExist(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new NotificationPublisherDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationPublisherChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, NotificationPublisherDataModel.DataColumns.NotificationPublisherId);

			var oDT = new DBDataSet("NotificationPublisher.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationPublisherChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, NotificationPublisherDataModel.DataColumns.NotificationPublisherId);

			var oDT = new DBDataSet("NotificationPublisher.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(NotificationPublisherDataModel data, RequestProfile requestProfile)
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

		public static DataTable EventTypeList(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.NotificationPublisherSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, NotificationPublisherDataModel.DataColumns.ApplicationId);

			var oDT = new DataAccess.DBDataTable("EventType.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion
	}
}
