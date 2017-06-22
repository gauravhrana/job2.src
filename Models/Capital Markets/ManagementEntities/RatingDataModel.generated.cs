using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class RatingDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string RatingId = "RatingId";
			public const string Date = "Date";
			public const string Analyst = "Analyst";
			public const string Rating = "Rating";
			public const string Notes = "Notes";
		}

		public static readonly RatingDataModel Empty = new RatingDataModel();

	}
}
