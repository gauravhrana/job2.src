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
	public partial class FunctionalityStatusDataManager : StandardDataManager
	{
        private static readonly string DataStoreKey = "";

		static FunctionalityStatusDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityStatus");
		}

		#region GetList

        public static List<FunctionalityStatusDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityStatusDataModel.Empty, requestProfile, 1);
		}

		#endregion GetList

		#region Search
		public static string ToSQLParameter(FunctionalityStatusDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FunctionalityStatusDataModel.DataColumns.FunctionalityStatusId:
					if (data.FunctionalityStatusId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityStatusDataModel.DataColumns.FunctionalityStatusId, data.FunctionalityStatusId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityStatusDataModel.DataColumns.FunctionalityStatusId);
					}
					break;

				case DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityStatusDataModel.BaseDataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityStatusDataModel.BaseDataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityStatusDataModel.BaseDataColumns.ApplicationId);
					}
					break;

				case DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityStatusDataModel.DataColumns.Application:
					if (!string.IsNullOrEmpty(data.Application))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityStatusDataModel.DataColumns.Application, data.Application.Trim());
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataModel.TaskTimeTracker.ApplicationDevelopment.FunctionalityStatusDataModel.DataColumns.Application);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(FunctionalityStatusDataModel data, RequestProfile requestedProfile)
		{
			var list = GetEntityDetails(data, requestedProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

		public static FunctionalityStatusDataModel GetDetails(FunctionalityStatusDataModel data, RequestProfile requestedProfile)
		{
			var list = GetEntityDetails(data, requestedProfile);

			return list.FirstOrDefault();

		}

		#endregion

		#region GetEntitySearch

		public static List<FunctionalityStatusDataModel> GetEntityDetails(FunctionalityStatusDataModel dataQuery, RequestProfile requestedProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FunctionalityStatusSearch ";

			var parameters =
			new
			{
					AuditId					= requestedProfile.AuditId
				,	ApplicationId			= dataQuery.ApplicationId
				,	FunctionalityStatusId	= dataQuery.FunctionalityStatusId
				,	Name					= dataQuery.Name
				,	ApplicationMode			= requestedProfile.ApplicationModeId
				,	ReturnAuditInfo			= returnAuditInfo
			};

			List<FunctionalityStatusDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityStatusDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}


			return result;
		}

		#endregion

		#region Save
		private static string Save(FunctionalityStatusDataModel data, RequestProfile requestedProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityStatusInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestedProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FunctionalityStatusUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId) +
						   ", " + ToSQLParameter(data, FunctionalityStatusDataModel.BaseDataColumns.ApplicationId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, FunctionalityStatusDataModel.DataColumns.FunctionalityStatusId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(FunctionalityStatusDataModel data, RequestProfile requestedProfile)
		{
			var sql = Save(data, requestedProfile, "Create");

			DBDML.RunSQL("FunctionalityStatus.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(FunctionalityStatusDataModel data, RequestProfile requestedProfile)
		{
			var sql = Save(data, requestedProfile, "Update");

			DBDML.RunSQL("FunctionalityStatus.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestedProfile)
		{
			var sql = "EXEC dbo.FunctionalityStatusRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("FunctionalityStatus.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(FunctionalityStatusDataModel dataQuery, RequestProfile requestedProfile)
		{
			const string sql = @"dbo.FunctionalityStatusDelete ";

			var parameters =
			new
			{
				AuditId = requestedProfile.AuditId
				,
				FunctionalityStatusId = dataQuery.FunctionalityStatusId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

        public static bool DoesExist(FunctionalityStatusDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FunctionalityStatusDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(FunctionalityStatusDataModel data, RequestProfile requestedProfile)
		{
			var sql = "EXEC dbo.FunctionalityStatusChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId) +
							", " + ToSQLParameter(data, FunctionalityStatusDataModel.DataColumns.FunctionalityStatusId);

			var oDT = new DBDataSet("FunctionalityStatus.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(FunctionalityStatusDataModel data, RequestProfile requestedProfile)
		{
			var sql = "EXEC dbo.FunctionalityStatusChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestedProfile.AuditId) +
							", " + ToSQLParameter(data, FunctionalityStatusDataModel.DataColumns.FunctionalityStatusId);

			var oDT = new DBDataSet("FunctionalityStatus.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FunctionalityStatusDataModel data, RequestProfile requestedProfile)
		{
			var isDeletable = true;

			var ds = GetChildren(data, requestedProfile);

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
