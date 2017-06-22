using Framework.Components.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DayCare
{
    public partial class EventSubTypeDataModel : BaseModel
    {    
        [PrimaryKey, IncludeInSearch]
        public int? EventSubTypeId { get; set; }

        [ForeignKey("EventType"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? EventTypeId { get; set; }
        [ForeignKeyName("EventType", "EventTypeId", "EventTypeId", "Name"), OnlyProperty]
        public string EventType { get; set; }

        [ForeignKey("ApplicationUser", "AuthenticationAndAuthorization"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? PersonId { get; set; }
        [ForeignKeyName("ApplicationUser", "PersonId", "ApplicationUserId", "ApplicationUserName", "AuthenticationAndAuthorization"), OnlyProperty]
        public string Person { get; set; }

        [IncludeInSearch]
        public string EventKey { get; set; }

        public int SortOrder { get; set; }
        public int Value { get; set; }

    }
}

