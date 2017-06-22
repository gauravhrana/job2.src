using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;

namespace TaskTimeTracker.Components.BusinessLayer
{
	public partial class EntityDateRangeStateTypeDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static EntityDateRangeStateTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("EntityDateRangeStateType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(EntityDateRangeStateTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case EntityDateRangeStateTypeDataModel.DataColumns.EntityDateRangeStateTypeId:
					if (data.EntityDateRangeStateTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, EntityDateRangeStateTypeDataModel.DataColumns.EntityDateRangeStateTypeId, data.EntityDateRangeStateTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, EntityDateRangeStateTypeDataModel.DataColumns.EntityDateRangeStateTypeId);
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

		public static List<EntityDateRangeStateTypeDataModel> GetEntityDetails(EntityDateRangeStateTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.EntityDateRangeStateTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	EntityDateRangeStateTypeId                  = dataQuery.EntityDateRangeStateTypeId
				 ,	Name                    = dataQuery.Name
			};

			List<EntityDateRangeStateTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<EntityDateRangeStateTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<EntityDateRangeStateTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(EntityDateRangeStateTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(EntityDateRangeStateTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static EntityDateRangeStateTypeDataModel GetDetails(EntityDateRangeStateTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(EntityDateRangeStateTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.EntityDateRangeStateTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.EntityDateRangeStateTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, EntityDateRangeStateTypeDataModel.DataColumns.EntityDateRangeStateTypeId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(EntityDateRangeStateTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("EntityDateRangeStateType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(EntityDateRangeStateTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("EntityDateRangeStateType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(EntityDateRangeStateTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.EntityDateRangeStateTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   EntityDateRangeStateTypeId  = data.EntityDateRangeStateTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(EntityDateRangeStateTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new EntityDateRangeStateTypeDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.EntityDateRangeStateTypeRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("EntityDateRangeStateType.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(EntityDateRangeStateTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.EntityDateRangeStateTypeChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, EntityDateRangeStateTypeDataModel.DataColumns.EntityDateRangeStateTypeId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(EntityDateRangeStateTypeDataModel data, RequestProfile requestProfile)
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
