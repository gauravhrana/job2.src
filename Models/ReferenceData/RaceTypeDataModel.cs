using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{
	public partial class RaceTypeDataModel : StandardModel
	{
		[PrimaryKey]
		public int? RaceTypeId { get; set; }
	}
}
