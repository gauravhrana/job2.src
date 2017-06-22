using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class OrderRequestDataModel : BaseModel
    {      

        [PrimaryKey, IncludeInSearch]
        public int? OrderRequestId { get; set; }
                 
        public DateTime?      EventDate               { get; set; }
		[IncludeInSearch]
        public string   Notes                { get; set; }
        public string   LastModifiedBy       { get; set; }
        public DateTime? LastModifiedOn       { get; set; }
        public int      ParentOrderRequestId { get; set; }

        [ForeignKey("Portfolio"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? PortfolioId { get; set; }

        [ForeignKeyName("Portfolio", "PortfolioId", "PortfolioId", "Name"), OnlyProperty]
        public string Portfolio { get; set; }
        
    
    }
}
