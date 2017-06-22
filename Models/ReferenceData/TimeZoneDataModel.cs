﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class TimeZoneDataModel : StandardModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? TimeZoneId { get; set; }
        public decimal? TimeDifference { get; set; }

	}
}
