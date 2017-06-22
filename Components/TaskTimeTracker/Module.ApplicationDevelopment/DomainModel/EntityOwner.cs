using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class EntityOwnerDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string EntityOwnerId        = "EntityOwnerId";
            public const string EntityId             = "EntityId";
            public const string DeveloperRoleId      = "DeveloperRoleId";
            public const string Developer            = "Developer";
            public const string FeatureOwnerStatusId = "FeatureOwnerStatusId";
            public const string Application = "Application";

            public const string Entity               = "Entity";
            public const string DeveloperRole        = "DeveloperRole";
            public const string FeatureOwnerStatus   = "FeatureOwnerStatus";
        }

		public static readonly EntityOwnerDataModel Empty = new EntityOwnerDataModel();

        public int? EntityOwnerId           { get; set; }
        public int? EntityId                { get; set; }
        public int? DeveloperRoleId         { get; set; }
        public string Developer             { get; set; }
        public int? FeatureOwnerStatusId    { get; set; }
        public string Application { get; set; }

        public string Entity                { get; set; }
        public string DeveloperRole         { get; set; }
        public string FeatureOwnerStatus    { get; set; }

    }
}
