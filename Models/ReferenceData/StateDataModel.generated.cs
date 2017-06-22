using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class StateDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string StateId = "StateId";
			public const string CountryId = "CountryId";
			public const string Country = "Country";
		}

		public static readonly StateDataModel Empty = new StateDataModel();

	}
}
