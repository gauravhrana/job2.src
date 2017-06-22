using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class DeliverableArtifactStatusDataManager : StandardDataManager
    {
        public static List<DeliverableArtifactStatusDataModel> GetDeliverableArtifactStatusList(RequestProfile requestProfile)
        {
            return GetEntityDetails(DeliverableArtifactStatusDataModel.Empty, requestProfile, 0);
        } 
    }
}
