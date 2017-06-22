using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class ModuleOwnerDataModel : StandardDataModel
    {
        public class DataColumns : BaseDataColumns
        {
            public const string ModuleOwnerId = "ModuleOwnerId";
            public const string ModuleId = "ModuleId";
            public const string DeveloperRoleId = "DeveloperRoleId";
            public const string Developer = "Developer";
            public const string Application = "Application";
            public const string FeatureOwnerStatusId = "FeatureOwnerStatusId";

            public const string Module = "Module";
            public const string DeveloperRole = "DeveloperRole";
            public const string FeatureOwnerStatus = "FeatureOwnerStatus";
			public const string TotalHoursWorked = "TotalHoursWorked";
			public const int ModuleOwnerTimeSpentConstant = 1;

        }

		public static readonly ModuleOwnerDataModel Empty = new ModuleOwnerDataModel();

        public int? ModuleOwnerId { get; set; }
        public int? ModuleId { get; set; }
        public int? DeveloperRoleId { get; set; }
        public string Developer { get; set; }
        public int? FeatureOwnerStatusId { get; set; }
		public int? TotalHoursWorked { get; set; }
        public string Application { get; set; }  

        public string Module { get; set; }
        public string DeveloperRole { get; set; }
        public string FeatureOwnerStatus { get; set; }

    }
}
