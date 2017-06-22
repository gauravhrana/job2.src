using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis
{
    public partial class UseCaseDataManager : StandardDataManager
    {
        public static List<UseCaseDataModel> GetUseCaseList(RequestProfile requestProfile)
        {
            return GetEntityDetails(UseCaseDataModel.Empty, requestProfile, 0);
        }        

        #region Get By UseCaseXUseCaseStep

        public static DataTable GetByUseCaseXUseCaseStep(int useCaseXUseCaseStepId, int auditId)
        {
            var sql = "EXEC UseCaseSearch @UseCaseId     =" + useCaseXUseCaseStepId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

        #region Get By UseCaseActorXUseCase

        public static DataTable GetByUseCaseActorXUseCase(int useCaseActorXUseCaseId, int auditId)
        {
            var sql = "EXEC UseCaseSearch @UseCaseId     =" + useCaseActorXUseCaseId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

    }
}
