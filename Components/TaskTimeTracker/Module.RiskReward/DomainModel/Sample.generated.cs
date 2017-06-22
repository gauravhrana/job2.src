using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RiskReward
{

	public partial class SampleDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string SampleId = "SampleId";
		}

		public static readonly SampleDataModel Empty = new SampleDataModel();

		public int? SampleId { get; set; }

	}
}
