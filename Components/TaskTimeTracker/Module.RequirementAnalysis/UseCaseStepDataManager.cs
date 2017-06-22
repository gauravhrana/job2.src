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
    public partial class UseCaseStepDataManager : StandardDataManager
    {
        #region Get By UseCase

        public static DataTable GetByUseCase(int UseCaseId, int auditId)
        {
            var sql = "EXEC UseCaseXUseCaseStepSearch @UseCaseId       =" + UseCaseId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion
    }
}
