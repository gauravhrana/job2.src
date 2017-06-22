using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{

	[Serializable]
	public partial class NeedDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string NeedId = "NeedId";
		}

		public static readonly NeedDataModel Empty = new NeedDataModel();

		public int? NeedId { get; set; }

	}
}
