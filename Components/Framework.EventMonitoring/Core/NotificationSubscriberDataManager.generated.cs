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
	public partial class NotificationSubscriberDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static NotificationSubscriberDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("NotificationSubscriber");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(NotificationSubscriberDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case NotificationSubscriberDataModel.DataColumns.NotificationSubscriberId:
					if (data.NotificationSubscriberId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NotificationSubscriberDataModel.DataColumns.NotificationSubscriberId, data.NotificationSubscriberId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NotificationSubscriberDataModel.DataColumns.NotificationSubscriberId);
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

		public static List<NotificationSubscriberDataModel> GetEntityDetails(NotificationSubscriberDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.NotificationSubscriberSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	NotificationSubscriberId                  = dataQuery.NotificationSubscriberId
				 ,	Name                    = dataQuery.Name
			};

			List<NotificationSubscriberDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<NotificationSubscriberDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<NotificationSubscriberDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(NotificationSubscriberDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(NotificationSubscriberDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static NotificationSubscriberDataModel GetDetails(NotificationSubscriberDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(NotificationSubscriberDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.NotificationSubscriberInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.NotificationSubscriberUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, NotificationSubscriberDataModel.DataColumns.NotificationSubscriberId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(NotificationSubscriberDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("NotificationSubscriber.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(NotificationSubscriberDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("NotificationSubscriber.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(NotificationSubscriberDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.NotificationSubscriberDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   NotificationSubscriberId  = data.NotificationSubscriberId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(NotificationSubscriberDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new NotificationSubscriberDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationSubscriberRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("NotificationSubscriber.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(NotificationSubscriberDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationSubscriberChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, NotificationSubscriberDataModel.DataColumns.NotificationSubscriberId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(NotificationSubscriberDataModel data, RequestProfile requestProfile)
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
