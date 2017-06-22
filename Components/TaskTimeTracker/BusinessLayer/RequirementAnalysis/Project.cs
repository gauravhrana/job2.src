using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
	public partial class ProjectDataManager : StandardDataManager
	{
		private static string DataStoreKey = "";
		
		static ProjectDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Project");			
		}

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ProjectSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                 ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
			var oDT = new DBDataTable("Project.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		public static List<ProjectDataModel> GetProjectList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ProjectSearch" +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var result = new List<ProjectDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new ProjectDataModel();

					dataItem.ProjectId = (int)dbReader[ProjectDataModel.DataColumns.ProjectId];
					dataItem.ApplicationId = (int)dbReader[BaseDataModel.BaseDataColumns.ApplicationId];

					SetStandardInfo(dataItem, dbReader);

					SetStandardInfo(dataItem, dbReader);

					result.Add(dataItem);
				}

			}

			return result;
		}

		#endregion GetList

		#region Search

		public static string ToSQLParameter(ProjectDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ProjectDataModel.DataColumns.ProjectId:
					if (data.ProjectId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectDataModel.DataColumns.ProjectId, data.ProjectId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectDataModel.DataColumns.ProjectId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        public static DataTable Search(ProjectDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL  
			var sql = "EXEC dbo.ProjectSearch " +
                 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, ProjectDataModel.DataColumns.ProjectId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description);

			var oDT = new DBDataTable("Project.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion Search
        
		#region GetDetails

        public static DataTable GetDetails(ProjectDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
        }

        public static List<ProjectDataModel> GetEntityDetails(ProjectDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.ProjectSearch ";

            var parameters =
            new
            {
                    AuditId              = requestProfile.AuditId
                ,   ApplicationId        = requestProfile.ApplicationId
                ,   ReturnAuditInfo      = returnAuditInfo
                ,   ProjectId            = dataQuery.ProjectId
                ,   Name                 = dataQuery.Name
                ,   Description          = dataQuery.Description
            };

            List<ProjectDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                result = dataAccess.Connection.Query<ProjectDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();

                
            }

            return result;
        }

        //public static List<ProjecDataModel> GetEntityDetails(ProjecDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.ProjectSearch " +
        //        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId) +
        //        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
        //        ", " + ToSQLParameter(data, ProjecDataModel.DataColumns.ProjectId) +
        //        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description);

        //    var result = new List<ProjecDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new ProjecDataModel();

        //            dataItem.ProjectId = (int)dbReader[ProjecDataModel.DataColumns.ProjectId];

        //            SetStandardInfo(dataItem, dbReader);

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
        //}

		#endregion GetDetails

		#region CreateOrUpdate

		private static string CreateOrUpdate(ProjectDataModel data, RequestProfile requestProfile, string action)
		{
            var traceId = TraceDataManager.GetNextTraceId(requestProfile);
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ProjectInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ProjectUpdate  " + "\r\n" +
                           " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId);	
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, ProjectDataModel.DataColumns.ProjectId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create

        public static int Create(ProjectDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var projectId = DBDML.RunScalarSQL("Project.Insert", sql, DataStoreKey);
            return Convert.ToInt32(projectId);
        }
		#endregion Create

		#region Update

        public static void Update(ProjectDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("Project.Update", sql, DataStoreKey);
        }
		#endregion Update		

		#region Delete

        public static void Delete(ProjectDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.ProjectDelete ";

            var parameters =
            new
            {
                    AuditId      = requestProfile.AuditId
                ,   ProjectId    = dataQuery.ProjectId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

                
            }
        }

		#endregion Delete

		#region DoesExist

        public static DataTable DoesExist(ProjectDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ProjectSearch " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							", " + ToSQLParameter(data, ProjectDataModel.DataColumns.ProjectId) +
							", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

			var oDT = new DBDataTable("Project.DoesExist", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion DoesExist

		#region GetChildren

        private static DataSet GetChildren(ProjectDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ProjectChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ProjectDataModel.DataColumns.ProjectId);

			var oDT = new DBDataSet("Project.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

        public static DataSet DeleteChildren(ProjectDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ProjectChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ProjectDataModel.DataColumns.ProjectId);

			var oDT = new DBDataSet("Project.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

        public static bool IsDeletable(ProjectDataModel data, RequestProfile requestProfile)
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

		#region Renumber
        public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ProjectRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("Project.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber
	}
}
