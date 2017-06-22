using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Feature
{
	public partial class FeatureGroupXFeatureDataModel : BaseDataModel
    {
		public class DataColumns : BaseDataColumns
        {
            public const string FeatureGroupXFeatureId = "FeatureGroupXFeatureId";
            public const string FeatureId              = "FeatureId";
            public const string FeatureGroupId         = "FeatureGroupId";

            public const string Feature                = "Feature";
            public const string FeatureGroup           = "FeatureGroup";
        }

        public int? FeatureGroupXFeatureId	{ get; set; }
        public int? FeatureId				{ get; set; }
        public int? FeatureGroupId			{ get; set; }

        public string Feature				{ get; set; }
        public string FeatureGroup			{ get; set; }


        public string ToURLQuery()
        {
            return String.Empty; 
        }                  
    }
}
