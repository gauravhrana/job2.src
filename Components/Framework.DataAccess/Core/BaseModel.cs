using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Components.DataAccess
{
    
    [Serializable]
    public class BaseModel
    {
        public class BaseColumns
        {
            public const string ApplicationId = "ApplicationId";
            public const string EntityKey     = "EntityKey";
        }

        public int? ApplicationId   { get; set; }
        public int? EntityKey       { get; set; }
    }
}
