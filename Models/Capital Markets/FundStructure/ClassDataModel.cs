﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

    public partial class ClassDataModel : StandardModel
    {

        [PrimaryKey, IncludeInSearch]
        public int? ClassId { get; set; }

		[ForeignKey("Fund"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? FundId { get; set; }

		[ForeignKeyName("Fund", "FundId", "FundId", "Name"), OnlyProperty]
		public string Fund { get; set; }

    }
}
