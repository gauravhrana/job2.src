using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class CommissionCodeDataModel : BaseModel
    {

        [PrimaryKey, IncludeInSearch]
        public int? CommissionCodeId { get; set; }

		[IncludeInSearch]
        public string CommissionCodeCode        { get; set; }
        public string CommissionCodeDescription { get; set; }        

        [ForeignKey("Broker"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? BrokerId { get; set; }

        [ForeignKeyName("Broker", "BrokerId", "BrokerId", "Name"), OnlyProperty]
        public string Broker { get; set; }

    }
}
