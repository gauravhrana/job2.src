using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class ProvinceTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string ProvinceTypeId = "ProvinceTypeId";
		}

		public static readonly ProvinceTypeDataModel Empty = new ProvinceTypeDataModel();

	}
}
