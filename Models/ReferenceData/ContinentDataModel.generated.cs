using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class ContinentDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string ContinentId = "ContinentId";
		}

		public static readonly ContinentDataModel Empty = new ContinentDataModel();

	}
}
