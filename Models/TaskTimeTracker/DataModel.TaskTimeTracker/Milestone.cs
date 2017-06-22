using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker
{
    [Serializable]
    [Table("Milestone")]
	public class MilestoneDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string MilestoneId = "MilestoneId";
			public const string ProjectId = "ProjectId";
			public const string Project = "Project";
		}

        public static readonly MilestoneDataModel Empty = new MilestoneDataModel();

		[Key]
		public int? MilestoneId { get; set; }

		public int? ProjectId { get; set; }
		public string Project { get; set; }
		
	}
}
