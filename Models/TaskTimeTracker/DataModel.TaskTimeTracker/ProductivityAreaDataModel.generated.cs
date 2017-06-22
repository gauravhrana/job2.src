using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{

	[Serializable]
	public partial class ProductivityAreaDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ProductivityAreaId = "ProductivityAreaId";
		}

		public static readonly ProductivityAreaDataModel Empty = new ProductivityAreaDataModel();

		public int? ProductivityAreaId { get; set; }

	}
}
