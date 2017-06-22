using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{

	[Serializable]
	public partial class ThemeKeyDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ThemeKeyId = "ThemeKeyId";
		}

		public static readonly ThemeKeyDataModel Empty = new ThemeKeyDataModel();

		public int? ThemeKeyId { get; set; }

	}
}
