using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class SexTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string SexTypeId = "SexTypeId";
		}

		public static readonly SexTypeDataModel Empty = new SexTypeDataModel();

	}
}
