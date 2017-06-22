using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class HelpLineDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string HelpLineId = "HelpLineId";

		}

		public static readonly HelpLineDataModel Empty = new HelpLineDataModel();

		[PrimaryKey]
		public int? HelpLineId { get; set; }

	}
}
