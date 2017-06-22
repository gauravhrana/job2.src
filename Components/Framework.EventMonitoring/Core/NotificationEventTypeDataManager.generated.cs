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
	public partial class NotificationEventTypeDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static NotificationEventTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("NotificationEventType");
		}

		#region ToSQLParameter

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

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;
			}

			return returnValue;

		}

		#endregion

		#region Get Entity Details

		public static List<NotificationEventTypeDataModel> GetEntityDetails(NotificationEventTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.NotificationEventTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	NotificationEventTypeId                  = dataQuery.NotificationEventTypeId
				 ,	Name                    = dataQuery.Name
			};

			List<NotificationEventTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<NotificationEventTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<NotificationEventTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(NotificationEventTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static NotificationEventTypeDataModel GetDetails(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(NotificationEventTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.NotificationEventTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.NotificationEventTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("NotificationEventType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("NotificationEventType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.NotificationEventTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   NotificationEventTypeId  = data.NotificationEventTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new NotificationEventTypeDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationEventTypeRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("NotificationEventType.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(NotificationEventTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NotificationEventTypeChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
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
