using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{

	[Serializable]
	public partial class SystemEntityCategoryDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string SystemEntityCategoryId = "SystemEntityCategoryId";
		}

		public static readonly SystemEntityCategoryDataModel Empty = new SystemEntityCategoryDataModel();

		public int? SystemEntityCategoryId { get; set; }

	}
}
