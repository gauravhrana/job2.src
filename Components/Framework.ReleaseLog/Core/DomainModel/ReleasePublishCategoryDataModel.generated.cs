using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.ReleaseLog
{

	[Serializable]
	public partial class ReleasePublishCategoryDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ReleasePublishCategoryId = "ReleasePublishCategoryId";
		}

		public static readonly ReleasePublishCategoryDataModel Empty = new ReleasePublishCategoryDataModel();

		public int? ReleasePublishCategoryId { get; set; }

	}
}
