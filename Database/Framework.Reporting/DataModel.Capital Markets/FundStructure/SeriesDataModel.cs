using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

    public partial class SeriesDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string SeriesId = "SeriesId";
        }

        public static readonly SeriesDataModel Empty = new SeriesDataModel();
        [PrimaryKey, IncludeInSearch]
        public int? SeriesId { get; set; }

    }
}
