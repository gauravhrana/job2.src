using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{

	public partial class MenuCategoryDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string MenuCategoryId = "MenuCategoryId";
		}

		public static readonly MenuCategoryDataModel Empty = new MenuCategoryDataModel();

		public int? MenuCategoryId { get; set; }

	}
}
