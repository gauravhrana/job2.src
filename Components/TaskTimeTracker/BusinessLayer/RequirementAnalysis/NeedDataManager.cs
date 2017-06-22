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
    public partial class NeedDataManager : StandardDataManager
    {
        public static List<NeedDataModel> GetNeedList(RequestProfile requestProfile)
        {
            return GetEntityDetails(NeedDataModel.Empty, requestProfile, 0);
        } 
    }
}
