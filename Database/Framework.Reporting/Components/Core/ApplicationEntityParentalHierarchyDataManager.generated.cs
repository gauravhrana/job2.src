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
	public partial class ApplicationEntityParentalHierarchyDataManager : StandardDataManager
	{

		private static string DataStoreKey = string.Empty;

		static ApplicationEntityParentalHierarchyDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationEntityParentalHierarchy");
		}

		#region ToSQLParameter

		public static string ToSQLParameter(ApplicationEntityParentalHierarchyDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ApplicationEntityParentalHierarchyDataModel.DataColumns.ApplicationEntityParentalHierarchyId:
					if (data.ApplicationEntityParentalHierarchyId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationEntityParentalHierarchyDataModel.DataColumns.ApplicationEntityParentalHierarchyId, data.ApplicationEntityParentalHierarchyId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationEntityParentalHierarchyDataModel.DataColumns.ApplicationEntityParentalHierarchyId);
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

		public static List<ApplicationEntityParentalHierarchyDataModel> GetEntityDetails(ApplicationEntityParentalHierarchyDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ApplicationEntityParentalHierarchySearch ";

			var parameters =
			new
			{
					AuditId                 = requestProfile.AuditId
				 ,	ApplicationId           = requestProfile.ApplicationId
				 ,	ApplicationMode         = requestProfile.ApplicationModeId
				 ,	ReturnAuditInfo         = returnAuditInfo
				 ,	ApplicationEntityParentalHierarchyId                  = dataQuery.ApplicationEntityParentalHierarchyId
				 ,	Name                    = dataQuery.Name
			};

			List<ApplicationEntityParentalHierarchyDataModel> result;
			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ApplicationEntityParentalHierarchyDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;

		}

		#endregion

		#region Get List

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var list = GetEntityDetails(ApplicationEntityParentalHierarchyDataModel.Empty, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Search

		public static DataTable Search(ApplicationEntityParentalHierarchyDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(ApplicationEntityParentalHierarchyDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);
			return list.ToDataTable();
		}

		#endregion

		#region Save

		public static string Save(ApplicationEntityParentalHierarchyDataModel data, string action, RequestProfile requestProfile)
		{

			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ApplicationEntityParentalHierarchyInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ApplicationEntityParentalHierarchyUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}
			sql = sql + ", " + ToSQLParameter(data, ApplicationEntityParentalHierarchyDataModel.DataColumns.ApplicationEntityParentalHierarchyId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion

		#region Create

		public static int Create(ApplicationEntityParentalHierarchyDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("ApplicationEntityParentalHierarchy.Insert", sql, DataStoreKey);
			return Convert.ToInt32(newId);
		}

		#endregion

		#region Update

		public static void Update(ApplicationEntityParentalHierarchyDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, "Update", requestProfile);
			DBDML.RunSQL("ApplicationEntityParentalHierarchy.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ApplicationEntityParentalHierarchyDataModel data, RequestProfile requestProfile)
		{

			const string sql = @"dbo.ApplicationEntityParentalHierarchyDelete ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,   ApplicationEntityParentalHierarchyId  = data.ApplicationEntityParentalHierarchyId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		#endregion

		#region Does Exist

		public static DataTable DoesExist(ApplicationEntityParentalHierarchyDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ApplicationEntityParentalHierarchyDataModel();
			doesExistRequest.Name = data.Name;
			return Search(doesExistRequest, requestProfile);
		}

		#endregion

		#region Renumber

		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationEntityParentalHierarchyRenumber" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", @Seed = " + seed +
				", @Increment = " + increment;
			Framework.Components.DataAccess.DBDML.RunSQL("ApplicationEntityParentalHierarchy.Renumber", sql, DataStoreKey);
		}

		#endregion

		#region Get Children

		public static DataSet GetChildren(ApplicationEntityParentalHierarchyDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationEntityParentalHierarchyChildrenGet" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(data, ApplicationEntityParentalHierarchyDataModel.DataColumns.ApplicationEntityParentalHierarchyId);
			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ApplicationEntityParentalHierarchyDataModel data, RequestProfile requestProfile)
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
