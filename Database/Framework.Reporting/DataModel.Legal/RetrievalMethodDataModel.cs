using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.Legal
{
     
    public partial class RetrievalMethodDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string RetrievalMethodId = "RetrievalMethodId";
        }

        public static readonly RetrievalMethodDataModel Empty = new RetrievalMethodDataModel();
        [PrimaryKey]
        public int? RetrievalMethodId { get; set; }

    }
}
