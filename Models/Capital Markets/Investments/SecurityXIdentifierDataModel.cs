using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

    public partial class SecurityXIdentifierDataModel : BaseModel
    {
        [PrimaryKey, IncludeInSearch]
        public int? SecurityXIdentifierId { get; set; }
        
        public string Ticker { get; set; }
        public string CUSIP { get; set; }
        public string SEDOL { get; set; }
        public string ISIN { get; set; }
        public string WKN { get; set; }
        public int? AltID1 { get; set; }
        public int? AltID2 { get; set; }
        public int? AltID3 { get; set; }
        public int? AltID4 { get; set; }
        public int? AltID5 { get; set; }

        [ForeignKey("Security"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? SecurityId { get; set; }

        [ForeignKeyName("Security", "SecurityId", "SecurityId", "Name"), OnlyProperty]
        public string Security { get; set; }
    }
}
