using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{
	public partial class GeographicRegionDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string GeographicRegionId = "GeographicRegionId";

		}

		public static readonly GeographicRegionDataModel Empty = new GeographicRegionDataModel();

		[PrimaryKey]
		public int? GeographicRegionId { get; set; }

	}
}
