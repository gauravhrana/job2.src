using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.Legal
{


    [Serializable]
    public partial class ReportTypeDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string ReportTypeId = "ReportTypeId";
        }

        public static readonly ReportTypeDataModel Empty = new ReportTypeDataModel();
        [PrimaryKey]
        public int? ReportTypeId { get; set; }

    }
}
