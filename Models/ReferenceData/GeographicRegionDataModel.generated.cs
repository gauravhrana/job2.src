using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class GeographicRegionDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string GeographicRegionId = "GeographicRegionId";
		}

		public static readonly GeographicRegionDataModel Empty = new GeographicRegionDataModel();

	}
}
