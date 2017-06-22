using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class OrderTypeDataModel : BaseModel
    {       
        [PrimaryKey, IncludeInSearch] 
        public int? OrderTypeId { get; set; } 

        [IncludeInSearch] 
        public string Code          { get; set; }
        public string Description   { get; set; }        


    }
}
