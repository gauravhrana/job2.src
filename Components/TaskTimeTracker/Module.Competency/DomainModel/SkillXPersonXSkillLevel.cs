using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Competency
{
    [Serializable]
    public class SkillXPersonXSkillLevelDataModel : BaseDataModel
    {
        public class DataColumns
        {
            public const string SkillXPersonXSkillLevelId = "SkillXPersonXSkillLevelId";
            public const string SkillId = "SkillId";
            public const string PersonId = "PersonId";
            public const string SkillLevelId = "SkillLevelId";

            public const string Skill = "Skill";
            public const string Person = "Person";
            public const string SkillLevel = "SkillLevel";
        }

        public static readonly SkillXPersonXSkillLevelDataModel Empty = new SkillXPersonXSkillLevelDataModel();

        public int?     SkillXPersonXSkillLevelId { get; set; }
        public int?     SkillId { get; set; }
        public int?     PersonId { get; set; }
        public int?     SkillLevelId { get; set; }
        public string   Skill { get; set; }
        public string   Person { get; set; }
        public string   SkillLevel { get; set; }
    }
}