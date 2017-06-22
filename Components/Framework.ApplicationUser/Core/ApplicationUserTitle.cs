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
	public partial class ApplicationUserTitleDataManager : StandardDataManager
	{
		private static string DataStoreKey = "";
		
		static ApplicationUserTitleDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationUserTitle");
		}

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationUserTitleSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)
				+ ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
			var oDT = new DBDataTable("ApplicationUserTitle.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region Search
		public static string ToSQLParameter(ApplicationUserTitleDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId:
					if (data.ApplicationUserTitleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId, data.ApplicationUserTitleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId);
					}
					break;				

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

		public static DataTable GetDetails(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region GetEntitySearch


		public static List<ApplicationUserTitleDataModel> GetEntityDetails(ApplicationUserTitleDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ApplicationUserTitleSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ApplicationId				= requestProfile.ApplicationId
				,	ApplicationUserTitleId		= dataQuery.ApplicationUserTitleId
				,	Name						= dataQuery.Name
				,	ApplicationMode				= requestProfile.ApplicationModeId
				,	ReturnAuditInfo				= returnAuditInfo
			};

			List<ApplicationUserTitleDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ApplicationUserTitleDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}
			return result;
		}

		#endregion GetDetails

		#region CreateOrUpdate
		private static string CreateOrUpdate(ApplicationUserTitleDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ApplicationUserTitleInsert  " + "\r\n" +
						" " + BaseDataManager.SetCommonParameters(requestProfile.AuditId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ApplicationUserTitleUpdate  " + "\r\n" +
						" " + BaseDataManager.SetCommonParameters(requestProfile.AuditId, requestProfile.ApplicationId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static int Create(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			var id = DataAccess.DBDML.RunScalarSQL("ApplicationUserTitle.Insert", sql, DataStoreKey);
			return Convert.ToInt32(id);

		}

		#endregion Create

		#region Update

		public static void Update(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("ApplicationUserTitle.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationUserTitleRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("ApplicationUserTitle.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(ApplicationUserTitleDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ApplicationUserTitleDelete ";

			var parameters =
			new
			{
					AuditId					= requestProfile.AuditId
				,	ApplicationUserTitleId	= dataQuery.ApplicationUserTitleId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion Delete

		#region DoesExist

		public static DataTable DoesExist(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ApplicationUserTitleDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationUserTitleChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId);

			var oDT = new DBDataSet("ApplicationUserTitle.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationUserTitleChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId);

			var oDT = new DBDataSet("ApplicationUserTitle.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ApplicationUserTitleDataModel data, RequestProfile requestProfile)
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
