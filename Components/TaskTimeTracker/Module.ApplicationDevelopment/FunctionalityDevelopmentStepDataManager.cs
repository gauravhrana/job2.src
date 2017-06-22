using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;



namespace TaskTimeTracker.Components.Module.ApplicationDevelopment
{
	public partial class FunctionalityDevelopmentStepDataManager : StandardDataManager
	{
        private static readonly string DataStoreKey = "";

		static FunctionalityDevelopmentStepDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityDevelopmentStep");
		}

		#region GetList

        public static List<FunctionalityDevelopmentStepDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityDevelopmentStepDataModel.Empty, requestProfile, 1);
		}

		#endregion GetList

		#region Search

		public static string ToSQLParameter(FunctionalityDevelopmentStepDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId:
					if (data.FunctionalityDevelopmentStepId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId, data.FunctionalityDevelopmentStepId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(FunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

		public static FunctionalityDevelopmentStepDataModel GetDetails(FunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile);

			return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<FunctionalityDevelopmentStepDataModel> GetEntityDetails(FunctionalityDevelopmentStepDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.FunctionalityDevelopmentStepSearch ";

			var parameters =
			new
			{
					AuditId = requestProfile.AuditId
				,	ApplicationId = requestProfile.ApplicationId
				,	ApplicationMode = requestProfile.ApplicationModeId
				,	Name = dataQuery.Name
				,	FunctionalityDevelopmentStepId = dataQuery.FunctionalityDevelopmentStepId
				,	ReturnAuditInfo = returnAuditInfo
			};

			List<FunctionalityDevelopmentStepDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityDevelopmentStepDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}


			return result;
		}

		#endregion

		#region ToList

		static private List<FunctionalityDevelopmentStepDataModel> ToList(DataTable dt)
		{
			var list = new List<FunctionalityDevelopmentStepDataModel>();

			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					var dataItem = new FunctionalityDevelopmentStepDataModel();

					dataItem.FunctionalityDevelopmentStepId = (int?)dr[FunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId];
					dataItem.DateCreated = (DateTime)dr[FunctionalityDevelopmentStepDataModel.DataColumns.DateCreated];
					dataItem.DateModified = (DateTime)dr[FunctionalityDevelopmentStepDataModel.DataColumns.DateModified];
					dataItem.CreatedByAuditId = (int?)dr[BaseDataModel.BaseDataColumns.CreatedByAuditId];
					dataItem.ModifiedByAuditId = (int?)dr[BaseDataModel.BaseDataColumns.ModifiedByAuditId];

					SetStandardInfo(dataItem, dr);

					list.Add(dataItem);
				}
			}
			return list;
		}

		#endregion

		#region GetEntitySearch

		static public List<FunctionalityDevelopmentStepDataModel> GetEntitySearch(FunctionalityDevelopmentStepDataModel obj, RequestProfile requestProfile)
		{
			var dt = Search(obj, requestProfile);

			var list = ToList(dt);

			return list;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(FunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityDevelopmentStepInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FunctionalityDevelopmentStepUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, FunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(FunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			DBDML.RunSQL("FunctionalityDevelopmentStep.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(FunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("FunctionalityDevelopmentStep.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityDevelopmentStepRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("FunctionalityDevelopmentStep.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(FunctionalityDevelopmentStepDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FunctionalityDevelopmentStepDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				FunctionalityDevelopmentStepId = dataQuery.FunctionalityDevelopmentStepId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion Delete

		#region DoesExist

		public static bool DoesExist(FunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FunctionalityDevelopmentStepDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

            return list.Count > 0;
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(FunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityDevelopmentStepChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, FunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId);

			var oDT = new DBDataSet("FunctionalityDevelopmentStep.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(FunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityDevelopmentStepChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, FunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId);

			var oDT = new DBDataSet("FunctionalityDevelopmentStep.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
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
