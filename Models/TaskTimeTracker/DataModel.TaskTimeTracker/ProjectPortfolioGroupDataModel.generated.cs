using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{

	[Serializable]
	public partial class ProjectPortfolioGroupDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ProjectPortfolioGroupId = "ProjectPortfolioGroupId";
		}

		public static readonly ProjectPortfolioGroupDataModel Empty = new ProjectPortfolioGroupDataModel();

		public int? ProjectPortfolioGroupId { get; set; }

	}
}
