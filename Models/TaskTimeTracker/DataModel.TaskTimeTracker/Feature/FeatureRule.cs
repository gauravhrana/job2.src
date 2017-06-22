using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker.Feature
{
    [Serializable]
    [Table("FeatureRule")]
	public class FeatureRuleDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string FeatureRuleId		  = "FeatureRuleId";			
			public const string FeatureRuleCategoryId = "FeatureRuleCategoryId";			
		}

        public static readonly FeatureRuleDataModel Empty = new FeatureRuleDataModel();
	
        [Key]
		public int?		 FeatureRuleId			{ get; set; }		
		public int?		 FeatureRuleCategoryId	{ get; set; }		

	}
}
