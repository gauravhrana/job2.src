using Framework.Components.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.CapitalMarkets
{
    public partial class OrderStatusTypeDataModel : BaseModel
    {
        
        [PrimaryKey, IncludeInSearch]
        public int? OrderStatusTypeId { get; set; }
         
        [IncludeInSearch] 
        public string Code           { get; set; }
        public string Description    { get; set; }

        [ForeignKey("OrderStatusGroup"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? OrderStatusGroupId { get; set; }

        [ForeignKeyName("OrderStatusGroup", "OrderStatusGroupId", "OrderStatusGroupId", "OrderStatusGroupCode"), OnlyProperty]
        public string OrderStatusGroup { get; set; }
    }
}
