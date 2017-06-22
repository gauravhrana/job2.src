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
	public partial class SubscriberApplicationRoleDataManager : StandardDataManager
	{

		private static readonly string DataStoreKey = string.Empty;

		static SubscriberApplicationRoleDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SubscriberApplicationRole");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(SubscriberApplicationRoleDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case SubscriberApplicationRoleDataModel.DataColumns.SubscriberApplicationRoleId:
					if (data.SubscriberApplicationRoleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SubscriberApplicationRoleDataModel.DataColumns.SubscriberApplicationRoleId, data.SubscriberApplicationRoleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SubscriberApplicationRoleDataModel.DataColumns.SubscriberApplicationRoleId);
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

		public static List<SubscriberApplicationRoleDataModel> GetEntityDetails(SubscriberApplicationRoleDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SubscriberApplicationRoleSearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	SubscriberApplicationRoleId                  = dataQuery.SubscriberApplicationRoleId
				 ,	Name                    = dataQuery.Name
			};

			List<SubscriberApplicationRoleDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<SubscriberApplicationRoleDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static List<SubscriberApplicationRoleDataModel> GetList(RequestProfile requestProfile)
		{
			return GetEntityDetails(SubscriberApplicationRoleDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region Search

		public static DataTable Search(SubscriberApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Get Details

		public static SubscriberApplicationRoleDataModel GetDetails(SubscriberApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 1);
			return list.FirstOrDefault();
		}

		#endregion

		#region Save

		public static string Save(SubscriberApplicationRoleDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SubscriberApplicationRoleInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.SubscriberApplicationRoleUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, SubscriberApplicationRoleDataModel.DataColumns.SubscriberApplicationRoleId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(SubscriberApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("SubscriberApplicationRole.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(SubscriberApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("SubscriberApplicationRole.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SubscriberApplicationRoleDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.SubscriberApplicationRoleDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   SubscriberApplicationRoleId  = data.SubscriberApplicationRoleId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static bool DoesExist(SubscriberApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SubscriberApplicationRoleDataModel();

			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.Name = data.Name;

			var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

			return list.Count > 0;
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SubscriberApplicationRoleRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("SubscriberApplicationRole.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(SubscriberApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.SubscriberApplicationRoleChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, SubscriberApplicationRoleDataModel.DataColumns.SubscriberApplicationRoleId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(SubscriberApplicationRoleDataModel data, RequestProfile requestProfile)
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
