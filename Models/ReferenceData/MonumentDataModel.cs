﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class MonumentDataModel : StandardModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? MonumentId { get; set; }

	}
}
