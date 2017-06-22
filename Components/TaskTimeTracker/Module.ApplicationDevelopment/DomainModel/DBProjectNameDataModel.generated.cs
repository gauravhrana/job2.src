using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

	[Serializable]
	public partial class DBProjectNameDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string DBProjectNameId = "DBProjectNameId";
		}

		public static readonly DBProjectNameDataModel Empty = new DBProjectNameDataModel();

		public int? DBProjectNameId { get; set; }

	}
}
