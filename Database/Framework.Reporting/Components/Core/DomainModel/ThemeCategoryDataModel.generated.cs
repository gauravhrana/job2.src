using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{

	public partial class ThemeCategoryDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ThemeCategoryId = "ThemeCategoryId";
		}

		public static readonly ThemeCategoryDataModel Empty = new ThemeCategoryDataModel();

		public int? ThemeCategoryId { get; set; }

	}
}
