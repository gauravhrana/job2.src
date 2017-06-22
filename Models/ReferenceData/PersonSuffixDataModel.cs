using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{
	public partial class PersonSuffixDataModel : StandardModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? PersonSuffixId { get; set; }
	}
}
