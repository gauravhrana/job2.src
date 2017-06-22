using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils; 

namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class DeveloperRoleDataManager : StandardDataManager
	{
        private static readonly string DataStoreKey = "";

		static DeveloperRoleDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("DeveloperRole");
		}

		#region GetList

        public static List<DeveloperRoleDataModel> GetList(RequestProfile requestProfile)
		{
            return GetEntityDetails(DeveloperRoleDataModel.Empty, requestProfile, 1);
		}

		#endregion GetList

		#region Search

		public static string ToSQLParameter(DeveloperRoleDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case DeveloperRoleDataModel.DataColumns.DeveloperRoleId:
					if (data.DeveloperRoleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DeveloperRoleDataModel.DataColumns.DeveloperRoleId, data.DeveloperRoleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DeveloperRoleDataModel.DataColumns.DeveloperRoleId);
					}
					break;


				case DeveloperRoleDataModel.BaseDataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DeveloperRoleDataModel.BaseDataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DeveloperRoleDataModel.BaseDataColumns.ApplicationId);
					}
					break;

				case DeveloperRoleDataModel.DataColumns.Application:
					if (!string.IsNullOrEmpty(data.Application))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DeveloperRoleDataModel.DataColumns.Application, data.Application.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DeveloperRoleDataModel.DataColumns.Application);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(DeveloperRoleDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

		public static DeveloperRoleDataModel GetDetails(DeveloperRoleDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			return list.FirstOrDefault();

		}
		#endregion

		#region GetEntitySearch

		public static List<DeveloperRoleDataModel> GetEntityDetails(DeveloperRoleDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.DeveloperRoleSearch ";

			var parameters =
			new
			{
					AuditId			= requestProfile.AuditId
				,	DeveloperRoleId = dataQuery.DeveloperRoleId
				,	ReturnAuditInfo = returnAuditInfo
				,	Name			= dataQuery.Name
				,	ApplicationMode = requestProfile.ApplicationModeId
			};

			List<DeveloperRoleDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<DeveloperRoleDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}


			return result;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(DeveloperRoleDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.DeveloperRoleInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(data, DeveloperRoleDataModel.BaseDataColumns.ApplicationId);

					break;

				case "Update":
					sql += "dbo.DeveloperRoleUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					", " + ToSQLParameter(data, DeveloperRoleDataModel.BaseDataColumns.ApplicationId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, DeveloperRoleDataModel.DataColumns.DeveloperRoleId) +
						", " + ToSQLParameter(data, DeveloperRoleDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, DeveloperRoleDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, DeveloperRoleDataModel.StandardDataColumns.SortOrder);

			return sql;

		}

		#endregion

		#region Create

		public static int Create(DeveloperRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var Id = DBDML.RunScalarSQL("DeveloperRole.Insert", sql, DataStoreKey);
			return Convert.ToInt32(Id);
		}

		#endregion Create

		#region Update

		public static void Update(DeveloperRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("DeveloperRole.Update", sql, DataStoreKey);
		}

		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.DeveloperRoleRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("DeveloperRole.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(DeveloperRoleDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.DeveloperRoleDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				DeveloperRoleId = dataQuery.DeveloperRoleId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

		public static bool DoesExist(DeveloperRoleDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new DeveloperRoleDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

            return list.Count > 0;
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(DeveloperRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.DeveloperRoleChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, DeveloperRoleDataModel.DataColumns.DeveloperRoleId);

			var oDT = new DBDataSet("DeveloperRole.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(DeveloperRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.DeveloperRoleChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, DeveloperRoleDataModel.DataColumns.DeveloperRoleId);

			var oDT = new DBDataSet("DeveloperRole.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(DeveloperRoleDataModel data, RequestProfile requestProfile)
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
