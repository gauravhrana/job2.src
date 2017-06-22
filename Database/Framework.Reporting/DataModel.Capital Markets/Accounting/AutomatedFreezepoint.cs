using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class AutomatedFreezepointDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AutomatedFreezepointId = "AutomatedFreezepointId";
		}

		public static readonly AutomatedFreezepointDataModel Empty = new AutomatedFreezepointDataModel();
		[PrimaryKey]
		public int? AutomatedFreezepointId { get; set; }

	}
}
