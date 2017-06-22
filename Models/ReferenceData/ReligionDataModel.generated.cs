using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class ReligionDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string ReligionId = "ReligionId";
		}

		public static readonly ReligionDataModel Empty = new ReligionDataModel();

	}
}
