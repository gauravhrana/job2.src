using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class CountryDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string CountryId = "CountryId";
            public const string TimeZoneId = "TimeZoneId";
            public const string TimeZone = "TimeZone";
        }

		public static readonly CountryDataModel Empty = new CountryDataModel();

        public int? CountryId { get; set; }
        public decimal? TimeZoneId { get; set; }

    }
}
