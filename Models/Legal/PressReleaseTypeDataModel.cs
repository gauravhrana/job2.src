using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.Legal
{


    [Serializable]
    public partial class PressReleaseTypeDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string PressReleaseTypeId = "PressReleaseTypeId";
        }

        public static readonly PressReleaseTypeDataModel Empty = new PressReleaseTypeDataModel();
        [PrimaryKey]
        public int? PressReleaseTypeId { get; set; }

    }
}
