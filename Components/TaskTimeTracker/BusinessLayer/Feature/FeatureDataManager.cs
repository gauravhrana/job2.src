using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using DataModel.TaskTimeTracker.Feature;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Feature
{
    public partial class FeatureDataManager : StandardDataManager
    {
        public static List<FeatureDataModel> GetFeatureList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FeatureDataModel.Empty, requestProfile, 0);
        } 

    }
}
