using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Feature
{
	public class FeatureRuleDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string FeatureRuleId		  = "FeatureRuleId";			
			public const string FeatureRuleCategoryId = "FeatureRuleCategoryId";			
		}
	
		public int?		 FeatureRuleId			{ get; set; }		
		public int?		 FeatureRuleCategoryId	{ get; set; }		

		public string ToURLQuery()
		{
			return String.Empty; //"FeatureRuleId=" + FeatureRuleId
		}
	}
}
