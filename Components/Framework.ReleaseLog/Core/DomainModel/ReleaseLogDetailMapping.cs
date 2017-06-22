using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.ReleaseLog
{
    [Serializable]
	public class ReleaseLogDetailMappingDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string ReleaseLogDetailMappingId		= "ReleaseLogDetailMappingId";
			public const string ReleasePublishCategoryParentId	= "ReleasePublishCategoryParentId";
			public const string ReleasePublishCategoryChildId	= "ReleasePublishCategoryChildId";
		}

		public static readonly ReleaseLogDetailMappingDataModel Empty = new ReleaseLogDetailMappingDataModel();

		public int? ReleaseLogDetailMappingId { get; set; }
		public int? ReleasePublishCategoryParentId { get; set; }
		public int? ReleasePublishCategoryChildId { get; set; }

	}
}
