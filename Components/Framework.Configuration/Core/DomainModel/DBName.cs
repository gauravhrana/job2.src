﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Configuration
{
    public class DBNameDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string DBNameId = "DBNameId";
        }

        public int? DBNameId { get; set; }

    }

}
