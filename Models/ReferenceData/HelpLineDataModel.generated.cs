using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class HelpLineDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string HelpLineId = "HelpLineId";
		}

		public static readonly HelpLineDataModel Empty = new HelpLineDataModel();

	}
}
