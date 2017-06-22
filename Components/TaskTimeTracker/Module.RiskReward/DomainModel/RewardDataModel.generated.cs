using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RiskReward
{

	[Serializable]
	public partial class RewardDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string RewardId = "RewardId";
		}

		public static readonly RewardDataModel Empty = new RewardDataModel();

		public int? RewardId { get; set; }

	}
}
