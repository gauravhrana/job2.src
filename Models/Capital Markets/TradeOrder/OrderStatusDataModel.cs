using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class OrderStatusDataModel : BaseModel
    {     

        [PrimaryKey, IncludeInSearch]
        public int? OrderStatusId           { get; set; }
         
		[IncludeInSearch]
        public int? OrderId                 { get; set; }

		[IncludeInSearch]
        public string Comments              { get; set; }
        public string LastModifiedBy        { get; set; }
        public DateTime? LastModifiedOn     { get; set; }

        [ForeignKey("OrderStatusType"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? OrderStatusTypeId { get; set; }

        [ForeignKeyName("OrderStatusType", "OrderStatusTypeId", "OrderStatusTypeId", "Code"), OnlyProperty]
        public string OrderStatusType { get; set; }

    }
}
