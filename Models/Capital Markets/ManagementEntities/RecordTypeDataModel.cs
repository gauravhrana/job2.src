﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class RecordTypeDataModel : StandardModel
    {
        [PrimaryKey, IncludeInSearch]
        public int?      RecordTypeId { get; set; }
        public string    Code { get; set; }
     
    }
}
