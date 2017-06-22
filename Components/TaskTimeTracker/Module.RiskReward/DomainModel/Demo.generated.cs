using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RiskReward
{

	public partial class DemoDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string DemoId = "DemoId";
		}

		public static readonly DemoDataModel Empty = new DemoDataModel();

		public int? DemoId { get; set; }

	}
}
