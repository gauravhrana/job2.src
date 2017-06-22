using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class CountryDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string CountryId = "CountryId";
		}

		public static readonly CountryDataModel Empty = new CountryDataModel();

	}
}
