using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class PersonSuffixDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string PersonSuffixId = "PersonSuffixId";
		}

		public static readonly PersonSuffixDataModel Empty = new PersonSuffixDataModel();

	}
}
