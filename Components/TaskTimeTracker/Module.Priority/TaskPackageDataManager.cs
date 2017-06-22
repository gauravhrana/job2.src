using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker.Priority;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.Module.Priority
{
    public partial class TaskPackageDataManager : StandardDataManager
    {
        public static List<TaskPackageDataModel> GetTaskPackageList(RequestProfile requestProfile)
        {
            return GetEntityDetails(TaskPackageDataModel.Empty, requestProfile, 0);
        } 
   }
}
