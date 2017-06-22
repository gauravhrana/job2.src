using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class ProductivityAreaDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ProductivityAreaId = "ProductivityAreaId";
		}

		public int? ProductivityAreaId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"ProductivityAreaId=" + ProductivityAreaId
		}
	}
}
