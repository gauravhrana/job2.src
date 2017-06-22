using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{
    [Serializable]
    public class ProjectXUseCaseDataModel : BaseDataModel
    {
        public class DataColumns
        {
            public const string ProjectXUseCaseId       = "ProjectXUseCaseId";
            public const string ProjectId               = "ProjectId";
            public const string UseCaseId               = "UseCaseId";
            public const string ProjectUseCaseStatusId  = "ProjectUseCaseStatusId";

            public const string Project                 = "Project";
            public const string UseCase                 = "UseCase";
            public const string ProjectUseCaseStatus    = "ProjectUseCaseStatus";
        }

        public static readonly ProjectXUseCaseDataModel Empty = new ProjectXUseCaseDataModel();

        public int? ProjectXUseCaseId               { get; set; }
        public int? ProjectId                       { get; set; }
        public int? UseCaseId                       { get; set; }
        public int? ProjectUseCaseStatusId          { get; set; }

        public string Project                       { get; set; }
        public string UseCase                       { get; set; }
        public string ProjectUseCaseStatus          { get; set; }
       
    }
}


