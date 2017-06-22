using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{

	[Serializable]
	public partial class ProjectPortfolioDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ProjectPortfolioId = "ProjectPortfolioId";
		}

		public static readonly ProjectPortfolioDataModel Empty = new ProjectPortfolioDataModel();

		public int? ProjectPortfolioId { get; set; }

	}
}
