using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
	public partial class ActivityDataManager : StandardDataManager
	{
        private static readonly string DataStoreKey = "";

		static ActivityDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Activity");

		}

		#region GetList

        public static List<ActivityDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ActivityDataModel.Empty, requestProfile);
		}

		#endregion GetList

		#region Search

		public static string ToSQLParameter(ActivityDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ActivityDataModel.DataColumns.ActivityId:
					if (data.ActivityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ActivityDataModel.DataColumns.ActivityId, data.ActivityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivityDataModel.DataColumns.ActivityId);
					}
					break;

				case ActivityDataModel.DataColumns.LayerId:
					if (data.LayerId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ActivityDataModel.DataColumns.LayerId, data.LayerId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivityDataModel.DataColumns.LayerId);
					}
					break;

				case ActivityDataModel.DataColumns.Layer:
					if (data.Layer != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ActivityDataModel.DataColumns.Layer, data.Layer);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ActivityDataModel.DataColumns.Layer);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(ActivityDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

        public static ActivityDataModel GetDetails(ActivityDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
		}

		static public List<ActivityDataModel> GetEntityDetails(ActivityDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ActivitySearch ";

			var parameters =
			new
			{
				    AuditId             = requestProfile.AuditId
			   ,    ActivityId          = dataQuery.ActivityId
			   ,    LayerId             = dataQuery.LayerId
			   ,    Name                = dataQuery.Name
			   ,    ApplicationId       = requestProfile.ApplicationId
			   ,    ApplicationMode     = requestProfile.ApplicationModeId
			};

			List<ActivityDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ActivityDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		//public static List<ActivityDataModel> GetEntityDetails(ActivityDataModel data, int auditId)
		//{
		//	var sql = "EXEC dbo.ActivitySearch " +
		//			  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
		//			  ", " + ToSQLParameter(data, ActivityDataModel.DataColumns.ActivityId);

		//	var result = new List<ActivityDataModel>();

		//	using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
		//	{
		//		var dbReader = reader.DBReader;

		//		while (dbReader.Read())
		//		{
		//			var dataItem = new ActivityDataModel();

		//			dataItem.ActivityId = (int)dbReader[ActivityDataModel.DataColumns.ActivityId];
		//			dataItem.LayerId = (int)dbReader[ActivityDataModel.DataColumns.LayerId];
		//			dataItem.Layer = (string)dbReader[ActivityDataModel.DataColumns.Layer];

		//			SetStandardInfo(dataItem, dbReader);

		//			SetBaseInfo(dataItem, dbReader);

		//			result.Add(dataItem);
		//		}
		//	}

		//	return result;
		//}

		#endregion GetDetails

		#region CreateOrUpdate
		private static string CreateOrUpdate(ActivityDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ActivityInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ActivityUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, ActivityDataModel.DataColumns.ActivityId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
						", " + ToSQLParameter(data, ActivityDataModel.DataColumns.LayerId);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(ActivityDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			DBDML.RunSQL("Activity.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(ActivityDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("Activity.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ActivityRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("Activity.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(ActivityDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ActivityDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ActivityId = dataQuery.ActivityId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

		public static bool DoesExist(ActivityDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ActivityDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile);
            return list.Count > 0;
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(ActivityDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ActivityChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ActivityDataModel.DataColumns.ActivityId);

			var oDT = new DBDataSet("Activity.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(ActivityDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ActivityChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ActivityDataModel.DataColumns.ActivityId);

			var oDT = new DBDataSet("Activity.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ActivityDataModel data, RequestProfile requestProfile)
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
	}
}
