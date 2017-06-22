using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Core;

namespace Framework.Components.Core
{
	public partial class SystemForeignRelationshipTypeDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SystemForeignRelationshipTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SystemForeignRelationshipType");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SystemForeignRelationshipTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SystemForeignRelationshipTypeDataModel.DataColumns.SystemForeignRelationshipTypeId:
					if (data.SystemForeignRelationshipTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemForeignRelationshipTypeDataModel.DataColumns.SystemForeignRelationshipTypeId, data.SystemForeignRelationshipTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemForeignRelationshipTypeDataModel.DataColumns.SystemForeignRelationshipTypeId);
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

		public static List<SystemForeignRelationshipTypeDataModel> GetEntityDetails(SystemForeignRelationshipTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SystemForeignRelationshipTypeSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SystemForeignRelationshipTypeId                  = dataQuery.SystemForeignRelationshipTypeId
				 ,	Name                    = dataQuery.Name
			};

			List<SystemForeignRelationshipTypeDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SystemForeignRelationshipTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SystemForeignRelationshipTypeDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SystemForeignRelationshipTypeDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SystemForeignRelationshipTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static SystemForeignRelationshipTypeDataModel GetDetails(SystemForeignRelationshipTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(SystemForeignRelationshipTypeDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SystemForeignRelationshipTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SystemForeignRelationshipTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SystemForeignRelationshipTypeDataModel.DataColumns.SystemForeignRelationshipTypeId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SystemForeignRelationshipTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SystemForeignRelationshipType.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SystemForeignRelationshipTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SystemForeignRelationshipType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SystemForeignRelationshipTypeDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SystemForeignRelationshipTypeDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SystemForeignRelationshipTypeId  = data.SystemForeignRelationshipTypeId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SystemForeignRelationshipTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SystemForeignRelationshipTypeDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SystemForeignRelationshipTypeRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("SystemForeignRelationshipType.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(SystemForeignRelationshipTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SystemForeignRelationshipTypeChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, SystemForeignRelationshipTypeDataModel.DataColumns.SystemForeignRelationshipTypeId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(SystemForeignRelationshipTypeDataModel data, RequestProfile requestProfile)
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
