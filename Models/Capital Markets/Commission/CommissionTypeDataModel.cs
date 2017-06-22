using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets 
{
    public partial class CommissionTypeDataModel : BaseModel
    {

        [PrimaryKey, IncludeInSearch]
        public int? CommissionTypeId { get; set; }

		[IncludeInSearch]
        public string CommissionTypeDescription      { get; set; }
        public string LastModifiedBy                 { get; set; }
        public DateTime? LastModifiedOn              { get; set; }
        public int ShowInFilter                      { get; set; }

    }
}
