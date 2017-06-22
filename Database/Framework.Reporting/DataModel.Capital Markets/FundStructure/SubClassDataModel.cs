using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

    public partial class SubClassDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string SubClassId = "SubClassId";
        }

        public static readonly SubClassDataModel Empty = new SubClassDataModel();
        [PrimaryKey, IncludeInSearch]
        public int? SubClassId { get; set; }

    }
}
