using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker.Competency;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.Module.Competency
{
    public partial class CompetencyDataManager : StandardDataManager
    {
        public static List<CompetencyDataModel> GetCompetencyList(RequestProfile requestProfile)
        {
            return GetEntityDetails(CompetencyDataModel.Empty, requestProfile, 0);
        }       

    }
}

