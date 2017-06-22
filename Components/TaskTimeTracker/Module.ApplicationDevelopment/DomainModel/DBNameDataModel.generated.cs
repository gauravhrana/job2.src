using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

	[Serializable]
	public partial class DBNameDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string DBNameId = "DBNameId";
		}

		public static readonly DBNameDataModel Empty = new DBNameDataModel();

		public int? DBNameId { get; set; }

	}
}
