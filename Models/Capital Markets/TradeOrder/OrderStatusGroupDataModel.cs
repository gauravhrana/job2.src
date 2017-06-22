using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class OrderStatusGroupDataModel : BaseModel
    {
        [PrimaryKey, IncludeInSearch]
        public int? OrderStatusGroupId            { get; set; }
         
        [IncludeInSearch]
        public string OrderStatusGroupCode        { get; set; }
        public string OrderStatusGroupDescription { get; set; }
        public int OrderProcessFlag               { get; set; }
    }
}
