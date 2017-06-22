using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class RegionDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string RegionId = "RegionId";

		}

		public static readonly RegionDataModel Empty = new RegionDataModel();

		[PrimaryKey]
		public int? RegionId { get; set; }

	}
}
