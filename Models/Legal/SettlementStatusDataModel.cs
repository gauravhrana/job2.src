using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.Legal
{


    [Serializable]
    public partial class SettlementStatusDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string SettlementStatusId = "SettlementStatusId";
        }

        public static readonly SettlementStatusDataModel Empty = new SettlementStatusDataModel();
        [PrimaryKey]
        public int? SettlementStatusId { get; set; }

    }
}
