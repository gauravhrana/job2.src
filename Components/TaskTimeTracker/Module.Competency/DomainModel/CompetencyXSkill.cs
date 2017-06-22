using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace DataModel.CompetencyTimeTracker.Skill
{
    [Serializable]
    public class CompetencyXSkillDataModel : BaseDataModel
    {
        public class DataColumns
        {
            public const string CompetencyXSkillId = "CompetencyXSkillId";
            public const string CompetencyId = "CompetencyId";
            public const string SkillId = "SkillId";

            public const string Competency = "Competency";
            public const string Skill = "Skill";
        }

        public static readonly CompetencyXSkillDataModel Empty = new CompetencyXSkillDataModel();

        public int? CompetencyXSkillId { get; set; }
        public int? CompetencyId { get; set; }
        public int? SkillId { get; set; }

        public string Competency { get; set; }
        public string Skill { get; set; }

    }
}
