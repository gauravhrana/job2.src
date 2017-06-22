using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class MSPAFileEventDataModel : BaseModel
    {

        [PrimaryKey, IncludeInSearch] 
        public int? MSPAFileEventId { get; set; }

        [IncludeInSearch]
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }       

        [ForeignKey("MSPAFile"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? MSPAFileId { get; set; }

        [ForeignKeyName("MSPAFile", "MSPAFileId", "MSPAFileId", "Filename"), OnlyProperty]
        public string MSPAFile { get; set; }

        [ForeignKey("TradingEventType"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? TradingEventTypeId { get; set; }

        [ForeignKeyName("TradingEventType", "TradingEventTypeId", "TradingEventTypeId", "Name"), OnlyProperty]
        public string TradingEventType { get; set; }
       
    }
}
