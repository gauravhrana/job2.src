using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker.Task
{
    [Serializable]
    [Table("TaskFormulation")]
	public class TaskFormulationDataModel : BaseDataModel
	{
		public class DataColumns : BaseDataColumns
		{
			public const string TaskFormulationId		= "TaskFormulationId";
			public const string ProjectId				= "ProjectId";
			public const string TaskId				    = "TaskId";			
            public const string FeatureId               = "FeatureId";
			public const string ProjectName			    = "ProjectName";
			public const string TaskName				= "TaskName";
			public const string FeatureName			    = "FeatureName";
			public const string SortOrder				= "SortOrder";
		}

        public static readonly TaskFormulationDataModel Empty = new TaskFormulationDataModel();

            [Key]
			public int?		TaskFormulationId	 { get; set; }
			public int?		ProjectId			 { get; set; }
			public int?		TaskId				 { get; set; }
			public int?		SortOrder			 { get; set; }
            public int?		FeatureId			 { get; set; }
			public string	ProjectName			 { get; set; }
			public string	TaskName			 { get; set; }
			public string	FeatureName			 { get; set; }

	}
}


