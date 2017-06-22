using Framework.Components.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DayCare
{
    public class EventSubTypeDataModel : BaseModel
    {

        public class DataColumns
        {
            public const string EventSubTypeId = "EventSubTypeId";

            public const string EventTypeId    = "EventTypeId";
            public const string EventType      = "EventType";

            public const string PersonId       = "PersonId";
            public const string Person         = "Person";

            public const string EventKey       = "EventKey";
            public const string Value          = "Value";
            public const string SortOrder      = "SortOrder";
        }

        public static readonly EventSubTypeDataModel Empty = new EventSubTypeDataModel();

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
