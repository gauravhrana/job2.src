using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.Legal
{
    [Serializable]
    public partial class CaseTypeDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string CaseTypeId = "CaseTypeId";
        }

        public static readonly CaseTypeDataModel Empty = new CaseTypeDataModel();
        [PrimaryKey]
        public int? CaseTypeId { get; set; }

    }
}
