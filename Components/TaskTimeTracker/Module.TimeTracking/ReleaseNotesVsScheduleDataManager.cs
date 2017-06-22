using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Components.DataAccess;
//using Framework.Components.ReleaseLog;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
	public partial class ReleaseNotesVsScheduleDataManager: StandardDataManager
	{
		public void CalculateRNandSDWorkedHours(DateTime fromDate, DateTime toDate, string emailAddress, int appId)
		{
			//var fromDateTime = new DateTime();
			//var toDateTime = new DateTime();
			//var dateValue = ReleaseLogDetailDataManager.GetDetails(ReleaseLogDetailDataModel.Empty, SessionVariables.RequestProfile);
			//var dv = dateValue.DefaultView;
			//dv.Sort = "ReleaseDate ASC";
			//if (fromDate.Equals("") && toDate.Equals(""))
			//{
			//	fromDateTime = Convert.ToDateTime(dv.ToTable().Rows[0][ReleaseLogDetailDataModel.DataColumns.ReleaseDate].ToString());
			//	toDateTime = Convert.ToDateTime(dv.ToTable().Rows[dateValue.Rows.Count - 1][ReleaseLogDetailDataModel.DataColumns.ReleaseDate].ToString());
			//}
		}
	}
}
