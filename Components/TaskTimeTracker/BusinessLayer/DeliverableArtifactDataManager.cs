using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class DeliverableArtifactDataManager : StandardDataManager
    {
        public static List<DeliverableArtifactDataModel> GetDeliverableArtifactList(RequestProfile requestProfile)
        {
            return GetEntityDetails(DeliverableArtifactDataModel.Empty, requestProfile, 0);
        } 

    }
}
