using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class FunctionalityOwnerDataModel : StandardDataModel
    {
		public class DataColumns : BaseDataColumns
        {
            public const string FunctionalityOwnerId = "FunctionalityOwnerId";
            public const string FunctionalityId = "FunctionalityId";
            public const string DeveloperRoleId = "DeveloperRoleId";
            public const string Developer = "Developer";
            public const string FeatureOwnerStatusId = "FeatureOwnerStatusId";
            public const string Application = "Application";

            public const string Functionality = "Functionality";
            public const string DeveloperRole = "DeveloperRole";
            public const string FeatureOwnerStatus = "FeatureOwnerStatus";

        }

		public static readonly FunctionalityOwnerDataModel Empty = new FunctionalityOwnerDataModel();

        public int? FunctionalityOwnerId { get; set; }
        public int? FunctionalityId { get; set; }
        public int? DeveloperRoleId { get; set; }
        public string Developer { get; set; }
        public int? FeatureOwnerStatusId { get; set; }
        public string Application { get; set; }

        public string Functionality { get; set; }
        public string DeveloperRole { get; set; }
        public string FeatureOwnerStatus { get; set; }

    }
}
