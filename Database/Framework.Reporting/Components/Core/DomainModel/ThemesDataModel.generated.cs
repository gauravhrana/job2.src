using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{

	public partial class ThemesDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ThemesId = "ThemesId";
		}

		public static readonly ThemesDataModel Empty = new ThemesDataModel();

		public int? ThemesId { get; set; }

	}
}
