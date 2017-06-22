using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RiskReward
{

	[Serializable]
	public partial class RiskDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string RiskId = "RiskId";
		}

		public static readonly RiskDataModel Empty = new RiskDataModel();

		public int? RiskId { get; set; }

	}
}
