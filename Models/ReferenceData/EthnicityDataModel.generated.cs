using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class EthnicityDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string EthnicityId = "EthnicityId";
		}

		public static readonly EthnicityDataModel Empty = new EthnicityDataModel();

	}
}
