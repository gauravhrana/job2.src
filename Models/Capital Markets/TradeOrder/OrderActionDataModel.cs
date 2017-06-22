using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class OrderActionDataModel : BaseModel
    {      

        [PrimaryKey, IncludeInSearch]
        public int? OrderActionId            { get; set; }
         
        [IncludeInSearch]
        public string OrderActionCode        { get; set; }
        public string OrderActionDescription { get; set; }
        public string PositionDirection      { get; set; }
        
        
    }
}
