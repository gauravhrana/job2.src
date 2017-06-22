using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataModel.TaskTimeTracker.TimeTracking
{
    [Serializable]
	public class DailyTaskItemQueueStatusDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string DailyTaskItemQueueStatusId	= "DailyTaskItemQueueStatusId";
			public const string DateCreated					= "DateCreated";
			public const string DateModified				= "DateModified";			
		}

		public static readonly DailyTaskItemQueueStatusDataModel Empty = new DailyTaskItemQueueStatusDataModel();

		public int? DailyTaskItemQueueStatusId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateModified { get; set; }		
	}
}
