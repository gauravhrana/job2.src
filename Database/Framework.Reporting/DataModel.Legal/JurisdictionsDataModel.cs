using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.Legal
{ 

    public partial class JurisdictionsDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string JurisdictionsId = "JurisdictionsId";
        }

        public static readonly JurisdictionsDataModel Empty = new JurisdictionsDataModel();
        [PrimaryKey]
        public int? JurisdictionsId { get; set; }

    }
}
