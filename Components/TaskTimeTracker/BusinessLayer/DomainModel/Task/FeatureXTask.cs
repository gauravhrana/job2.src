using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Task
{
	public class FeatureXTaskDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string FeatureXTaskId	= "FeatureXTaskId";
			public const string TaskId			= "TaskId";
			public const string FeatureId		= "FeatureId";

			public const string Task			= "Task";
			public const string Feature			= "Feature";
		}

		public int? FeatureXTaskId				{ get; set; }
		public int? TaskId						{ get; set; }
		public int? FeatureId					{ get; set; }

		public string Task						{ get; set; }
		public string Feature					{ get; set; }


		public string ToURLQuery()
		{
			return String.Empty; 
		}
	}
}
