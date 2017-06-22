using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{

	[Serializable]
	public partial class LanguageDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string LanguageId = "LanguageId";
		}

		public static readonly LanguageDataModel Empty = new LanguageDataModel();

		public int? LanguageId { get; set; }

	}
}
