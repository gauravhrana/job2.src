using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class AutomatedFreezepointDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AutomatedFreezepointId = "AutomatedFreezepointId";
		}

		public static readonly AutomatedFreezepointDataModel Empty = new AutomatedFreezepointDataModel();

	}
}