using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Dapper;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
	public partial class ScheduleStateDataManager : StandardDataManager
	{		

		public static List<ScheduleStateDataModel> GetScheduleStateList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ScheduleStateDataModel.Empty, requestProfile, 0);
		}

		public static int CreateCSVData(ScheduleStateDataModel data, RequestProfile requestProfile,string dataStoreKey)
		{
			var sql = Save(data, "Create", requestProfile);
			var newId = DBDML.RunScalarSQL("ScheduleState.Insert", sql, dataStoreKey);
			return Convert.ToInt32(newId);
		}
		
	}
}
