using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class GeographicRegionDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string GeographicRegionId = "GeographicRegionId";
		}

		public static readonly GeographicRegionDataModel Empty = new GeographicRegionDataModel();

		public int? GeographicRegionId { get; set; }

	}
}
