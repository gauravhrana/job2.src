using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class CommissionSplitDataModel : BaseModel
    {

        [PrimaryKey, IncludeInSearch]
        public int? CommissionSplitId { get; set; }

		[IncludeInSearch]
        public string CommissionSplitCode        { get; set; }
        public string CommissionSplitDescription { get; set; }
        public decimal? FullRate                 { get; set; }
        public decimal? NoneCCA                  { get; set; }
        public decimal? CCA                      { get; set; }
        public DateTime? StartDate               { get; set; }
        public DateTime? EndDate                 { get; set; }
        public string LastModifiedBy             { get; set; }
        public DateTime? LastModifiedOn          { get; set; }

        [ForeignKey("CommissionCode"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? CommissionCodeId { get; set; }

        [ForeignKeyName("CommissionCode", "CommissionCodeId", "CommissionCodeId", "CommissionCodeCode"), OnlyProperty]
        public string CommissionCode { get; set; }

    }
}
