using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class FunctionalityDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string FunctionalityId              = "FunctionalityId";
            public const string FunctionalityActiveStatusId  = "FunctionalityActiveStatusId";
            public const string FunctionalityActiveStatus    = "FunctionalityActiveStatus";
			public const string Image                        = "Image";
            public const string FunctionalityPriorityId      = "FunctionalityPriorityId";
            public const string FunctionalityPriority        = "FunctionalityPriority";
			public const string NumberOfImages               = "NumberOfImages";
            public const string Application                  = "Application";
		}

		public static readonly FunctionalityDataModel Empty = new FunctionalityDataModel();

		public int?      FunctionalityId             { get; set; }
        public int?      FunctionalityActiveStatusId { get; set; }
        public string    FunctionalityActiveStatus   { get; set; }
		public byte[]    Image                       { get; set; }		
        public int?      FunctionalityPriorityId     { get; set; }
		public int?      NumberOfImages              { get; set; }
        public string    FunctionalityPriority       { get; set; }
        public string    Application                 { get; set; }

	}
}
