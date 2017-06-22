using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.ReleaseLog;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;

namespace Framework.Components.ReleaseLog
{
	public class ReleaseNotesWorkFlow
	{
		
		// What that will do is take a ReleaseLogDetailID clone it and save it with different applicationID. And Delete the original RelaseLogDetail.
		// Should debug if debug should be real if real.
		public static void MoveToApplication(int releaseLogDetailId, int applicationId, RequestProfile requestProfile)
		{
			var dataItem = new ReleaseLogDetailDataModel();
			dataItem.ReleaseLogDetailId = releaseLogDetailId;

			var oDetail = ReleaseLogDetailDataManager.GetDetails(dataItem, requestProfile);
            if (oDetail != null)
			{
				//copy data from old record
				dataItem.ApplicationId					= oDetail.ApplicationId;
				dataItem.Application					= oDetail.Application;
				dataItem.ReleaseLog						= oDetail.ReleaseLog;
				dataItem.ReleaseLogId					= oDetail.ReleaseLogId;
				dataItem.ItemNo							= oDetail.ItemNo;
				dataItem.Description					= oDetail.Description;
				dataItem.RequestedBy					= oDetail.RequestedBy;
				dataItem.RequestedDate					= oDetail.ReleaseDate;
				dataItem.PrimaryDeveloper				= oDetail.PrimaryDeveloper;
				dataItem.SortOrder						= oDetail.SortOrder;
				dataItem.ReleaseIssueTypeId				= oDetail.ReleaseIssueTypeId;
				dataItem.ReleasePublishCategoryId		= oDetail.ReleasePublishCategoryId;
				dataItem.ReleaseIssueType				= oDetail.ReleaseIssueType;
				dataItem.ReleasePublishCategory			= oDetail.ReleasePublishCategory;
				dataItem.Feature						= oDetail.Feature;
				dataItem.Module							= oDetail.Module;
				dataItem.ModuleId						= oDetail.ModuleId;
				dataItem.JIRA							= oDetail.JIRA;
				dataItem.PrimaryEntity					= oDetail.PrimaryEntity;
				dataItem.TimeSpent						= oDetail.TimeSpent;							


				//delete old record
				ReleaseLogDetailDataManager.Delete(dataItem, requestProfile);

				// change application id
				dataItem.ApplicationId = applicationId;


				// ReleaseLogDetailId will remain same, only ApplicationId will change
				ReleaseLogDetailDataManager.Save(dataItem, requestProfile, "Insert");
			}

		}

	}
}
