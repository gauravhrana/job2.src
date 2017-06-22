using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.ApplicationUser
{
	public partial class ApplicationRoleDataManager : StandardDataManager
	{
		private static string DataStoreKey = "";
		
		static ApplicationRoleDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationRole");
		}

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationRoleSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)
				+ ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
			var oDT = new DBDataTable("ApplicationRole.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region Search
		public static string ToSQLParameter(ApplicationRoleDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ApplicationRoleDataModel.DataColumns.ApplicationRoleId:
					if (data.ApplicationRoleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationRoleDataModel.DataColumns.ApplicationRoleId, data.ApplicationRoleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationRoleDataModel.DataColumns.ApplicationRoleId);
					}
					break;

				case ApplicationRoleDataModel.DataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationRoleDataModel.DataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationRoleDataModel.DataColumns.ApplicationId);
					}
					break;

				case ApplicationRoleDataModel.DataColumns.Application:
					if (data.Application != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationRoleDataModel.DataColumns.Application, data.Application);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationRoleDataModel.DataColumns.Application);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(ApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

		public static DataTable GetDetails(ApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region GetEntitySearch

		public static List<ApplicationRoleDataModel> GetEntityDetails(ApplicationRoleDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			
			const string sql = @"dbo.ApplicationRoleSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ApplicationId				= requestProfile.ApplicationId
				,	ApplicationMode				= requestProfile.ApplicationModeId
				,	ReturnAuditInfo				= returnAuditInfo
				,	ApplicationRoleId			= dataQuery.ApplicationRoleId
				,	Name						= dataQuery.Name
			};

			List<ApplicationRoleDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ApplicationRoleDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion GetDetails

		#region CreateOrUpdate
		private static string CreateOrUpdate(ApplicationRoleDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ApplicationRoleInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ApplicationRoleUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, ApplicationRoleDataModel.DataColumns.ApplicationRoleId) +						
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static int Create(ApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var Id = DataAccess.DBDML.RunScalarSQL("ApplicationRole.Insert", sql, DataStoreKey);
			return Convert.ToInt32(Id);
			
		}

		#endregion Create

		#region Update

		public static void Update(ApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("ApplicationRole.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationRoleRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("ApplicationRole.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(ApplicationRoleDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ApplicationRoleDelete ";

			var parameters =
			new
			{
					AuditId				= requestProfile.AuditId
				,	ApplicationRoleId	= dataQuery.ApplicationRoleId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion Delete

		#region DoesExist

		public static DataTable DoesExist(ApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ApplicationRoleDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(ApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationRoleChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ApplicationRoleDataModel.DataColumns.ApplicationRoleId);

			var oDT = new DBDataSet("ApplicationRole.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(ApplicationRoleDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationRoleChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ApplicationRoleDataModel.DataColumns.ApplicationRoleId);

			var oDT = new DBDataSet("ApplicationRole.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ApplicationRoleDataModel data, RequestProfile requestProfile)
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

		public static void Sync(int newApplicationId, int oldApplicationId, RequestProfile requestProfile)
		{
			// get all records for old Application Id
			var sql = "EXEC dbo.ApplicationRoleSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, oldApplicationId);

			var oDT = new DBDataTable("ApplicationRole.Search", sql, DataStoreKey);

			// formaulate a new request Profile which will have new Applicationid
			var newRequestProfile = new RequestProfile();
			newRequestProfile.ApplicationId = newApplicationId;
			newRequestProfile.AuditId = requestProfile.AuditId;

			foreach (DataRow dr in oDT.DBTable.Rows)
			{
				var data = new ApplicationRoleDataModel();
				data.ApplicationId = newApplicationId;
				data.Name = dr[ApplicationRoleDataModel.DataColumns.Name].ToString();

				// check for existing record in new Application Id
				var dt = DoesExist(data, newRequestProfile);
				if (dt == null || dt.Rows.Count == 0)
				{
					data.Description = dr[ApplicationRoleDataModel.DataColumns.Description].ToString();
					data.SortOrder = Convert.ToInt32(dr[ApplicationRoleDataModel.DataColumns.SortOrder]);

					//create in new application id
					Create(data, newRequestProfile);

				}

			}
		}

	}
}
