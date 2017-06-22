using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Feature
{
	public class FeatureDataModel : StandardDataModel
    {
		public class DataColumns : StandardDataColumns
        {
            public const string FeatureId		= "FeatureId";            
        }

        public int?		FeatureId		{ get; set; }		

        public string ToURLQuery()
        {
            return String.Empty; //"FeatureId=" + FeatureId
        }        
    }
}
