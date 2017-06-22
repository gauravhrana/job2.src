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
    public partial class ProjectUseCaseStatusDataManager : StandardDataManager
    {
        public static List<ProjectUseCaseStatusDataModel> GetProjectUseCaseStatusList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ProjectUseCaseStatusDataModel.Empty, requestProfile, 0);
        }  

    }
}
