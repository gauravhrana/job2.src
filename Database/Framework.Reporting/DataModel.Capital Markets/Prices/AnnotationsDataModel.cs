using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class AnnotationsDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AnnotationsId = "AnnotationsId";
		}

		public static readonly AnnotationsDataModel Empty = new AnnotationsDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? AnnotationsId { get; set; }

	}
}
