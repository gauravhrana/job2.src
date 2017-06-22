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
	public partial class ScheduleDetailActivityCategoryDataManager : StandardDataManager
	{		

		#region Add Master Data

		public static void AddInitialData(int applicationId, RequestProfile requestProfile)
		{
			var listRecords = new List<string>() { 
								"Analysis",
								"Documentation",
								"Design",
								"Development",
								"Research",
								"Deployment",
								"Quality Assurance",
								"Human Resource",
								"Data Entry"
								};

			var newRequestProfile = requestProfile;
			newRequestProfile.ApplicationId = applicationId;

			foreach (var value in listRecords)
			{
				var obj = new ScheduleDetailActivityCategoryDataModel();
				obj.Name = value;

				if(!DoesExist(obj, newRequestProfile))
				{
					obj.Description = value;
					obj.SortOrder = listRecords.IndexOf(value) + 1;

					Create(obj, newRequestProfile);
				}
			}

		}

		#endregion

	}
}
