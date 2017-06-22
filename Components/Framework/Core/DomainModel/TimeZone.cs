using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class TimeZoneDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string TimeZoneId = "TimeZoneId";
            public const string TimeDifference = "TimeDifference";
        }

		public static readonly TimeZoneDataModel Empty = new TimeZoneDataModel();

        public int? TimeZoneId { get; set; }
        public decimal? TimeDifference { get; set; }
  
    }
}
