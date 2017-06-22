using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Audit
{

	[Serializable]
	public partial class TraceDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string TraceId = "TraceId";
		}

		public static readonly TraceDataModel Empty = new TraceDataModel();

		public int? TraceId { get; set; }

	}
}
