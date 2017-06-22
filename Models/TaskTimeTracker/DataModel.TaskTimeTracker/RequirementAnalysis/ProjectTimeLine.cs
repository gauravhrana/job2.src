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
    [Table("ProjectTimeLine")]
	public class ProjectTimeLineDataModel : BaseDataModel
	{
		public class DataColumns 
		{
			public const string ProjectTimeLineId   = "ProjectTimeLineId";
			public const string ProjectId           = "ProjectId";
			public const string Project             = "Project";
            public const string StartDate           = "StartDate";
            public const string EndDate             = "EndDate";
		}

        public static readonly ProjectTimeLineDataModel Empty = new ProjectTimeLineDataModel();
		
            [Key]
			public int?         ProjectTimeLineId { get; set; }
			public int?         ProjectId { get; set; }
			public string       Project { get; set; }
            public DateTime?    StartDate { get; set; }
            public DateTime?    EndDate { get; set; }

	}
}
