using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class ProjectTimeLineDataManager : BaseDataManager
    {

        static readonly string DataStoreKey = "";
       
        static ProjectTimeLineDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ProjectTimeLine");
        }

        #region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ProjectTimeLineSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

            var oDT = new DBDataTable("ProjectTimeLine.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Search

        public static string ToSQLParameter(ProjectTimeLineDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";
			switch (dataColumnName)
			{
				case ProjectTimeLineDataModel.DataColumns.ProjectTimeLineId:
					if (data.ProjectTimeLineId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectTimeLineDataModel.DataColumns.ProjectTimeLineId, data.ProjectTimeLineId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectTimeLineDataModel.DataColumns.ProjectTimeLineId);
					}
					break;

				case ProjectTimeLineDataModel.DataColumns.ProjectId:
					if (data.ProjectId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectTimeLineDataModel.DataColumns.ProjectId, data.ProjectId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectTimeLineDataModel.DataColumns.ProjectId);
					}
					break;

				case ProjectTimeLineDataModel.DataColumns.Project:
					if (!string.IsNullOrEmpty(data.Project))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ProjectTimeLineDataModel.DataColumns.Project, data.Project);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectTimeLineDataModel.DataColumns.Project);
					}
					break;

				case ProjectTimeLineDataModel.DataColumns.StartDate:
					if (data.StartDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProjectTimeLineDataModel.DataColumns.StartDate, data.StartDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectTimeLineDataModel.DataColumns.StartDate);
					}
					break;

				case ProjectTimeLineDataModel.DataColumns.EndDate:
					if (data.EndDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ProjectTimeLineDataModel.DataColumns.EndDate, data.EndDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ProjectTimeLineDataModel.DataColumns.EndDate);
					}
					break;
			}

			return returnValue;
		}


        public static DataTable Search(ProjectTimeLineDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.ProjectTimeLineSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                //", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
                ", " + ToSQLParameter(data, ProjectTimeLineDataModel.DataColumns.ProjectTimeLineId) +
                ", " + ToSQLParameter(data, ProjectTimeLineDataModel.DataColumns.ProjectId) +
                ", " + ToSQLParameter(data, ProjectTimeLineDataModel.DataColumns.StartDate) +
                ", " + ToSQLParameter(data, ProjectTimeLineDataModel.DataColumns.EndDate);

            var oDT = new DBDataTable("ProjectTimeLine.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(ProjectTimeLineDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
        }

        static public List<ProjectTimeLineDataModel> GetEntityDetails(ProjectTimeLineDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.ProjectTimeLineSearch ";

            var parameters =
            new
            {
                    AuditId              = requestProfile.AuditId
                ,   ApplicationId        = requestProfile.ApplicationId
                ,   ReturnAuditInfo      = returnAuditInfo
                ,   ProjectTimeLineId    = dataQuery.ProjectTimeLineId

            };

            List<ProjectTimeLineDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                result = dataAccess.Connection.Query<ProjectTimeLineDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();

                
            }

            return result;
        }

        //public static List<ProjectTimeLineDataModel> GetEntityDetails(ProjectTimeLineDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.ProjectTimeLineSearch " +
        //        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //       ", " + ToSQLParameter(data, ProjectTimeLineDataModel.DataColumns.ProjectTimeLineId);

        //    var result = new List<ProjectTimeLineDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new ProjectTimeLineDataModel();

        //            dataItem.ProjectTimeLineId  = (int?)dbReader[ProjectTimeLineDataModel.DataColumns.ProjectTimeLineId];
        //            dataItem.ProjectId          = (int?)dbReader[ProjectTimeLineDataModel.DataColumns.ProjectId];
        //            dataItem.StartDate          = (DateTime)dbReader[ProjectTimeLineDataModel.DataColumns.StartDate];
        //            dataItem.EndDate            = (DateTime)dbReader[ProjectTimeLineDataModel.DataColumns.EndDate];
        //            dataItem.Project            = (string)dbReader[ProjectTimeLineDataModel.DataColumns.Project];

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
        //}

        #endregion

        #region Create

        public static int Create(ProjectTimeLineDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var projectTimeLineId = DBDML.RunScalarSQL("ProjectTimeLine.Insert", sql, DataStoreKey);
            return Convert.ToInt32(projectTimeLineId);
        }
        #endregion Create

        #region Update

        public static void Update(ProjectTimeLineDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("ProjectTimeLine.Update", sql, DataStoreKey);
        }
        #endregion Update	

        #region Delete

        public static void Delete(ProjectTimeLineDataModel dataQuery, int auditId)
        {
            const string sql = @"dbo.ProjectTimeLineDelete ";

            var parameters =
            new
            {
                    AuditId              = auditId
                ,   ProjectTimeLineId    = dataQuery.ProjectTimeLineId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

                
            }
        }

        #endregion      

        #region CreateOrUpdate

        private static string CreateOrUpdate(ProjectTimeLineDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ProjectTimeLineInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) + 
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ProjectTimeLineUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);                        
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data, ProjectTimeLineDataModel.DataColumns.ProjectTimeLineId) +
				", " + ToSQLParameter(data, ProjectTimeLineDataModel.DataColumns.ProjectId) +
				", " + ToSQLParameter(data, ProjectTimeLineDataModel.DataColumns.StartDate) +
				", " + ToSQLParameter(data, ProjectTimeLineDataModel.DataColumns.EndDate);
				
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(ProjectTimeLineDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ProjectTimeLineSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
			", " + ToSQLParameter(data, ProjectTimeLineDataModel.DataColumns.ProjectId) +
			", " + ToSQLParameter(data, ProjectTimeLineDataModel.DataColumns.ProjectTimeLineId);

            var oDT = new DBDataTable("ProjectTimeLine.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

    }
}
