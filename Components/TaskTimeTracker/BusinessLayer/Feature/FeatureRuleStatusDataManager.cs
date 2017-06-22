using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;
using DataModel.TaskTimeTracker.Feature;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Feature
{
    public partial class FeatureRuleStatusDataManager : StandardDataManager
    {
        public static List<FeatureRuleStatusDataModel> GetFeatureRuleStatusList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FeatureRuleStatusDataModel.Empty, requestProfile, 0);
        } 
    }
}
