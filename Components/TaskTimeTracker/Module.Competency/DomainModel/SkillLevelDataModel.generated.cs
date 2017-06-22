using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Competency
{

	[Serializable]
	public partial class SkillLevelDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string SkillLevelId = "SkillLevelId";
		}

		public static readonly SkillLevelDataModel Empty = new SkillLevelDataModel();

		public int? SkillLevelId { get; set; }

	}
}
