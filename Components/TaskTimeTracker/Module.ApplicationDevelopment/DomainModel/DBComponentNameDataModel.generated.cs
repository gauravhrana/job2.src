using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

	[Serializable]
	public partial class DBComponentNameDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string DBComponentNameId = "DBComponentNameId";
		}

		public static readonly DBComponentNameDataModel Empty = new DBComponentNameDataModel();

		public int? DBComponentNameId { get; set; }

	}
}
