using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

    public partial class ClassDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string ClassId = "ClassId";
        }

        public static readonly ClassDataModel Empty = new ClassDataModel();
        [PrimaryKey, IncludeInSearch]
        public int? ClassId { get; set; }

    }
}
