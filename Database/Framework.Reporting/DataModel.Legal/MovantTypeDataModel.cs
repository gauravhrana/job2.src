using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.Legal
{ 

    public partial class MovantTypeDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string MovantTypeId = "MovantTypeId";
        }

        public static readonly MovantTypeDataModel Empty = new MovantTypeDataModel();
        [PrimaryKey]
        public int? MovantTypeId { get; set; }

    }
}
