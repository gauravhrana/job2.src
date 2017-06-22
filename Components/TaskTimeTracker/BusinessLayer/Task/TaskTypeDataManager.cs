﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Task
{
    public partial class TaskTypeDataManager : StandardDataManager
    {
        public static List<TaskTypeDataModel> GetTaskTypeList(RequestProfile requestProfile)
        {
            return GetEntityDetails(TaskTypeDataModel.Empty, requestProfile, 0);
        } 
    }
}
