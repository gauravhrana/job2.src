using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.EventMonitoring;

namespace Framework.Components.EventMonitoring
{
	public partial class NotificationPublisherDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static NotificationPublisherDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("NotificationPublisher");
		}

		#region ToSQLParameter

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

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<NotificationPublisherDataModel> GetEntityDetails(NotificationPublisherDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.NotificationPublisherSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	NotificationPublisherId                  = dataQuery.NotificationPublisherId
				 ,	Name                    = dataQuery.Name
			};

			List<NotificationPublisherDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<NotificationPublisherDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<NotificationPublisherDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(NotificationPublisherDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static NotificationPublisherDataModel GetDetails(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(NotificationPublisherDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.NotificationPublisherInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.NotificationPublisherUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, NotificationPublisherDataModel.DataColumns.NotificationPublisherId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("NotificationPublisher.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("NotificationPublisher.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.NotificationPublisherDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   NotificationPublisherId  = data.NotificationPublisherId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new NotificationPublisherDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationPublisherRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("NotificationPublisher.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(NotificationPublisherDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationPublisherChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, NotificationPublisherDataModel.DataColumns.NotificationPublisherId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
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
				foreach (DataTable dt in ds.Tables)
				{
					if (dt.Rows.Count > 0)
					{
						isDeletable = false;
						break;
					}
				}
			}
			return isDeletable;
		}

		#endregion

	}
}
