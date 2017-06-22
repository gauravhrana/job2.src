using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.Legal 
{

    public partial class ReportingRequirementDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string ReportingRequirementId = "ReportingRequirementId";
        }   

        public static readonly ReportingRequirementDataModel Empty = new ReportingRequirementDataModel();
        [PrimaryKey]
        public int? ReportingRequirementId { get; set; }

    }
}
