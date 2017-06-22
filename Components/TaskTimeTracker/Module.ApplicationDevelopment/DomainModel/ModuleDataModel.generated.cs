using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

	[Serializable]
	public partial class ModuleDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ModuleId = "ModuleId";
		}

		public static readonly ModuleDataModel Empty = new ModuleDataModel();

		public int? ModuleId { get; set; }

	}
}
