using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class VacationPlanDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string VacationPlanId			= "VacationPlanId";
			public const string ApplicationUserId		= "ApplicationUserId";
			public const string StartDate				= "StartDate";
			public const string EndDate					= "EndDate";
		}

		public int? VacationPlanId { get; set; }
		public int? ApplicationUserId { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"VacationPlanId=" + VacationPlanId
		}
	}
}
