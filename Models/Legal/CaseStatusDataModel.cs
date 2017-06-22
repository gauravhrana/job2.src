using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.Legal
{    

    [Serializable]
    public partial class CaseStatusDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string CaseStatusId = "CaseStatusId";
        }

        public static readonly CaseStatusDataModel Empty = new CaseStatusDataModel();
        [PrimaryKey]
        public int? CaseStatusId { get; set; }

    }
}
