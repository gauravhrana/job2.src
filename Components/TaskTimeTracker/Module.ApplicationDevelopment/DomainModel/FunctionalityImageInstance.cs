using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
    public class FunctionalityImageInstanceDataModel : StandardDataModel 
    {
        public class DataColumns
        {
            public const string FunctionalityImageInstanceId = "FunctionalityImageInstanceId";
            public const string FunctionalityImageId = "FunctionalityImageId";
            public const string FunctionalityImage = "FunctionalityImage";
            public const string FunctionalityImageAttributeId = "FunctionalityImageAttributeId";
            public const string FunctionalityImageAttribute = "FunctionalityImageAttribute";
        }

		public static readonly FunctionalityImageInstanceDataModel Empty = new FunctionalityImageInstanceDataModel();

        public int? FunctionalityImageInstanceId { get; set; }
        public int? FunctionalityImageId { get; set; }
        public string FunctionalityImage { get; set; }
        public int? FunctionalityImageAttributeId { get; set; }
        public string FunctionalityImageAttribute { get; set; }

    }
}
