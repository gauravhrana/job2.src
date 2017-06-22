using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class RaceTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string RaceTypeId = "RaceTypeId";
		}

		public static readonly RaceTypeDataModel Empty = new RaceTypeDataModel();

	}
}
