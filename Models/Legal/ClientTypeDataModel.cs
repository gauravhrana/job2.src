using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.Legal
{


    [Serializable]
    public partial class ClientTypeDataModel : StandardModel
    {

        public class DataColumns : StandardColumns 
        {
            public const string ClientTypeId = "ClientTypeId";
        }

        public static readonly ClientTypeDataModel Empty = new ClientTypeDataModel();
        [PrimaryKey]
        public int? ClientTypeId { get; set; }

    }
}
