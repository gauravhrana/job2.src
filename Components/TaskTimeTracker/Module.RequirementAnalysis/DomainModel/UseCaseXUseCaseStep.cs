using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{
    [Serializable]
    public class UseCaseXUseCaseStepDataModel : BaseDataModel
    {
        public class DataColumns
        {
            public const string UseCaseXUseCaseStepId     = "UseCaseXUseCaseStepId";
            public const string UseCaseStepId             = "UseCaseStepId";
            public const string UseCaseId                 = "UseCaseId";
            public const string UseCaseWorkFlowCategoryId = "UseCaseWorkFlowCategoryId";

            public const string UseCaseStep                  = "UseCaseStep";
            public const string UseCase                      = "UseCase";
            public const string UseCaseWorkFlowCategory      = "UseCaseWorkFlowCategory";
        }

        public static readonly UseCaseXUseCaseStepDataModel Empty = new UseCaseXUseCaseStepDataModel();
       
        public int? UseCaseXUseCaseStepId                { get; set; }
        public int? UseCaseStepId                        { get; set; }
        public int? UseCaseId                            { get; set; }
        public int? UseCaseWorkFlowCategoryId            { get; set; }

        public string UseCaseStep                        { get; set; }
        public string UseCase                            { get; set; }
        public string UseCaseWorkFlowCategory            { get; set; }   

    }
}


