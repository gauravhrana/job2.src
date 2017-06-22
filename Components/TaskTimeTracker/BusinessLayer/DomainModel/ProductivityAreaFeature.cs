using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class ProductivityAreaFeatureDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ProductivityAreaFeatureId = "ProductivityAreaFeatureId";
		}

		public int? ProductivityAreaFeatureId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"ProductivityAreaFeatureId=" + ProductivityAreaFeatureId
		}
	}
}
