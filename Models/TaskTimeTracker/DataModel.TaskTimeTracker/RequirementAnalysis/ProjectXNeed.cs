using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{
    [Serializable]
    [Table("ProjectXNeed")]
	public class ProjectXNeedDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string ProjectXNeedId	= "ProjectXNeedId";
			public const string ProjectId		= "ProjectId";
			public const string NeedId			= "NeedId";
			public const string Project			= "Project";
			public const string Need			= "Need";
		}

        public static readonly ProjectXNeedDataModel Empty = new ProjectXNeedDataModel();

        [Key]
		public int? ProjectXNeedId { get; set; }
		public int? ProjectId { get; set; }
		public int? NeedId { get; set; }
		public string Project { get; set; }
		public string Need { get; set; }

	}
}
