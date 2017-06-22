using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;


namespace DataModel.TaskTimeTracker.Feature
{
	public class RunTimeFeatureDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string RunTimeFeatureId = "RunTimeFeatureId";			
		}
	
		public int? RunTimeFeatureId { get; set; }		

		public string ToURLQuery()
		{
			return String.Empty; //"RunTimeFeatureId=" + RunTimeFeatureId
		}			
		
	}
}
