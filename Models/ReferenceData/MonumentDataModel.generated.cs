using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class MonumentDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string MonumentId = "MonumentId";
		}

		public static readonly MonumentDataModel Empty = new MonumentDataModel();

	}
}
