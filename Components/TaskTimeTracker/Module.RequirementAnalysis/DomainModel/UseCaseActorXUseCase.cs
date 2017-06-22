using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{
    [Serializable]
    public class UseCaseActorXUseCaseDataModel : BaseDataModel
    {
        public class DataColumns
        {
            public const string UseCaseActorXUseCaseId   = "UseCaseActorXUseCaseId";
            public const string UseCaseActorId           = "UseCaseActorId";
            public const string UseCaseId                = "UseCaseId";
            public const string UseCaseRelationshipId    = "UseCaseRelationshipId";

            public const string UseCaseActor             = "UseCaseActor";
            public const string UseCase                  = "UseCase";
            public const string UseCaseRelationship      = "UseCaseRelationship";
        }

        public static readonly UseCaseActorXUseCaseDataModel Empty = new UseCaseActorXUseCaseDataModel();

        public int?     UseCaseActorXUseCaseId      { get; set; }
        public int?     UseCaseActorId              { get; set; }
        public int?     UseCaseId                   { get; set; }
        public int?     UseCaseRelationshipId       { get; set; }

        public string   UseCaseActor                { get; set; }
        public string   UseCase                     { get; set; }
        public string   UseCaseRelationship         { get; set; }
      
    }
}


