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
	public partial class FunctionalityXFunctionalityDevelopmentStepDataManager : BaseDataManager
	{
        private static readonly string DataStoreKey = "";
		
		static FunctionalityXFunctionalityDevelopmentStepDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("FunctionalityXFunctionalityDevelopmentStep");
		}

		#region GetList

        public static List<FunctionalityXFunctionalityDevelopmentStepDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FunctionalityXFunctionalityDevelopmentStepDataModel.Empty, requestProfile, 0);
		}

		#endregion GetList

		#region Search

		public static string ToSQLParameter(FunctionalityXFunctionalityDevelopmentStepDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityXFunctionalityDevelopmentStepId:
					if (data.FunctionalityXFunctionalityDevelopmentStepId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityXFunctionalityDevelopmentStepId, data.FunctionalityXFunctionalityDevelopmentStepId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityXFunctionalityDevelopmentStepId);
					}
					break;

				case FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityId:
					if (data.FunctionalityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityId, data.FunctionalityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityId);
					}
					break;

				case FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId:
					if (data.FunctionalityDevelopmentStepId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId, data.FunctionalityDevelopmentStepId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId);
					}
					break;

				case FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.SortOrder);
					}
					break;

				case FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.Version:
					if (data.Version != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.Version, data.Version);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.Version);
					}
					break;
				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        public static DataTable Search(FunctionalityXFunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion Search

		#region GetDetails

        public static FunctionalityXFunctionalityDevelopmentStepDataModel GetDetails(FunctionalityXFunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion 

		#region GetEntitySearch

		public static List<FunctionalityXFunctionalityDevelopmentStepDataModel> GetEntityDetails(FunctionalityXFunctionalityDevelopmentStepDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{

			const string sql = @"dbo.FunctionalityXFunctionalityDevelopmentStepSearch ";

			var parameters =
			new
			{
					AuditId											= requestProfile.AuditId 
				,	ApplicationId									= requestProfile.ApplicationId
				,	ApplicationMode									= requestProfile.ApplicationModeId
				,	FunctionalityXFunctionalityDevelopmentStepId	= dataQuery.FunctionalityXFunctionalityDevelopmentStepId
				,	FunctionalityId									= dataQuery.FunctionalityId
				,	FunctionalityDevelopmentStepId					= dataQuery.FunctionalityDevelopmentStepId
				,	ReturnAuditInfo									= returnAuditInfo
			};

			List<FunctionalityXFunctionalityDevelopmentStepDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<FunctionalityXFunctionalityDevelopmentStepDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion GetDetails

		#region ToList

		static private List<FunctionalityXFunctionalityDevelopmentStepDataModel> ToList(DataTable dt)
		{
			var list = new List<FunctionalityXFunctionalityDevelopmentStepDataModel>();

			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					var dataItem = new FunctionalityXFunctionalityDevelopmentStepDataModel();

					dataItem.FunctionalityXFunctionalityDevelopmentStepId = (int?)dr[FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityXFunctionalityDevelopmentStepId];
					dataItem.FunctionalityId=  (int?)dr[FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityId];
					dataItem.FunctionalityDevelopmentStepId = (int?)dr[FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId];
					dataItem.Functionality = (string)dr[FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.Functionality];
					dataItem.FunctionalityDevelopmentStep = (string)dr[FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStep];
					dataItem.SortOrder = (int?)dr[FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.SortOrder];
					dataItem.Version = (string)dr[FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.Version];	

					list.Add(dataItem);
				}
			}
			return list;
		}

		#endregion

		#region GetEntitySearch

		static public List<FunctionalityXFunctionalityDevelopmentStepDataModel> GetEntitySearch(FunctionalityXFunctionalityDevelopmentStepDataModel obj, RequestProfile requestProfile)
		{
			var dt = Search(obj, requestProfile);

			var list = ToList(dt);

			return list;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(FunctionalityXFunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.FunctionalityXFunctionalityDevelopmentStepInsert  " + "\r\n" +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.FunctionalityXFunctionalityDevelopmentStepUpdate  " + "\r\n" +
						   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityXFunctionalityDevelopmentStepId) +
						", " + ToSQLParameter(data, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityId) +
						", " + ToSQLParameter(data, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityDevelopmentStepId) +
						", " + ToSQLParameter(data, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.Version) +
						", " + ToSQLParameter(data, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

		public static void Create(FunctionalityXFunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");

			DBDML.RunSQL("FunctionalityXFunctionalityDevelopmentStep.Insert", sql, DataStoreKey);

		}

		#endregion Create

		#region Update

		public static void Update(FunctionalityXFunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");

			DBDML.RunSQL("FunctionalityXFunctionalityDevelopmentStep.Update", sql, DataStoreKey);
		}
		#endregion Update

		#region Renumber
		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityXFunctionalityDevelopmentStepRenumber " +
					  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("FunctionalityXFunctionalityDevelopmentStep.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

		public static void Delete(FunctionalityXFunctionalityDevelopmentStepDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.FunctionalityXFunctionalityDevelopmentStepDelete ";

			var parameters =
			new
			{
					AuditId											= requestProfile.AuditId
				,	FunctionalityXFunctionalityDevelopmentStepId	= dataQuery.FunctionalityXFunctionalityDevelopmentStepId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
		}

		#endregion Delete

		#region DoesExist

		public static bool DoesExist(FunctionalityXFunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new FunctionalityXFunctionalityDevelopmentStepDataModel();
			doesExistRequest.FunctionalityDevelopmentStep = data.FunctionalityDevelopmentStep;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);

            return list.Count > 0;
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(FunctionalityXFunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityXFunctionalityDevelopmentStepChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityXFunctionalityDevelopmentStepId);

			var oDT = new DBDataSet("FunctionalityXFunctionalityDevelopmentStep.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

		public static DataSet DeleteChildren(FunctionalityXFunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.FunctionalityXFunctionalityDevelopmentStepChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, FunctionalityXFunctionalityDevelopmentStepDataModel.DataColumns.FunctionalityXFunctionalityDevelopmentStepId);

			var oDT = new DBDataSet("FunctionalityXFunctionalityDevelopmentStep.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(FunctionalityXFunctionalityDevelopmentStepDataModel data, RequestProfile requestProfile)
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
