using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{
	public class ProjecDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ProjectId = "ProjectId";
		}

		public int? ProjectId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"ProjectId=" + ProjectId
		}
	}
}
