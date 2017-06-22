using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class RegionDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string RegionId = "RegionId";
		}

		public static readonly RegionDataModel Empty = new RegionDataModel();

		public int? RegionId { get; set; }

	}
}
