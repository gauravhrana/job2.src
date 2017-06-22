using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.Legal
{ 

    public partial class CounselDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string CounselId = "CounselId";
        }

        public static readonly CounselDataModel Empty = new CounselDataModel();

        [PrimaryKey]
        public int? CounselId { get; set; }

    }
}
