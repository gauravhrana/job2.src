using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class MonumentDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string MonumentId = "MonumentId";

		}

		public static readonly MonumentDataModel Empty = new MonumentDataModel();

		[PrimaryKey]
		public int? MonumentId { get; set; }

	}
}
