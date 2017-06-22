using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{

	[Serializable]
	public partial class ProductivityAreaFeatureDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ProductivityAreaFeatureId = "ProductivityAreaFeatureId";
		}

		public static readonly ProductivityAreaFeatureDataModel Empty = new ProductivityAreaFeatureDataModel();

		public int? ProductivityAreaFeatureId { get; set; }

	}
}
