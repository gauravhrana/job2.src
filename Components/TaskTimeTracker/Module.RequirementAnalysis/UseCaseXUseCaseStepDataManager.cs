using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;
using DataModel.TaskTimeTracker;

namespace TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis
{
    public partial class UseCaseXUseCaseStepDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static UseCaseXUseCaseStepDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("UseCaseXUseCaseStep");
        }

        #region GetList

        public static List<UseCaseXUseCaseStepDataModel> GetList(RequestProfile requestProfile)
		{
            return GetEntityDetails(UseCaseXUseCaseStepDataModel.Empty, requestProfile, 0);
		}

        #endregion

        #region GetDetails

        public static UseCaseXUseCaseStepDataModel GetDetails(UseCaseXUseCaseStepDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

		public static List<UseCaseXUseCaseStepDataModel> GetEntityDetails(UseCaseXUseCaseStepDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.UseCaseXUseCaseStepSearch ";

			var parameters =
			new
			{
					AuditId					  = requestProfile.AuditId
				,	ApplicationId			  = requestProfile.ApplicationId
				,	ReturnAuditInfo			  = returnAuditInfo
				,	UseCaseId		 		  = dataQuery.UseCaseId
				,	UseCaseStepId			  = dataQuery.UseCaseStepId
				,	UseCaseWorkFlowCategoryId = dataQuery.UseCaseWorkFlowCategoryId
				,	UseCaseXUseCaseStepId	  = dataQuery.UseCaseXUseCaseStepId

			};

			List<UseCaseXUseCaseStepDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<UseCaseXUseCaseStepDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

        #endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(UseCaseXUseCaseStepDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.UseCaseXUseCaseStepInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.UseCaseXUseCaseStepUpdate  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId) +
                ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStepId) +
                ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseId) +
                ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategoryId);

            return sql;

        }
        #endregion CreateOrUpdate

        #region Create

        public static void Create(UseCaseXUseCaseStepDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("UseCaseXUseCaseStep.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update
        public static void Update(UseCaseXUseCaseStepDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");

            Framework.Components.DataAccess.DBDML.RunSQL("UseCaseXUseCaseStep.Update", sql, DataStoreKey);
        }
        #endregion Update

        #region Delete

        public static string ToSQLParameter(UseCaseXUseCaseStepDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId:
                    if (data.UseCaseXUseCaseStepId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId, data.UseCaseXUseCaseStepId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId);
                    }
                    break;

                case UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStepId:
                    if (data.UseCaseStepId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStepId, data.UseCaseStepId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStepId);
                    }
                    break;

                case UseCaseXUseCaseStepDataModel.DataColumns.UseCaseId:
                    if (data.UseCaseId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseId, data.UseCaseId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseId);
                    }
                    break;

                case UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategoryId:
                    if (data.UseCaseWorkFlowCategoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategoryId, data.UseCaseWorkFlowCategoryId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategoryId);
                    }
                    break;

                case UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStep:
                    if (data.UseCaseStep != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStep, data.UseCaseStep);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStep);
                    }
                    break;

                case UseCaseXUseCaseStepDataModel.DataColumns.UseCase:
                    if (data.UseCase != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UseCaseXUseCaseStepDataModel.DataColumns.UseCase, data.UseCase);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseXUseCaseStepDataModel.DataColumns.UseCase);
                    }
                    break;

                case UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategory:
                    if (data.UseCaseWorkFlowCategory != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategory, data.UseCaseWorkFlowCategory);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategory);
                    }
                    break;
                default:
                    returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static void Delete(UseCaseXUseCaseStepDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.UseCaseXUseCaseStepDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId) +
                ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseId) +
                ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategoryId) +
                ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStepId);

            Framework.Components.DataAccess.DBDML.RunSQL("UseCaseXUseCaseStep.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static DataTable Search(UseCaseXUseCaseStepDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.UseCaseXUseCaseStepSearch " +
                " " + BaseDataManager.SetCommonParametersForSearch(requestProfile.AuditId, requestProfile.ApplicationId, requestProfile.ApplicationModeId) +
                ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId) +
                ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseId) +
                ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategoryId) +
                ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStepId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("UseCaseXUseCaseStep.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(UseCaseXUseCaseStepDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.UseCaseXUseCaseStepInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.UseCaseXUseCaseStepUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId) +
                        ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseId) +
                        ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategoryId) +
                        ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStepId);

            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(UseCaseXUseCaseStepDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.UseCaseXUseCaseStepSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("UseCaseXUseCaseStep.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion

        #region Create By UseCase

        public static void CreateByUseCase(int useCaseId, int[] useCaseStepIds, RequestProfile requestProfile)
        {
            foreach (int useCaseStepId in useCaseStepIds)
            {
                var sql = "EXEC UseCaseXUseCaseStepInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @UseCaseId					=   " + useCaseId +
                            ",      @UseCaseStepId				=   " + useCaseStepId;

                Framework.Components.DataAccess.DBDML.RunSQL("UseCaseXUseCaseStepInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By UseCaseStep

        public static void CreateByUseCaseStep(int useCaseStepId, int[] useCaseIds, RequestProfile requestProfile)
        {
            foreach (int useCaseId in useCaseIds)
            {
                var sql = "EXEC UseCaseXUseCaseStepInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @UseCaseId					=   " + useCaseId +
                            ",      @UseCaseStepId				=   " + useCaseStepId;
                Framework.Components.DataAccess.DBDML.RunSQL("UseCaseXUseCaseStepInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Get By UseCaseStep

        public static DataTable GetByUseCaseStep(int useCaseStepId, int auditId)
        {
            var sql = "EXEC UseCaseXUseCaseStepSearch @UseCaseStepId     =" + useCaseStepId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Get By UseCase

        public static DataTable GetByUseCase(int useCaseId, int auditId)
        {
            var sql = "EXEC UseCaseXUseCaseStepSearch @UseCaseId       =" + useCaseId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create By UseCase

        public static void Create(int useCaseId, int[] useCaseStepIds, RequestProfile requestProfile)
        {
            foreach (int useCaseStepId in useCaseStepIds)
            {
                var sql = "EXEC UseCaseXUseCaseStepInsert " +
                          "@UseCaseId=" + useCaseId + ", " +
                          "@UseCaseStepId=" + useCaseStepId + ", " +
                          "@ApplicationId=" + requestProfile.ApplicationId + ", " +
                          "@AuditId=" + requestProfile.AuditId;

                Framework.Components.DataAccess.DBDML.RunSQL("UseCaseXUseCaseStep_Insert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Delete By UseCaseStep

        public static void DeleteByUseCaseStep(int useCaseStepId, int auditId)
        {
            var sql = "EXEC UseCaseXUseCaseStepDelete @UseCaseStepId       =" + useCaseStepId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By UseCase

        public static void DeleteByUseCase(int useCaseId, int auditId)
        {
            var sql = "EXEC UseCaseXUseCaseStepDelete @UseCaseId		=" + useCaseId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            Framework.Components.DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

    }
}
