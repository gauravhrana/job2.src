using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class OrderStatusDataModel : BaseModel
    {
        public class DataColumns
        {
            public const string OrderStatusId        = "OrderStatusId";
            public const string OrderId              = "OrderId";
            public const string OrderStatusTypeId    = "OrderStatusTypeId";
            public const string Comments             = "Comments";
            public const string LastModifiedBy       = "LastModifiedBy";
            public const string LastModifiedOn       = "LastModifiedOn";
        }

        public static readonly OrderStatusDataModel Empty = new OrderStatusDataModel();

        [PrimaryKey, IncludeInSearch]
        public int? OrderStatusId            { get; set; }

        public string OrderId                { get; set; }
        public string OrderStatusTypeId      { get; set; }
        public string Comments               { get; set; }
        public string LastModifiedBy         { get; set; }
        public string LastModifiedOn         { get; set; }
       
    }
}
